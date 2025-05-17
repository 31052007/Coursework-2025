using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public string Lyricist { get; set; }
        public int Year { get; set; }
        public string Singer { get; set; }

        public Song(int id, string title, string composer, string lyricist, int year, string singer)
        {
            Id = id;
            Title = title;
            Composer = composer;
            Lyricist = lyricist;
            Year = year;
            Singer = singer;
        }

        public static Song FromFileString(string line)
        {
            var parts = line.Split(';');
            return new Song(int.Parse(parts[0]), parts[1], parts[2], parts[3], int.Parse(parts[4]), parts[5]);
        }

        public override string ToString()
        {
            return $"{Id};{Title};{Composer};{Lyricist};{Year};{Singer}";
        }
    }
}
