using UnityEngine.InputSystem;

namespace MotionMaster.Input
{
    /// <summary>
    /// represents an input user that has control of a device in the system
    /// </summary>
    [System.Serializable]
    public class InputUser
    {
        /// <summary>
        /// a ghost device that simulates a real device.
        /// However, it has no control scheme attached to it and;
        /// thus, cannot perform any actions/inputs.
        /// </summary>
        public static Mouse _ghostDevice = InputSystem.AddDevice<Mouse>();
        /// <summary>
        /// the user's personal id number
        /// </summary>
        private int id;
        /// <summary>
        /// the device assigned to this user
        /// </summary>
        private InputDevice device;

        public InputDevice Device { get { return device; } }

        public InputUser(int id)
        {
            this.id = id;
            device = _ghostDevice;
        }

        public InputUser(int id, InputDevice device)
        {
            this.id = id;
            this.device = device;
        }

        /// <summary>
        /// sets the user's device to a new device
        /// </summary>
        /// <param name="device">the device the user's device will be set to</param>
        public void SetDevice(InputDevice device) => this.device = device;

        /// <summary>
        /// resets the user's device to its default device configuration (ghost)
        /// </summary>
        public void ResetDevice() => device = _ghostDevice;

        /// <summary>
        /// signifies if the user's device can be paired to an adequate device
        /// </summary>
        /// <returns></returns>
        public bool IsReady() => device == null || device == _ghostDevice;
    }
}
