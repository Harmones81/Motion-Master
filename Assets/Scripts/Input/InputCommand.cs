using System;
using System.Collections.Generic;
using UnityEngine;

namespace MotionMaster.Input
{
    [System.Serializable]
    public struct CommandSequence
    {
        public Command[] commands;
    }

    [System.Serializable]
    public struct Command
    {
        public Inputs reqInput;
        public int reqHoldTime;
        public bool allowExcessHold;
    }

    [CreateAssetMenu(menuName = "Motion Master/Input/Command")]
    public class InputCommand : ScriptableObject
    {
        public event Action<List<InputData>> onCheckedInputs;

        [Header("SETTINGS")]
        public int commandPriority;
        [Range(1, FighterInput.BUFFER_SIZE)] public int checkWindow;

        [Header("SEQUENCES")]
        public CommandSequence[] sequences;

        private List<InputData> checkedInputs;

        public bool IsMet(InputData[] buffer)
        {
            foreach(var seq in sequences)
            {
                if(CheckSequence(seq, buffer))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckSequence(CommandSequence seq, InputData[] buffer)
        {
            checkedInputs.Clear();

            int seqIndex = 0;
            int seqLength = seq.commands.Length - 1;
            int startIndex = (buffer.Length - 1) - checkWindow;

            for(int i = startIndex; i < buffer.Length; i++)
            {
                if (buffer[i] == null)
                {
                    continue;
                }

                var curInput = buffer[i];
                var curComm = seq.commands[seqIndex];

                if((curInput.input & curComm.reqInput) == curInput.input)
                {
                    if(curInput.CanExecute(curComm.reqHoldTime, curComm.allowExcessHold))
                    {
                        seqIndex++;
                        checkedInputs.Add(curInput);

                        if(seqIndex >= seqLength)
                        {
                            onCheckedInputs?.Invoke(checkedInputs);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
