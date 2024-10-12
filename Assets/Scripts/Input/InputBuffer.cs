namespace MotionMaster.Input
{
    /// <summary>
    /// a custom data structure specifically designed to hold the players inputs for a period of time
    /// </summary>
    [System.Serializable]
    public class InputBuffer
    {
        private InputData[] buffer;

        public InputData[] Buffer { get { return buffer; } }

        public InputBuffer(int size) => buffer = new InputData[size];

        /// <summary>
        /// updates the items in the buffer by shifting them when necessary
        /// </summary>
        /// <param name="input"></param>
        public void Update(Inputs input)
        {
            if (IsNewInput(input))
            {
                InputData id = new InputData(input);
                Shift(buffer.Length - 1);
                buffer[buffer.Length - 1] = id;
            }
            else
            {
                Shift(buffer.Length - 2);
                buffer[buffer.Length - 1].holdTime += 1;
            }
        }

        /// <summary>
        /// shifts the items in the buffer up by 1 index
        /// </summary>
        /// <param name="stopIndex"></param>
        private void Shift(int stopIndex)
        {
            for(int i = 0; i <= stopIndex; i++)
            {
                if(i < stopIndex)
                {
                    buffer[i] = buffer[i + 1];
                    continue;
                }

                buffer[i] = default;
            }
        }

        /// <summary>
        /// determines whether the specified input is a new input. 
        /// The last item in the buffer is designated specifically for the player's current input
        /// </summary>
        /// <param name="input">the input being checked</param>
        /// <returns></returns>
        private bool IsNewInput(Inputs input) => input != buffer[buffer.Length - 1].input;
    }
}
