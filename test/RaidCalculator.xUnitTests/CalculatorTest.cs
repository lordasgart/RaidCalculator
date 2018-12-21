using System;
using FluentAssertions;
using RaidCalculator.Services;
using Xunit;

namespace RaidCalculator.xUnitTests
{
    public class CalculatorTest
    {
        [Theory]
        [InlineData(0,8,26)]
        public void GetEndTimeFromRemainingTimeTest(int hours, int minutes, int seconds)
        {
            TimeSpan durationUntilEnd = new TimeSpan(hours, minutes, seconds);
            Calculator calculator = new Calculator();

            var endTime = calculator.GetEndTimeFromRemainingTime(durationUntilEnd);

            endTime.Should().BeAfter(DateTime.Now);
        }

        [Theory]
        [InlineData(0, 8, 26)]
        public void GetStartTimeFromTimeUntilStartTest(int hours, int minutes, int seconds)
        {
            TimeSpan durationUntilStart = new TimeSpan(hours, minutes, seconds);
            Calculator calculator = new Calculator();

            var startTime = calculator.GetStartTimeFromTimeUntilStart(durationUntilStart);

            startTime.Should().BeAfter(DateTime.Now);
        }
    }
}
