using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2025._1
{
    class MusicManager
    {
        private List<Band> bands = new List<Band>();
        private int nextBandKey = 1;
        private int nextSongKey = 1;
        private int nextTourKey = 1;

        public void AddBandFromInput()
        {
            Console.Write("Название группы: ");
            string name = Console.ReadLine();
            Console.Write("Год создания: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Страна: ");
            string country = Console.ReadLine();
            Console.Write("Позиция в хит-параде: ");
            int chart = int.Parse(Console.ReadLine());

            bands.Add(new Band
            {
                Key = nextBandKey++,
                Name = name,
                YearFormed = year,
                Country = country,
                HitChartPosition = chart
            });

            Console.WriteLine("Группа добавлена.");
        }

        public void AddSongToBandFromInput()
        {
            Console.Write("Название группы: ");
            string bandName = Console.ReadLine();
            var band = bands.FirstOrDefault(b => b.Name == bandName);
            if (band == null)
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            Console.Write("Название песни: ");
            string title = Console.ReadLine();
            Console.Write("Композитор: ");
            string composer = Console.ReadLine();
            Console.Write("Автор текста: ");
            string lyricist = Console.ReadLine();
            Console.Write("Год создания: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Исполнитель: ");
            string singer = Console.ReadLine();

            band.Repertoire.Add(new Song
            {
                Key = nextSongKey++,
                Title = title,
                Composer = composer,
                Lyricist = lyricist,
                YearCreated = year,
                Singer = singer
            });

            Console.WriteLine("Песня добавлена.");
        }

        public void RemoveSongFromBandFromInput()
        {
            Console.Write("Название группы: ");
            string bandName = Console.ReadLine();
            Console.Write("Название песни: ");
            string songTitle = Console.ReadLine();

            var band = bands.FirstOrDefault(b => b.Name == bandName);
            if (band == null)
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            int removed = band.Repertoire.RemoveAll(s => s.Title == songTitle);
            Console.WriteLine(removed > 0 ? "Песня удалена." : "Песня не найдена.");
        }

        public void AddTourFromInput()
        {
            Console.Write("Название группы: ");
            string bandName = Console.ReadLine();
            var band = bands.FirstOrDefault(b => b.Name == bandName);
            if (band == null)
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            Console.Write("Название гастрольной программы: ");
            string program = Console.ReadLine();
            Console.Write("Город: ");
            string city = Console.ReadLine();
            Console.Write("Дата начала (yyyy-MM-dd): ");
            DateTime start = DateTime.Parse(Console.ReadLine());
            Console.Write("Дата конца (yyyy-MM-dd): ");
            DateTime end = DateTime.Parse(Console.ReadLine());
            Console.Write("Средняя цена билета: ");
            double price = double.Parse(Console.ReadLine());

            band.Tours.Add(new Tour
            {
                Key = nextTourKey++,
                ProgramTitle = program,
                City = city,
                StartDate = start,
                EndDate = end,
                TicketPrice = price
            });

            Console.WriteLine("Гастроли добавлены.");
        }

        public void PrintSongsBySinger()
        {
            Console.Write("Введите имя певца: ");
            string singer = Console.ReadLine();
            var songs = bands.SelectMany(b => b.Repertoire)
                             .Where(s => s.Singer.Equals(singer, StringComparison.OrdinalIgnoreCase));

            foreach (var song in songs)
                Console.WriteLine($"[{song.Key}] {song.Title}");
        }

        public void PrintBandsByComposer()
        {
            Console.Write("Введите имя композитора: ");
            string composer = Console.ReadLine();
            var matched = bands
                .Where(b => b.Repertoire.Any(s => s.Composer.Equals(composer, StringComparison.OrdinalIgnoreCase)))
                .Select(b => b.Name)
                .Distinct();

            foreach (var name in matched)
                Console.WriteLine(name);
        }

        public void PrintSongInfo()
        {
            Console.Write("Введите название песни: ");
            string title = Console.ReadLine();

            foreach (var band in bands)
            {
                var song = band.Repertoire.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
                if (song != null)
                {
                    Console.WriteLine($"[{song.Key}] Композитор: {song.Composer}, Автор текста: {song.Lyricist}, Год: {song.YearCreated}, Группа: {band.Name}");
                    return;
                }
            }

            Console.WriteLine("Песня не найдена.");
        }

        public void PrintTopBandRepertoire()
        {
            var topBand = bands.OrderBy(b => b.HitChartPosition).FirstOrDefault();
            if (topBand != null)
            {
                Console.WriteLine($"Репертуар группы {topBand.Name}:");
                foreach (var song in topBand.Repertoire)
                    Console.WriteLine($"[{song.Key}] {song.Title}");
            }
            else
            {
                Console.WriteLine("Нет данных о группах.");
            }
        }

        public void PrintTourInfo()
        {
            Console.Write("Введите название группы: ");
            string bandName = Console.ReadLine();
            var band = bands.FirstOrDefault(b => b.Name == bandName);
            if (band == null)
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            foreach (var tour in band.Tours)
            {
                int duration = (tour.EndDate - tour.StartDate).Days;
                Console.WriteLine($"[{tour.Key}] Город: {tour.City}, Начало: {tour.StartDate:yyyy-MM-dd}, Конец: {tour.EndDate:yyyy-MM-dd}, Длительность: {duration} дней");
            }
        }
    }
}
