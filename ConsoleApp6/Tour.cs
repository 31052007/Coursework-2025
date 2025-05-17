using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Tour
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
            return new Tour(int.Parse(parts[0]), parts[1], DateTime.Parse(parts[2]), DateTime.Parse(parts[3]), decimal.Parse(parts[4]));
        }

        public override string ToString()
        {
            return $"{Id};{City};{StartDate:yyyy-MM-dd};{EndDate:yyyy-MM-dd};{TicketPrice}";
        }
    }
}
