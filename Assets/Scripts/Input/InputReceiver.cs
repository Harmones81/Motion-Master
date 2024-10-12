using UnityEngine;
using UnityEngine.InputSystem;

namespace MotionMaster.Input
{
    /// <summary>
    /// an abstract class that handles all the common behaviors for any gameobject that receives input
    /// </summary>
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

        /// <summary>
        /// alters the device pairings for the input action asset for this object
        /// </summary>
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

        /// <summary>
        /// sets a user for this object
        /// </summary>
        protected abstract void SetUser();

        /// <summary>
        /// callback that triggers when a user gets added to the system
        /// </summary>
        /// <param name="user">the user that was added</param>
        protected abstract void UserAdded(InputUser user);

        /// <summary>
        /// callback that triggers when a user gets removed from the system
        /// </summary>
        /// <param name="user">the user that was removed from the system</param>
        protected abstract void UserRemoved(InputUser user);

        /// <summary>
        /// callback that triggers when the main user's device undergoes a change. 
        /// Since the main user is always present, we simply alter their device pairings
        /// </summary>
        protected void MainUserDeviceChange() => SetDevicePairing();
    }
}
