using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2025._1
{
    class Program
    {
        static void Main(string[] args)
        {
            MusicManager manager = new MusicManager();
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить группу");
                Console.WriteLine("2. Добавить песню в группу");
                Console.WriteLine("3. Удалить песню из группы");
                Console.WriteLine("4. Добавить гастроли");
                Console.WriteLine("5. Показать песни певца");
                Console.WriteLine("6. Показать группы композитора");
                Console.WriteLine("7. Инфо о песне по названию");
                Console.WriteLine("8. Репертуар самой популярной группы");
                Console.WriteLine("9. Место и длительность гастролей группы");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": manager.AddBandFromInput(); break;
                    case "2": manager.AddSongToBandFromInput(); break;
                    case "3": manager.RemoveSongFromBandFromInput(); break;
                    case "4": manager.AddTourFromInput(); break;
                    case "5": manager.PrintSongsBySinger(); break;
                    case "6": manager.PrintBandsByComposer(); break;
                    case "7": manager.PrintSongInfo(); break;
                    case "8": manager.PrintTopBandRepertoire(); break;
                    case "9": manager.PrintTourInfo(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор"); break;
                }
            }
             Console.ReadKey();
        }
    }
}
