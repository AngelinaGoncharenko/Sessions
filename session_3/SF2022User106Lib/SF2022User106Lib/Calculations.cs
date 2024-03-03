namespace SF2022User106Lib
{
    public class Calculations
    {
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            var freeTimes = new List<string>();
            var startFreeTime = beginWorkingTime;
            var busyTimeIndex = 0;

            while (startFreeTime <= endWorkingTime)
            {
                var endFreeTime = startFreeTime + TimeSpan.FromMinutes(consultationTime);
                var freeTime = GetFreeTimeFormat(startFreeTime, endFreeTime);

                if (busyTimeIndex >= startTimes.Length)
                {
                    if (endFreeTime <= endWorkingTime)
                        freeTimes.Add(freeTime);

                    startFreeTime = endFreeTime;
                }

                var startBusyTime = startTimes[busyTimeIndex];

                if (startFreeTime <= startBusyTime && endFreeTime <= startBusyTime)
                {
                    freeTimes.Add(freeTime);
                    startFreeTime = endFreeTime;
                }
                else
                {
                    var busyDuration = durations[busyTimeIndex];
                    var endBusyTime = startBusyTime + TimeSpan.FromMinutes(busyDuration);
                    startFreeTime = endBusyTime;
                    busyTimeIndex += 1;
                }
            }


            return freeTimes.ToArray();
        }

        private static string GetFreeTimeFormat(TimeSpan startTime, TimeSpan endTime)
        {
            return string.Format("{0:00}:{1:00}-{2:00}:{3:00}", startTime.Hours, startTime.Minutes, endTime.Hours, endTime.Minutes);
        }
    }
}