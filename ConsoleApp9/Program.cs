using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Song
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
        return new Song(
            int.Parse(parts[0]),
            parts[1],
            parts[2],
            parts[3],
            int.Parse(parts[4]),
            parts[5]
        );
    }

    public override string ToString()
    {
        return $"{Id};{Title};{Composer};{Lyricist};{Year};{Singer}";
    }
}

public class Tour
{
    public int Id { get; set; }
    public string City { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TicketPrice { get; set; }

    public Tour(int id, string city, DateTime startDate, DateTime endDate, decimal ticketPrice)
    {
        Id = id;
        City = city;
        StartDate = startDate;
        EndDate = endDate;
        TicketPrice = ticketPrice;
    }

    public static Tour FromFileString(string line)
    {
        var parts = line.Split(';');
        return new Tour(
            int.Parse(parts[0]),
            parts[1],
            DateTime.Parse(parts[2]),
            DateTime.Parse(parts[3]),
            decimal.Parse(parts[4])
        );
    }

    public override string ToString()
    {
        return $"{Id};{City};{StartDate:yyyy-MM-dd};{EndDate:yyyy-MM-dd};{TicketPrice}";
    }
}

public class Band
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int YearFormed { get; set; }
    public string Country { get; set; }
    public int ChartPosition { get; set; }
    public List<Song> Repertoire { get; set; }
    public List<Tour> Tours { get; set; }

    public Band(int id, string name, int yearFormed, string country, int chartPosition)
    {
        Id = id;
        Name = name;
        YearFormed = yearFormed;
        Country = country;
        ChartPosition = chartPosition;
        Repertoire = new List<Song>();
        Tours = new List<Tour>();
    }

    public static Band FromFileString(string line)
    {
        var parts = line.Split(';');
        return new Band(
            int.Parse(parts[0]),
            parts[1],
            int.Parse(parts[2]),
            parts[3],
            int.Parse(parts[4])
        );
    }

    public override string ToString()
    {
        return $"{Id};{Name};{YearFormed};{Country};{ChartPosition}";
    }
}

public class Program
{
    static List<Band> bands = new List<Band>();
    //static string bandFile = "C:\\Users\\Николай Охлоповский\\OneDrive\\Рабочий стол\\С#\\ConsoleApp9\\txt\\Band.txt";
    //static string songFile = "C:\\Users\\Николай Охлоповский\\OneDrive\\Рабочий стол\\С#\\ConsoleApp9\\txt\\Song.txt";
    //static string tourFile = "C:\\Users\\Николай Охлоповский\\OneDrive\\Рабочий стол\\С#\\ConsoleApp9\\txt\\Tour.txt";

    //static string bandFile = "C:\\Users\\kab-35-17\\Desktop\\ConsoleApp9\\txt\\Band.txt";
    //static string songFile = "C:\\Users\\kab-35-17\\Desktop\\ConsoleApp9\\txt\\Song.txt";
    //static string tourFile = "C:\\Users\\kab-35-17\\Desktop\\ConsoleApp9\\txt\\Tour.txt";

    public static void Main(string[] args)
    {
        LoadData();  // Загружаем данные из файлов

        // Главное меню
        while (true)
        {
            Console.WriteLine("\nМенеджер музыкальных групп");
            Console.WriteLine("1. Добавить новую группу");
            Console.WriteLine("2. Добавить песню в группу");
            Console.WriteLine("3. Добавить гастролей для группы");
            Console.WriteLine("4. Удалить песню из репертуара");
            Console.WriteLine("5. Найти песни, исполненные на гастролях группы");
            Console.WriteLine("6. Найти группы, исполняющие песни заданного композитора");
            Console.WriteLine("7. Завершить программу");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddBand();
                    break;
                case "2":
                    AddSongToBand();
                    break;
                case "3":
                    AddTourToBand();
                    break;
                case "4":
                    RemoveSongFromBand();
                    break;
                case "5":
                    FindSongsPerformedOnTour();
                    break;
                case "6":
                    FindBandsByComposer();
                    break;
                case "7":
                    SaveData(); // Сохраняем данные перед завершением
                    Console.WriteLine("Программа завершена.");
                    return; // Завершаем программу
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
          Console.ReadKey();
    }

    // Методы добавления и работы с данными (как в предыдущем коде) остаются неизменными

    static void AddBand()
    {
        Console.WriteLine("Введите название группы:");
        string name = Console.ReadLine();
        Console.WriteLine("Введите год создания группы:");
        int yearFormed = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите страну группы:");
        string country = Console.ReadLine();
        Console.WriteLine("Введите положение в хит-параде:");
        int chartPosition = int.Parse(Console.ReadLine());

        var newBand = new Band(bands.Count + 1, name, yearFormed, country, chartPosition);
        bands.Add(newBand);
        Console.WriteLine($"Группа {name} добавлена.");
    }

    static void AddSongToBand()
    {
        Console.WriteLine("Введите ID группы:");
        int bandId = int.Parse(Console.ReadLine());
        var band = bands.FirstOrDefault(b => b.Id == bandId);
        if (band != null)
        {
            Console.WriteLine("Введите название песни:");
            string title = Console.ReadLine();
            Console.WriteLine("Введите композитора:");
            string composer = Console.ReadLine();
            Console.WriteLine("Введите автора текста:");
            string lyricist = Console.ReadLine();
            Console.WriteLine("Введите год создания:");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите певца:");
            string singer = Console.ReadLine();

            var song = new Song(band.Repertoire.Count + 1, title, composer, lyricist, year, singer);
            band.Repertoire.Add(song);
            Console.WriteLine($"Песня '{title}' добавлена в репертуар группы {band.Name}.");
        }
        else
        {
            Console.WriteLine("Группа с таким ID не найдена.");
        }
    }

    static void AddTourToBand()
    {
        Console.WriteLine("Введите ID группы:");
        int bandId = int.Parse(Console.ReadLine());
        var band = bands.FirstOrDefault(b => b.Id == bandId);
        if (band != null)
        {
            Console.WriteLine("Введите город гастролей:");
            string city = Console.ReadLine();
            Console.WriteLine("Введите дату начала гастролей (формат: yyyy-MM-dd):");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату окончания гастролей (формат: yyyy-MM-dd):");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите среднюю цену билета:");
            decimal ticketPrice = decimal.Parse(Console.ReadLine());

            var tour = new Tour(band.Tours.Count + 1, city, startDate, endDate, ticketPrice);
            band.Tours.Add(tour);
            Console.WriteLine($"Гастроли в {city} добавлены для группы {band.Name}.");
        }
        else
        {
            Console.WriteLine("Группа с таким ID не найдена.");
        }
    }

