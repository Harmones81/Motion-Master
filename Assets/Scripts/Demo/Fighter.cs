using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MotionMaster
{
    public enum ScreenPosition { Left, Right }

    public class Fighter : MonoBehaviour
    {
        [Header("DATA")]
        [SerializeField] public ScreenPosition screenPosition;

        public ScreenPosition ScreenPosition { get {  return screenPosition; } }
    }
}
