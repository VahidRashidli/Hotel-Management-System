using System;
using System.Collections.Generic;


namespace Hotel_Management_System.Models
{
    class Booking : Repository.Services<Booking>
    {
        public int Id { get; set; }
        public DateTime StartDate;
        public DateTime EndDate;
        public bool IsCancelled=false;
        public Customer customer { get; set; }
        public Room room { get; set; }
        public Booking(DateTime StartDate, DateTime EndDate,Customer customer,Room room)
        {
            this.Id = Database.Database.bookings.Count + 1;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.customer = customer;
            this.room = room;


        }
    }
}
