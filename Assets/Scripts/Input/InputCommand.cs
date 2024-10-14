using System;
using System.Collections.Generic;
using UnityEngine;

namespace MotionMaster.Input
{
    /// <summary>
    /// struct that holds an array of commands that form a sequence
    /// </summary>
    [System.Serializable]
    public struct CommandSequence
    {
        public Command[] commands;
    }

    /// <summary>
    /// represents a single command within a sequence
    /// </summary>
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
        /// <summary>
        /// event that is invoked when all the checked inputs satisfy the sequence. 
        /// Is used by other components to set the checked inputs used state to true. 
        /// Doing this ensures that those inputs are used to check for future commands.
        /// </summary>
        public event Action<List<InputData>> onCheckedInputs;

        [Header("SETTINGS")]
        public int commandPriority;
        [Range(1, FighterInput.BUFFER_SIZE)] public int checkWindow;

        [Header("SEQUENCES")]
        public CommandSequence[] sequences;

        /// <summary>
        /// list of inputs that were checked to see if the command was satisfied
        /// </summary>
        private List<InputData> checkedInputs;

        /// <summary>
        /// method used to check if any of the allowed sequences were executed
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
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

        /// <summary>
        /// method that checks a single sequence to see if it was executed
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
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