    static void RemoveSongFromBand()
    {
        Console.WriteLine("Введите название песни для удаления:");
        string title = Console.ReadLine();
        Console.WriteLine("Введите ID группы:");
        int bandId = int.Parse(Console.ReadLine());

        var band = bands.FirstOrDefault(b => b.Id == bandId);
        if (band != null)
        {
            var songToRemove = band.Repertoire.FirstOrDefault(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (songToRemove != null)
            {
                band.Repertoire.Remove(songToRemove);
                Console.WriteLine($"Песня '{title}' удалена из репертуара группы {band.Name}.");
            }
            else
            {
                Console.WriteLine($"Песня '{title}' не найдена в репертуаре группы.");
            }
        }
        else
        {
            Console.WriteLine("Группа с таким ID не найдена.");
        }
    }

    static void FindSongsPerformedOnTour()
    {
        Console.WriteLine("Введите название группы:");
        string bandName = Console.ReadLine();
        var band = bands.FirstOrDefault(b => b.Name.Equals(bandName, StringComparison.OrdinalIgnoreCase));
        if (band != null)
        {
            Console.WriteLine($"Песни, исполненные на гастролях группы {band.Name}:");
            foreach (var song in band.Repertoire)
            {
                Console.WriteLine(song.Title);
            }
        }
        else
        {
            Console.WriteLine("Группа с таким названием не найдена.");
        }
    }

    static void FindBandsByComposer()
    {
        Console.WriteLine("Введите имя композитора:");
        string composer = Console.ReadLine();
        var bandsByComposer = bands.Where(b => b.Repertoire.Any(s => s.Composer.Equals(composer, StringComparison.OrdinalIgnoreCase))).ToList();

        Console.WriteLine($"Группы, исполняющие песни композитора {composer}:");
        foreach (var band in bandsByComposer)
        {
            Console.WriteLine(band.Name);
        }
    }

    // Метод для загрузки данных из файлов
    static void LoadData()
    {
        if (File.Exists(bandFile))
        {
            var bandLines = File.ReadAllLines(bandFile);
            foreach (var line in bandLines)
            {
                bands.Add(Band.FromFileString(line));
            }
        }

        if (File.Exists(songFile))
        {
            var songLines = File.ReadAllLines(songFile);
            foreach (var line in songLines)
            {
                var song = Song.FromFileString(line);
                var band = bands.FirstOrDefault(b => b.Id == song.Id);
                if (band != null)
                {
                    band.Repertoire.Add(song);
                }
            }
        }

        if (File.Exists(tourFile))
        {
            var tourLines = File.ReadAllLines(tourFile);
            foreach (var line in tourLines)
            {
                var tour = Tour.FromFileString(line);
                var band = bands.FirstOrDefault(b => b.Id == tour.Id);
                if (band != null)
                {
                    band.Tours.Add(tour);
                }
            }
        }
    }

    // Метод для сохранения данных в файлы
    static void SaveData()
    {
        File.WriteAllLines(bandFile, bands.Select(b => b.ToString()));
        var songs = bands.SelectMany(b => b.Repertoire).Select(s => s.ToString());
        File.WriteAllLines(songFile, songs);
        var tours = bands.SelectMany(b => b.Tours).Select(t => t.ToString());
        File.WriteAllLines(tourFile, tours);
    }
}

