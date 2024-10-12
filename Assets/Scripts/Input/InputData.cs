namespace MotionMaster.Input
{
    [System.Serializable]
    public class InputData
    {
        /// <summary>
        /// the input this frame
        /// </summary>
        public Inputs input;
        /// <summary>
        /// the amount of time the input was held
        /// </summary>
        public int holdTime;
        /// <summary>
        /// whether the input was used to perform a command
        /// </summary>
        public bool used;

        public InputData(Inputs input)
        {
            this.input = input;
            holdTime = 1;
            used = false;
        }

        /// <summary>
        /// checks whether the input is in an executable state
        /// </summary>
        /// <param name="holdTime">the amount of time the input must be held</param>
        /// <param name="allowExcessHold">determines whether the input can be held longer than the required hold time</param>
        /// <returns></returns>
        public bool CanExecute(int holdTime, bool allowExcessHold)
        {
            if (allowExcessHold)
            {
                return this.holdTime >= holdTime && !used;
            }

            return this.holdTime == holdTime && !used;
        }
    }
}
