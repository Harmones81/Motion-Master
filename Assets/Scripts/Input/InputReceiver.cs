using UnityEngine;
using UnityEngine.InputSystem;

namespace MotionMaster.Input
{
    public abstract class InputReceiver : MonoBehaviour
    {
        protected InputReader reader;
        protected InputUser user;
        protected Controls controls;

        protected virtual void Awake()
        {
            controls = new Controls();
            reader = new InputReader(controls);
        }

        protected virtual void OnEnable()
        {
            SetUser();
            SetDevicePairing();

            InputManager.Instance.onUserAdded += UserAdded;
            InputManager.Instance.onUserRemoved += UserRemoved;
            InputManager.Instance.onMainUserDeviceChange += MainUserDeviceChange;
        }

        protected void SetDevicePairing()
        {
            if (user == InputManager.Instance.MainUser)
            {
                controls.devices = new[] { Keyboard.current, user.Device };
            }
            else
            {
                controls.devices = new[] { user.Device };
            }
        }

        protected abstract void SetUser();

        protected abstract void UserAdded(InputUser user);

        protected abstract void UserRemoved(InputUser user);

        protected void MainUserDeviceChange() => SetDevicePairing();
    }
}
