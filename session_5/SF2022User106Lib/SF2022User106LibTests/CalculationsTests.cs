using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022User106Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User106Lib.Tests
{
    [TestClass()]
    public class CalculationsTests
    {
        [TestMethod()]
        public void AvailablePeriods_DefaultArguments_TrueStrings()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0)
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10,
                40
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            var results = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            var desiredResults = new string[]
            {
                "08:00 - 08:30",
                "08:30 - 09:00",
                "09:00 - 09:30",
                "09:30 - 10:00",
                "11:30 - 12:00",
                "12:00 - 12:30",
                "12:30 - 13:00",
                "13:00 - 13:30",
                "13:30 - 14:00",
                "14:00 - 14:30",
                "14:30 - 15:00",
                "15:40 - 16:10",
                "16:10 - 16:40",
                "17:30 - 18:00"
            };

            var resultsString = "";
            foreach (var result in results)
            {
                resultsString += result;
            }

            var desiredResultsString = "";
            foreach (var desiredResult in desiredResults)
            {
                desiredResultsString += desiredResult;
            }

            Assert.AreEqual(desiredResultsString, resultsString);
        }

        [TestMethod()]
        public void AvailablePeriods_StartTimesLessDurations_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10,
                40
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_BeginWorkingTimeLargerEndWorkingTime_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10
            };

            var beginWorkingTime = new TimeSpan(19, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_ConsultationTimeLessOrEqualZero_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = -1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_DurationsElmentLessOrEqualZero_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                -10
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_DurationsLengthLessStartTimesLength_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_StartTimesNotSorted_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10,
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_BeginWorkingTimeDayNotEndWorkingTimeDay_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10,
            };

            var beginWorkingTime = new TimeSpan(1, 8, 0, 0);
            var endWorkingTime = new TimeSpan(2, 18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_StartTimesElementLessBeginWorkingTime_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(7, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10,
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }

        [TestMethod()]
        public void AvailablePeriods_StartTimesElementLasrgerEndWorkingTime_ArgumentOutOfRangeException()
        {
            TimeSpan[] startTimes = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(22, 30, 0),
            };

            int[] durations = new int[]
            {
                60,
                30,
                10,
                10,
            };

            var beginWorkingTime = new TimeSpan(8, 0, 0);
            var endWorkingTime = new TimeSpan(18, 0, 0);

            var consultationTime = 30;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime));
        }
    }
}