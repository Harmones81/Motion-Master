using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MotionMaster.Input
{
    /// <summary>
    /// class that reads input utilizing the input action asset
    /// </summary>
    public class InputReader : Controls.IGameActions, Controls.IMenuActions
    {
        public event Action<Vector2> onDirectionChange;
        public event Action<Vector2> onNavigationChange;

        public event Action attackPressed;
        public event Action submitPressed;
        public event Action cancelPressed;

        public event Action attackReleased;
        public event Action submitReleased;
        public event Action cancelReleased;

        private Controls controls;

        public InputReader(Controls controls)
        {
            this.controls = controls;
            this.controls.Game.SetCallbacks(this);
            this.controls.Menu.SetCallbacks(this);
        }

        public void EnableActionMap(string actionMap)
        {
            switch(actionMap)
            {
                case "Game":
                    controls.Game.Enable();
                    controls.Menu.Disable();
                    break;
                case "Menu":
                    controls.Menu.Enable();
                    controls.Game.Disable();
                    break;
            }
        }

        public void OnDirections(InputAction.CallbackContext context) => onDirectionChange?.Invoke(context.ReadValue<Vector2>());

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
            {
                attackPressed?.Invoke();
            }

            if(context.phase == InputActionPhase.Canceled)
            {
                attackReleased?.Invoke();
            }
        }

        public void OnNavigation(InputAction.CallbackContext context) => onNavigationChange?.Invoke(context.ReadValue<Vector2>());

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                submitPressed?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                submitReleased?.Invoke();
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                cancelPressed?.Invoke();
            }

            if (context.phase == InputActionPhase.Canceled)
            {
                cancelReleased?.Invoke();
            }
        }
    }
}
