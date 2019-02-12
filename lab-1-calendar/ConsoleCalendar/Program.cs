using System;
using Nager.Date;
using System.Text;
using System.Linq;

namespace ConsoleCalendar {
    class Program {
        private static int _year;
        private static int _month;
        private static readonly int[,] calendar = new int[6, 7];
        
        static void Main(string[] args) {
            Console.WriteLine("Enter the date: ");
            var dateString = Console.ReadLine();
            if (!DateTime.TryParse(dateString, out var dateValue)) {
                Console.WriteLine("Unable to parse '{0}'.", dateString);
                Environment.Exit(1);
            }
            _month = dateValue.Month;
            _year = dateValue.Year;
            
            WriteHeader();
            ConstructCalendar();
            WriteCalendar();
            CountWorkingDays();
        }

        private static void WriteHeader() {
            Console.WriteLine("Mo Tu We Th Fr Sa Su");
        }

        private static void ConstructCalendar() {
            var firstDayOfMonth = new DateTime(_year, _month, 1);
            var daysInMonth = DateTime.DaysInMonth(_year, _month);
            var currentDay = 1;
            
            var i = 0;
            var j = (int)firstDayOfMonth.DayOfWeek - 1;
            while (currentDay <= daysInMonth) {
                calendar[i, j] = currentDay;
                currentDay++;
                j = (j + 1) % calendar.GetLength(1);
                if (j == 0) i++;
            }
        }

        private static void WriteCalendar() {
            var line = new StringBuilder();
            for (var i = 0; i < calendar.GetLength(0); i++) {
                for (var j = 0; j < calendar.GetLength(1); j++) {
                    if (calendar[i, j] == 0) {
                        line.Append("   ");
                        continue;
                    }
                    if (calendar[i, j] < 10) line.Append(" ");
                    line.Append(calendar[i, j]).Append(" ");
                }
                Console.WriteLine(line);
                line.Clear();
            }
        }

        private static void CountWorkingDays() {
            var workingDaysCount = Enumerable
                .Range(1, DateTime.DaysInMonth(_year, _month))
                .Select(day => new DateTime(_year, _month, day))
                .Count(IsWorkingDay);
            Console.WriteLine("Working days: " + workingDaysCount);
        }

        private static bool IsWorkingDay(DateTime dateTime) {
            return    !DateSystem.IsPublicHoliday(dateTime, CountryCode.RU) 
                   && !DateSystem.IsWeekend(dateTime, CountryCode.RU);
        }
    }
}
