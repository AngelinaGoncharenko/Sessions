namespace SF2022User106Lib
{
    public class Calculations
    {
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            if (startTimes.Length < durations.Length)
                throw new ArgumentOutOfRangeException("Массив startTimes не может быть меньше массива durations");

            if (beginWorkingTime >= endWorkingTime)
                throw new ArgumentOutOfRangeException("Время начала рабочего дня не может превышать время окончания рабочего дня");

            if (consultationTime <= 0)
                throw new ArgumentOutOfRangeException("Время консультации не может быть меньше или равно нулю");

            foreach (var duration in durations)
            {
                if (duration <= 0)
                    throw new ArgumentOutOfRangeException("Элементы массива durations не могут быть меньше или равны нулю");
            }

            if (durations.Length < startTimes.Length)
                throw new ArgumentOutOfRangeException("Массив durations не может быть меньше массива startTimes");

            for (var i = 0; i < startTimes.Length - 1; i++)
            {
                if (startTimes[i] + TimeSpan.FromMinutes(durations[i]) > startTimes[i + 1])
                    throw new ArgumentOutOfRangeException("Элементы массива startTimes должны быть расположены возрастанию, а так же продолжительность консультации не должна забирать время другой консультации");
            }

            if (beginWorkingTime.Days != endWorkingTime.Days)
                throw new ArgumentOutOfRangeException("День окончания и начала рабочего дня должен совпадать");

            if (startTimes[0] < beginWorkingTime)
                throw new ArgumentOutOfRangeException("startTimes не должен содержать элементов, значение которых меньше beginWorkingTime");

            if (startTimes[startTimes.Length - 1] + TimeSpan.FromMinutes(durations[startTimes.Length - 1]) > endWorkingTime)
                throw new ArgumentOutOfRangeException("startTimes не должен содержать элементов, значение которых больше endWorkingTime");

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
                    continue;
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
            return string.Format("{0:00}:{1:00} - {2:00}:{3:00}", startTime.Hours, startTime.Minutes, endTime.Hours, endTime.Minutes);
        }
    }
}