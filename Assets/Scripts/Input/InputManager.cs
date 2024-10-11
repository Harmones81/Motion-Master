using MotionMaster.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MotionMaster.Input
{
    public class InputManager : Singleton<InputManager>
    {
        private const int MAX_USER_AMOUNT = 3;

        public event Action onMainUserDeviceChange;
        public event Action<InputUser> onUserAdded;
        public event Action<InputUser> onUserRemoved;

        private InputUser mainUser;
        private FixedList<InputUser> users;
        private FixedList<InputDevice> devices;

        public InputUser MainUser { get { return mainUser; } }
        public FixedList<InputUser> Users { get { return users; } }
        public FixedList<InputDevice> Devices { get { return devices; } }

        private void Awake()
        {
            mainUser = new InputUser(1);
            users = new FixedList<InputUser>(MAX_USER_AMOUNT);
            devices = new FixedList<InputDevice>(MAX_USER_AMOUNT + 1);
        }
    }
}
