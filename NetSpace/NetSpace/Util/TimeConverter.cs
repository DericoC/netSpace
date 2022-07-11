using System;
namespace NetSpace.Util
{
    public class TimeConverter
    {
        public TimeConverter()
        {
        }

        public TimeSpan getTimeFromMinutes(double value)
        {
            double decimalValue = 0.0;
            if (value < 0)
            {
                return TimeSpan.Zero;
            }

            double dividedNum = value / 60;
            double flooredValue = Math.Floor(dividedNum);

            if (HasDecimals(dividedNum))
            {
                decimalValue = dividedNum - flooredValue;
            }

            return new TimeSpan(getHourInt(value), getMinuteInt(decimalValue), 0);
        }

        bool HasDecimals(double d)
        {
            return (d - (int)d != 0);
        }

        private static int getHourInt(double flooredValue)
        {
            return (int)(flooredValue / 60);
        }

        private static int getMinuteInt(double decimalValue)
        {
            return (int)(decimalValue * 60);
        }
    }
}

