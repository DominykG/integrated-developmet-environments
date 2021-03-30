using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace LD2
{
    class Tea
    {
        private string name;
        private string manufacturer;
        private DateTime productionDate;
        private float price;
        private int daysUntilExpiry;

        public string Name { get => name; set => name = value; }
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        public DateTime ProductionDate { get => productionDate; set => productionDate = value; }
        public float Price { get => price; set => price = value; }
        public int DaysUntilExpiry { get => daysUntilExpiry; set => daysUntilExpiry = value; }

        public Tea() {}

        public Tea(string name, string manufacturer, DateTime productionDate, float price, int daysUntilExpiry)
        {
            Name = name;
            Manufacturer = manufacturer;
            ProductionDate = productionDate;
            Price = price;
            DaysUntilExpiry = daysUntilExpiry;
        }

        public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Manufacturer)}: {Manufacturer}, " +
                                               $"{nameof(ProductionDate)}: {ProductionDate}, {nameof(Price)}: {Price}, " +
                                               $"{nameof(DaysUntilExpiry)}: {DaysUntilExpiry}";

        public static double AverageDaysUntilExpiry(List<Tea> teaList)
        {
            double averageDays = 0;
            foreach (Tea tea in teaList)
            {
                averageDays += tea.DaysUntilExpiry;
            }
            return averageDays/teaList.Count;
        }

        public static List<Tea> Filter(List<Tea> teaList, DateTime productionDate)
        {
            var filteredTeaList = new List<Tea>();

            teaList.ForEach(tea => {
                if (tea.ProductionDate == productionDate)
                {
                    filteredTeaList.Add(tea);
                }
            });

            return filteredTeaList;
        }

        public static List<Tea> Filter(List<Tea> teaList, string manufacturer, int daysUntilExpiry, float price)
        {
            var filteredTeaList = new List<Tea>();

            teaList.ForEach(tea => {
                if (tea.manufacturer == manufacturer && tea.price == price && tea.daysUntilExpiry == daysUntilExpiry)
                {
                    filteredTeaList.Add(tea);
                }
            });

            return filteredTeaList;
        }

        public static string ToCsvFile(List<Tea> teaList, String fileName)
        {
            var csv = new StringBuilder();

            csv.AppendLine("Name,Manufacturer,ProductionDate,Price,DaysUntilExpiry");

            teaList.ForEach(tea => csv.AppendLine($"{tea.Name},{tea.Manufacturer},{tea.ProductionDate},{tea.Price},{tea.DaysUntilExpiry}"));

            File.WriteAllText(fileName + ".csv", csv.ToString());

            return csv.ToString();
        }

        public static List<Tea> FromCsvFile(string fileName) => File.ReadAllLines(fileName)
                                                                    .Skip(1)
                                                                    .Select(line => line.Split(","))
                                                                    .Select(values => fromCsvString(values))
                                                                    .ToList();
        

        private static Tea fromCsvString(string[] teaCsv) => new Tea(teaCsv[0], 
                                                                        teaCsv[1], 
                                                                        DateTime.Parse(teaCsv[2]), 
                                                                        float.Parse(teaCsv[3]),
                                                                        int.Parse(teaCsv[4]));
    }
}
