using MotionMaster.Input;
using System;
using UnityEngine;

namespace MotionMaster
{
    [Flags]
    public enum Inputs
    {
        None = 0,

        Forward = 1,
        Up = 2,
        Down = 4,
        Back = 8,

        Up_Forward = Forward | Up,
        Down_Forward = Forward | Down,
        Up_Back = Back | Up,
        Down_Back = Back | Down,

        Attack = 16,
    }

    [RequireComponent(typeof(Fighter))]
    public class FighterInput : InputReceiver
    {
        public const int BUFFER_SIZE = 60;
        public const float ANALOG_DEADZONE = 0.01f;

        [Header("DEPENDENCIES")]
        [SerializeField] private Fighter fighter;
        [SerializeField] private InputBuffer inputBuffer;
        [Header("BUTTON VALUE")]
        [SerializeField] private bool attackPressed;
        [Header("ANALOG VALUE")]
        [SerializeField] private Vector2 directionValue;
        [Header("PLAYER INPUT")]
        [SerializeField] private Inputs currentInput;

        public Inputs CurrentInput => currentInput;

        public InputBuffer InputBuffer { get { return inputBuffer; } }

        private bool Up => directionValue.y >= ANALOG_DEADZONE;

        private bool Down => directionValue.y <= -ANALOG_DEADZONE;

        private bool Forward => fighter.ScreenPosition == ScreenPosition.Left ? directionValue.x >= ANALOG_DEADZONE
            : directionValue.x <= -ANALOG_DEADZONE;

        private bool Back => fighter.ScreenPosition == ScreenPosition.Left ? directionValue.x <= -ANALOG_DEADZONE
            : directionValue.x >= ANALOG_DEADZONE;

        protected override void Awake()
        {
            base.Awake();

            fighter = GetComponent<Fighter>();
            inputBuffer = new InputBuffer(BUFFER_SIZE);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            reader.EnableActionMap("Game");
            reader.attackPressed += AttackPressed;
            reader.attackReleased += AttackReleased;
            reader.onDirectionChange += DirectionChange;
        }

        private void OnDisable()
        {
            reader.attackPressed -= AttackPressed;
            reader.attackReleased -= AttackReleased;
            reader.onDirectionChange -= DirectionChange;
        }

        private void Update()
        {
            UpdateCurrentInput();
            inputBuffer.Update(currentInput);
        }

        private void DirectionChange(Vector2 direction) => directionValue = direction;

        private void AttackPressed() => attackPressed = true;

        private void AttackReleased() => attackPressed = false;

        private void UpdateCurrentInput()
        {
            if (Forward) { currentInput |= Inputs.Forward; } else { currentInput &= ~Inputs.Forward; }
            if (Back) { currentInput |= Inputs.Back; } else { currentInput &= ~Inputs.Back; }
            if (Up) { currentInput |= Inputs.Up; } else { currentInput &= ~Inputs.Up; }
            if (Down) { currentInput |= Inputs.Down; } else { currentInput &= ~Inputs.Down; }

            if (attackPressed) { currentInput |= Inputs.Attack; } else { currentInput &= ~Inputs.Attack; }
        }

        protected override void SetUser()
        {
            user = InputManager.Instance.MainUser;
        }

        protected override void UserAdded(InputUser user)
        {
            return;
        }

        protected override void UserRemoved(InputUser user)
        {
            return;
        }

        private void OnGUI()
        {
            
        }
    }
}
