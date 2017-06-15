using System;
using DoubleBuffer;

namespace Part12
{
    public class DayNightCycle
    {
        // Duration of day, in frames.
        private static int dayDuration = 99;

        // Counter to keep track of how close we are to turn to night or day.
        private int currentFrame = 0;

        /// <summary>
        /// Rests the frame counter to zero.
        /// </summary>
        public void ResetFrameCounter()
        {
            currentFrame = 0;
        }

        /// <summary>
        /// Used to let the class keep track of the current frame.
        /// </summary>
        public void UpdateFrameCounter()
        {
            currentFrame++;
        }

        /// <summary>
        /// Returns true if it is night, false if still day.
        /// </summary>
         /// <returns></returns>
        public bool IsNight()
        {
            if (dayDuration > 0)
                return (int)((double)currentFrame / (double)dayDuration) % 2 == 1;
            else
                return false;
        }

        /// <summary>
        /// Changes the background color in case it is night.
        /// </summary>
        /// <param name="graphics">The double buffer for graphics</param>
        /// <param name="dayColor">The color for daytime</param>
        /// <param name="nightColor">The color for nighttime</param>
        public void UpdateBackgroundColor(DoubleGraphicsBuffer graphics, ConsoleColor dayColor, ConsoleColor nightColor)
        {
            graphics.SetBackgroundColor(IsNight() ? nightColor : dayColor);
        }

    }
}
