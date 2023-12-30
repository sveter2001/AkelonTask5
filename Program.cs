using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticTask1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var allVacationCount = 0;
            var VacationDictionary = new Dictionary<string, List<DateTime>>()
            {
                ["Иванов Иван Иванович"] = new List<DateTime>(),
                ["Петров Петр Петрович"] = new List<DateTime>(),
                ["Юлина Юлия Юлиановна"] = new List<DateTime>(),
                ["Сидоров Сидор Сидорович"] = new List<DateTime>(),
                ["Павлов Павел Павлович"] = new List<DateTime>(),
                ["Георгиев Георг Георгиевич"] = new List<DateTime>()
            };
            var VacationDictionaryShort = new Dictionary<string, List<string>>()
            {
                ["Иванов Иван Иванович"] = new List<string>(),
                ["Петров Петр Петрович"] = new List<string>(),
                ["Юлина Юлия Юлиановна"] = new List<string>(),
                ["Сидоров Сидор Сидорович"] = new List<string>(),
                ["Павлов Павел Павлович"] = new List<string>(),
                ["Георгиев Георг Георгиевич"] = new List<string>()
            };
            var WeekdaysList = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            // Список отпусков сотрудников
            List<DateTime> AllVacationsList = new List<DateTime>();
            List<DateTime> dateList = new List<DateTime>();
            foreach (var VacationList in VacationDictionary)
            {
                Random gen = new Random();
                DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime end = new DateTime(DateTime.Today.Year, 12, 31);

                dateList = VacationList.Value;
                int vacationCount = 28;
                while (vacationCount > 0)
                {
                    int range = (end - start).Days;
                    var startDate = start.AddDays(gen.Next(range));

                    if (WeekdaysList.Contains(startDate.DayOfWeek.ToString()))
                    {
                        var endDate = new DateTime(DateTime.Now.Year, 12, 31);
                        int vacationDuration = 7; // стандартная длительность
                        if (vacationCount > 7)
                        {//упрощенный блок
                            string[] vacationSteps = { "7", "14" };
                            int vacIndex = gen.Next(vacationSteps.Length);
                            vacationDuration = vacationSteps[vacIndex] == "7" ? 7 : 14;
                        }
                        endDate = startDate.AddDays(vacationDuration);

                        // Проверка условий по отпуску
                        bool canCreateVacation = false;
                        bool existStart = false;
                        if (!AllVacationsList.Any(element => element >= startDate && element <= endDate))
                        {
                            if (!AllVacationsList.Any(element => element.AddDays(3) >= startDate && element.AddDays(-3) <= endDate))//измененное условие
                            {
                                existStart = startDate.Month == endDate.Month;//измененное условие
                                if (existStart)//измененное условие
                                    canCreateVacation = true;
                            }
                        }

                        if (canCreateVacation)
                        {
                            for (DateTime dt = startDate; dt < endDate; dt = dt.AddDays(1))
                            {
                                AllVacationsList.Add(dt);
                                dateList.Add(dt);
                            }
                            allVacationCount++;
                            VacationDictionaryShort[VacationList.Key].Add(startDate.ToString("dd.MM.yyyy") +
                                " - " + endDate.ToString("dd.MM.yyyy"));
                            vacationCount -= vacationDuration;
                        }
                    }
                }
            }
            Console.WriteLine("Всего отпусков в этом году: " + $"{allVacationCount}");
            foreach (var VacationList in VacationDictionaryShort)
            {
                Console.WriteLine("Дни отпуска " + VacationList.Key + " : ");
                for (int i = 0; i < VacationList.Value.Count; i++) { Console.WriteLine(VacationList.Value[i]); }
            }
            Console.ReadKey();
        }
    }
}