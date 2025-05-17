using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearFormed { get; set; }
        public string Country { get; set; }
        public int ChartPosition { get; set; }
        public List<Song> Repertoire { get; set; }
        public List<Tour> Tours { get; set; }

        public Group(int id, string name, int yearFormed, string country, int chartPosition)
        {
            Id = id;
            Name = name;
            YearFormed = yearFormed;
            Country = country;
            ChartPosition = chartPosition;
            Repertoire = new List<Song>();
            Tours = new List<Tour>();
        }

        public static Group FromFileString(string line)
        {
            var parts = line.Split(';');
            return new Group(
                int.Parse(parts[0]), parts[1], int.Parse(parts[2]), parts[3], int.Parse(parts[4]));
        }

        public override string ToString()
        {
            return $"{Id};{Name};{YearFormed};{Country};{ChartPosition}";
        }
    }
}
