using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2025._1
{
    class Band
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public int YearFormed { get; set; }
        public string Country { get; set; }
        public int HitChartPosition { get; set; }
        public List<Song> Repertoire { get; set; } = new List<Song>();
        public List<Tour> Tours { get; set; } = new List<Tour>();
    }
}
