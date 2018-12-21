using System;
using System.Collections.Generic;
using System.Text;
using RaidCalculator.Interfaces;

namespace RaidCalculator.Services
{
    public class Calculator : ICalculator
    {
        public DateTime GetEndTimeFromRemainingTime(TimeSpan remainingTime)
        {
            return DateTime.Now + remainingTime;
        }

        public DateTime GetStartTimeFromTimeUntilStart(TimeSpan timeUntilStart)
        {
            return DateTime.Now + timeUntilStart;
        }
    }
}
