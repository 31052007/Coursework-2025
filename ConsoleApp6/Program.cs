using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static List<Group> groups = new List<Group>();

        static string groupFile = @"C:\Users\Николай Охлоповский\OneDrive\Рабочий стол\С#\ConsoleApp6\txt\Group.txt";
        static string songFile = @"C:\Users\Николай Охлоповский\OneDrive\Рабочий стол\С#\ConsoleApp6\txt\Song.txt";
        static string tourFile = @"C:\Users\Николай Охлоповский\OneDrive\Рабочий стол\С#\ConsoleApp6\txt\Tour.txt";

        static void Main(string[] args)
        {
            LoadData();

            while (true)
            {
                Console.WriteLine("\n.....МЕНЮ.....");
                Console.WriteLine("1. Добавить группу");
                Console.WriteLine("2. Добавить песню");
                Console.WriteLine("3. Добавить гастроли");
                Console.WriteLine("4. Удалить песню");
                Console.WriteLine("5. Песни, исполненные на гастролях");
                Console.WriteLine("6. Группы по композитору");
                Console.WriteLine("7. Данные о песне и группе");
                Console.WriteLine("8. Репертуар самой популярной группы");
                Console.WriteLine("9. Место и продолжительность гастролей");
                Console.WriteLine("10. Песни певца");
                Console.WriteLine("11. Выход");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddGroup(); break;
                    case "2": AddSong(); break;
                    case "3": AddTour(); break;
                    case "4": RemoveSong(); break;
                    case "5": SongsOnTour(); break;
                    case "6": GroupsByComposer(); break;
                    case "7": SongInfo(); break;
                    case "8": MostPopularGroupSongs(); break;
                    case "9": TourDetails(); break;
                    case "10": SongsBySinger(); break;
                    case "11":
                        SaveData();
                        return;
                    default: Console.WriteLine("Неверный выбор."); break;
                }
            }
            Console.ReadKey();
        }

        static void AddGroup()
        {
            Console.WriteLine("Название:");
            var name = Console.ReadLine();
            Console.WriteLine("Год:");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("Страна:");
            var country = Console.ReadLine();
            Console.WriteLine("Позиция в чарте:");
            var chart = int.Parse(Console.ReadLine());

            groups.Add(new Group(groups.Count + 1, name, year, country, chart));
            Console.WriteLine("Группа добавлена.");
        }

        static void AddSong()
        {
            Console.WriteLine("ID группы:");
            var id = int.Parse(Console.ReadLine());
            var group = groups.FirstOrDefault(g => g.Id == id);
            if (group == null) { Console.WriteLine("Не найдено."); return; }

            Console.WriteLine("Название песни:");
            var title = Console.ReadLine();
            Console.WriteLine("Композитор:");
            var comp = Console.ReadLine();
            Console.WriteLine("Автор текста:");
            var lyr = Console.ReadLine();
            Console.WriteLine("Год:");
            var year = int.Parse(Console.ReadLine());
            Console.WriteLine("Певец:");
            var singer = Console.ReadLine();

            group.Repertoire.Add(new Song(group.Repertoire.Count + 1, title, comp, lyr, year, singer));
            Console.WriteLine("Песня добавлена.");
        }

        static void AddTour()
        {
            Console.WriteLine("ID группы:");
            var id = int.Parse(Console.ReadLine());
            var group = groups.FirstOrDefault(g => g.Id == id);
            if (group == null) { Console.WriteLine("Не найдено."); return; }

            Console.WriteLine("Город:");
            var city = Console.ReadLine();
            Console.WriteLine("Дата начала (yyyy-MM-dd):");
            var start = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Дата окончания:");
            var end = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Цена билета:");
            var price = decimal.Parse(Console.ReadLine());

            group.Tours.Add(new Tour(group.Tours.Count + 1, city, start, end, price));
            Console.WriteLine("Гастроли добавлены.");
        }

        static void RemoveSong()
        {
            Console.WriteLine("ID группы:");
            var id = int.Parse(Console.ReadLine());
            var group = groups.FirstOrDefault(g => g.Id == id);
            if (group == null) return;

            Console.WriteLine("Название песни:");
            var title = Console.ReadLine();

            var song = group.Repertoire.FirstOrDefault(s => s.Title == title);
            if (song != null)
            {
                group.Repertoire.Remove(song);
                Console.WriteLine("Песня удалена.");
            }
        }

        static void SongsOnTour()
        {
            Console.WriteLine("Название группы:");
            var name = Console.ReadLine();
            var group = groups.FirstOrDefault(g => g.Name == name);
            if (group != null)
                foreach (var s in group.Repertoire) Console.WriteLine(s.Title);
        }

        static void GroupsByComposer()
        {
            Console.WriteLine("Имя композитора:");
            var comp = Console.ReadLine();
            var result = groups.Where(g => g.Repertoire.Any(s => s.Composer == comp));
            foreach (var g in result) Console.WriteLine(g.Name);
        }

        static void SongInfo()
        {
            Console.WriteLine("Название песни:");
            var title = Console.ReadLine();
            foreach (var g in groups)
            {
                var song = g.Repertoire.FirstOrDefault(s => s.Title == title);
                if (song != null)
                {
                    Console.WriteLine($"Композитор: {song.Composer}, Автор текста: {song.Lyricist}, Год: {song.Year}, Группа: {g.Name}");
                    return;
                }
            }
            Console.WriteLine("Песня не найдена.");
        }

        static void MostPopularGroupSongs()
        {
            var topGroup = groups.OrderBy(g => g.ChartPosition).FirstOrDefault();
            if (topGroup != null)
            {
                Console.WriteLine($"Самая популярная группа: {topGroup.Name}");
                foreach (var song in topGroup.Repertoire) Console.WriteLine(song.Title);
            }
        }

        static void TourDetails()
        {
            Console.WriteLine("Название группы:");
            var name = Console.ReadLine();
            var group = groups.FirstOrDefault(g => g.Name == name);
            if (group == null) return;

            foreach (var t in group.Tours)
            {
                var days = (t.EndDate - t.StartDate).Days;
                Console.WriteLine($"Город: {t.City}, Длительность: {days} дней");
            }
        }

        static void SongsBySinger()
        {
            Console.WriteLine("Имя певца:");
            var singer = Console.ReadLine();
            foreach (var g in groups)
                foreach (var s in g.Repertoire.Where(s => s.Singer == singer))
                    Console.WriteLine($"Песня: {s.Title}, Группа: {g.Name}");
        }

        static void LoadData()
        {
            if (File.Exists(groupFile))
                foreach (var line in File.ReadAllLines(groupFile))
                    groups.Add(Group.FromFileString(line));

            if (File.Exists(songFile))
                foreach (var line in File.ReadAllLines(songFile))
                {
                    var song = Song.FromFileString(line);
                    var group = groups.FirstOrDefault(g => g.Id == song.Id);
                    if (group != null) group.Repertoire.Add(song);
                }

            if (File.Exists(tourFile))
                foreach (var line in File.ReadAllLines(tourFile))
                {
                    var tour = Tour.FromFileString(line);
                    var group = groups.FirstOrDefault(g => g.Id == tour.Id);
                    if (group != null) group.Tours.Add(tour);
                }
        }

        static void SaveData()
        {
            File.WriteAllLines(groupFile, groups.Select(g => g.ToString()));
            File.WriteAllLines(songFile, groups.SelectMany(g => g.Repertoire).Select(s => s.ToString()));
            File.WriteAllLines(tourFile, groups.SelectMany(g => g.Tours).Select(t => t.ToString()));
        }
    }
}
