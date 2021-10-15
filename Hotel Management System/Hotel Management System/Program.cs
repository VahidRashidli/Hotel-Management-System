using System;
using System.Collections.Generic;
using System.Threading;
using Hotel_Management_System.ConsoleMenu;
using Hotel_Management_System.Models;

namespace Hotel_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.Database.AddDefaultRooms();
            Database.Database.AddDefaultUsers();
            string[] options = new string[] { "Login","Create a user" ,"Available room search",
             " Booking", "Booking details", "Check-in", "Check-out",
            "Room information","Customer information","Booking reports(by customer, date, room)","Log out"
            };
            string prompt = "Use arrows to cycle through options. Press Enter to choose.";
            Menu menu1 = new Menu(options, prompt);
            int selectedIndex = 0;
            selectedIndex = menu1.Run(selectedIndex);//User Enter duymesini basana kimi Run metodu dovr edecek.Enter duymesi  
                                                     //basilan kimi secilen option-in indexi selectedIndex deyisenine oturulecek
            User user = null;
            do
            {
                switch (selectedIndex)
                {
                    case 0:
                        if (user == null)
                        {
                            Console.WriteLine("Enter your username.(To remind you, your username is the same as your email)");
                            string username = Console.ReadLine();
                            Console.WriteLine("Enter your password");
                            string password1 = Console.ReadLine();
                            user = Database.Database.users.Find(user => user.Password == password1 && user.UserName == username);
                            if (user != null)
                            {
                                Console.WriteLine("You have logged in successfully");
                                Thread.Sleep(2000);
                                menu1.Prompt += $" Hello {user.Name}!";
                                break;
                            }
                            Console.WriteLine("Incorrect password or username!");
                            Thread.Sleep(2000);


                        }
                        else
                        {
                            Console.WriteLine("You are already logged in!");
                            Thread.Sleep(2000);
                        }
                        break;
                    case 1:
                        Console.WriteLine("Enter the name of the user");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter the password of the user");
                        string password = Console.ReadLine();
                        Console.WriteLine("Enter the email of the user");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter the phone number of the user");
                        string phone = Console.ReadLine();
                        User user1 = new User(name, password, email, phone);
                        Console.WriteLine("A user has been added successfully");
                        Database.Database.users.Add(user1);
                        Thread.Sleep(2000);
                        break;
                    case 2:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Console.WriteLine("Enter first date (From which date). For example, 03.04.2021");
                            DateTime StartDate = Utility.Date();
                            Console.WriteLine("Enter second date (to which date).");
                            DateTime EndDate = Utility.Date();
                            while (StartDate >= EndDate)
                            {
                                Console.WriteLine("Start date cannot be greater than end date!\n");
                                Console.WriteLine("Enter first date (From which date). For example, 03.04.2021");
                                StartDate = Utility.Date();
                                Console.WriteLine("Enter second date (to which date).");
                                EndDate = Utility.Date();
                            }
                            List<Room> AllRooms = Database.Database.rooms;
                            List<Room> AvailableRooms = new List<Room>();
                            foreach (Room room in AllRooms)
                            {
                                if (room.booking != null)
                                {

                                    if (EndDate < room.booking.StartDate || StartDate > room.booking.EndDate)
                                    {
                                        AvailableRooms.Add(room);
                                        continue;
                                    }
                                    continue;
                                }
                                AvailableRooms.Add(room);
                            }
                            if (AvailableRooms.Count != 0)
                            {
                                Console.WriteLine("-----------------------Available rooms--------------- ");
                                foreach (Room room in AvailableRooms)
                                {
                                    Console.WriteLine($"Room Id(number):{room.Id}, number of AvailableRooms:{room.NumberofRooms}, Price:{room.PricePerDay}");
                                }
                                Utility.TillEnterPressed();
                            }
                            else if (AvailableRooms.Count == 0)
                            {
                                Console.WriteLine("There is no available room for this interval.");
                                Thread.Sleep(2300);
                            }
                        }
                        break;
                    case 3:
                        if (Utility.IsUserLoggedIn(user))
                        {

                            Console.WriteLine("Enter PIN(personal identification number) of the customer");
                            string PIN1 = Console.ReadLine();
                            Customer customer = Database.Database.customers.Find(customer => customer.PIN == PIN1 && customer.IsCheckedOut == false);
                            if (customer != null)
                            {
                                if (customer.HasBooking)
                                {
                                    Console.WriteLine("A customer can have one booking at a time!");
                                    Thread.Sleep(2000);
                                    break;
                                }
                                Console.WriteLine($"The customer info:Name:{customer.Name},Phone:{customer.Phone}\n");
                                Console.WriteLine("Enter first date (From which date). For example, 03.04.2021");
                                DateTime StartDate = Utility.Date();
                                Console.WriteLine("Enter second date (to which date).");
                                DateTime EndDate = Utility.Date();
                                while (StartDate >= EndDate)
                                {
                                    Console.WriteLine("Start date cannot be greater than end date!\n");
                                    Console.WriteLine("Enter first date (From which date). For example, 03.04.2021");
                                    StartDate = Utility.Date();
                                    Console.WriteLine("Enter second date (to which date).");
                                    EndDate = Utility.Date();
                                }
                                List<Room> AllRooms = Database.Database.rooms;
                                List<Room> AvailableRooms = new List<Room>();
                                foreach (Room room in AllRooms)
                                {
                                    if (room.booking != null)
                                    {

                                        if (EndDate < room.booking.StartDate || StartDate > room.booking.EndDate)
                                        {
                                            AvailableRooms.Add(room);
                                            continue;
                                        }
                                        continue;
                                    }
                                    AvailableRooms.Add(room);
                                }
                                if (AvailableRooms.Count != 0)
                                {
                                    Console.WriteLine("-----------------------Available rooms--------------- ");
                                    foreach (Room room in AvailableRooms)
                                    {
                                        Console.WriteLine($"Room Id(number):{room.Id}, number of AvailableRooms:{room.NumberofRooms}, Price:{room.PricePerDay}");
                                    }

                                }
                                else if (AvailableRooms.Count == 0)
                                {
                                    Console.WriteLine("There is no available room for this interval.");
                                    Thread.Sleep(2300);
                                    break;
                                }

                                Console.WriteLine("Which room to book?( ID of the room is required! )");
                                int ID = Utility.ExceptionHandlerForIntegers();
                                Room selectedRoom = Database.Database.rooms.Find(room => room.Id == ID);
                                selectedRoom.IsBooked = true;
                                Booking booking = new Booking(StartDate, EndDate, customer, selectedRoom);
                                Database.Database.bookings.Add(booking);
                                customer.Booking = booking;
                                customer.room = selectedRoom;
                                customer.HasBooking = true;
                                selectedRoom.booking = booking;
                                selectedRoom.customer = customer;
                                Console.WriteLine("The room has successfully been booked!");
                                Thread.Sleep(2300);
                                break;
                            }
                            Console.WriteLine("The customer first needs to be checked in");
                            Thread.Sleep(2300);
                        }
                        break;
                    case 4:
                        if (Utility.IsUserLoggedIn(user))
                        {


                            Console.WriteLine("Enter PIN(personal identification number) of the customer");
                            string PIN = Console.ReadLine();
                            if (Database.Database.customers.Exists(customer => customer.PIN == PIN && !customer.IsCheckedOut))
                            {
                                Customer customer = Database.Database.customers.Find(customer => customer.PIN == PIN);
                                string infoAboutBooking1;
                                if (customer.Booking == null)
                                {
                                    infoAboutBooking1 = "This customer has not booked a room yet";
                                }
                                else infoAboutBooking1 = $"Booked room {customer.room.Id} from {customer.Booking.StartDate.ToString("dd/MM/yyyy")} to {customer.Booking.EndDate.ToString("dd/MM/yyyy")}";
                                Console.WriteLine($"{customer.Name}, Customer phone {customer.Phone}. {infoAboutBooking1}");
                                Utility.TillEnterPressed();
                            }
                            else
                            {
                                Console.WriteLine("There is no such customer");
                                Thread.Sleep(2000);
                            }
                        }
                        break;
                    case 5:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Console.WriteLine("Enter PIN(personal identification number) of the customer");
                            string PIN = Console.ReadLine();
                            if (!Database.Database.customers.Exists(customer => customer.PIN == PIN))//Let's see if this customer already exists in the database(PIN is always unique)
                            {

                                Console.WriteLine("Enter the name of the customer");
                                name = Console.ReadLine();
                                Console.WriteLine("Enter the phone number of the customer");
                                phone = Console.ReadLine();
                                Customer customer = new Customer(name, phone, PIN);
                                Database.Database.customers.Add(customer);
                                Console.WriteLine("A cutomer has been added successfuly");
                                Thread.Sleep(2000);
                                break;
                            }
                            Customer customer1 = Database.Database.customers.Find(customer => customer.PIN == PIN);
                            Console.WriteLine($"The customer has already been in the hotel and checked out {customer1.LeavingDate.ToString("'in' dd/MM/yyyy")}.The customer info has been updated.");
                            customer1.IsCheckedOut = false;
                            customer1.LeavingDate = default(DateTime);
                            customer1.EnterDate = DateTime.Now;
                            Thread.Sleep(3000);
                        }
                        break;
                    case 6:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Console.WriteLine("Enter PIN(personal identification number)");
                            string PIN = Console.ReadLine();
                            if (Database.Database.customers.Exists(customer => customer.PIN == PIN))
                            {
                                Customer WantedCustomer = Database.Database.customers.Find(customer => customer.PIN == PIN);
                                WantedCustomer.IsCheckedOut = true;
                                WantedCustomer.LeavingDate = DateTime.Now;
                                WantedCustomer.Booking = null;
                                if (WantedCustomer.room != null)
                                {
                                    WantedCustomer.room.IsBooked = false;
                                    WantedCustomer.room.booking.StartDate = default(DateTime);
                                    WantedCustomer.room.booking.EndDate = default(DateTime);
                                    WantedCustomer.room.booking = null;
                                    WantedCustomer.room = null;
                                }

                                Console.WriteLine("The customer has been checked out successfuly");
                                Thread.Sleep(2000);
                                break;
                            }
                            Console.WriteLine("There is no such customer");
                            Thread.Sleep(2000);
                            break;
                        }
                        break;
                    case 7:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Menu menuForRoom = new Menu(new string[] { "Create a room", "Delete a room", "Room info by ID", "Update a room" }, prompt);
                            int selectedIndexForRoom = 0;
                            selectedIndexForRoom = menuForRoom.Run(selectedIndexForRoom);
                            switch (selectedIndexForRoom)
                            {
                                case 0:
                                    Console.WriteLine("Enter room number");
                                    int roomId = Utility.ExceptionHandlerForIntegers();
                                    Console.WriteLine("Enter price per day");
                                    decimal roomPrice = Utility.ExceptionHandlerForIntegers();
                                    Console.WriteLine("Number of rooms");
                                    byte NumberOfRooms = (Byte)Utility.ExceptionHandlerForIntegers();
                                    Room room = new Room(roomId, roomPrice, NumberOfRooms);
                                    Database.Database.rooms.Add(room);
                                    Console.WriteLine("The room has been added successfully!");
                                    Thread.Sleep(2000);

                                    break;
                                case 1:
                                    Console.WriteLine("Enter room number");
                                    roomId = Utility.ExceptionHandlerForIntegers();
                                    if (Database.Database.rooms.Exists(room => room.Id == roomId))
                                    {
                                        room = Database.Database.rooms.Find(room => room.Id == roomId);
                                        room.IsDeleted = true;
                                        Console.WriteLine("The room has been deleted");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    Console.WriteLine("There is no such room");
                                    Thread.Sleep(2000);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter room number");
                                    roomId = Utility.ExceptionHandlerForIntegers();
                                    room = Database.Database.rooms.Find(room => room.Id == roomId);

                                    if (room != null && !room.IsDeleted)
                                    {
                                        string extraInfoAboutRoom;
                                        if (room.IsBooked)
                                        {
                                            extraInfoAboutRoom = $"The room is booked from {room.booking.StartDate} to {room.booking.EndDate} by {room.customer.Name} \n Customer ID: {room.customer.Id} ";
                                        }
                                        else extraInfoAboutRoom = "The room has not been booked yet";
                                        Console.WriteLine($"Room Id(number):{room.Id}, number of rooms:{room.NumberofRooms}\nPrice:{room.PricePerDay}.{extraInfoAboutRoom}");
                                        Utility.TillEnterPressed();
                                        break;
                                    }
                                    Console.WriteLine("There is no such room");
                                    Thread.Sleep(2000);
                                    break;
                                case 3:
                                    Console.WriteLine("Enter room number");
                                    roomId = Utility.ExceptionHandlerForIntegers();
                                    room = Database.Database.rooms.Find(room => room.Id == roomId);
                                    if (room != null && !room.IsDeleted && !room.IsBooked)
                                    { //We will not change the number of the room since it is unique.
                                        Database.Database.rooms.Remove(room);
                                        Console.WriteLine("Enter new price per day");
                                        roomPrice = Utility.ExceptionHandlerForIntegers();
                                        Console.WriteLine("Number of rooms");
                                        NumberOfRooms = (Byte)Utility.ExceptionHandlerForIntegers();
                                        room = new Room(roomId, roomPrice, NumberOfRooms);
                                        Database.Database.rooms.Add(room);
                                        Console.WriteLine("The room has been updated successfully!");
                                        Thread.Sleep(2000);
                                    }
                                    else if (room == null)
                                    {
                                        Console.WriteLine("There is no such room");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (room.IsBooked)
                                    {
                                        Console.WriteLine("You cannot update booked rooms");
                                        Thread.Sleep(2000);

                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 8:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Menu menuForCustomer = new Menu(new string[] { "Update the booking of the customer(Extend date)", "Cancel booking", "Search a customer by PIN" }, prompt);
                            int selectedIndexForCustomer = 0;
                            selectedIndexForCustomer = menuForCustomer.Run(selectedIndexForCustomer);
                            switch (selectedIndexForCustomer)
                            {
                                case 0:
                                    Customer customer = Utility.SearchCustomer();
                                    if (customer != null && !customer.IsCheckedOut && customer.Booking != null)
                                    {
                                        Console.WriteLine("Enter first date (From which date). For example, 03.04.2021");
                                        DateTime StartDate = Utility.Date();
                                        Console.WriteLine("Enter second date (to which date).");
                                        DateTime EndDate = Utility.Date();
                                        while (StartDate >= EndDate)
                                        {
                                            Console.WriteLine("Start date cannot be greater than end date!\n");
                                            Console.WriteLine("Enter first date (From which date). For example, 03.04.2021");
                                            StartDate = Utility.Date();
                                            Console.WriteLine("Enter second date (to which date).");
                                            EndDate = Utility.Date();
                                        }
                                        customer.Booking.StartDate = StartDate;
                                        customer.Booking.EndDate = EndDate;
                                        customer.room.booking.StartDate = StartDate;
                                       customer.room.booking.EndDate = EndDate;
                                        Booking booking = Database.Database.bookings.Find(booking => booking.customer.Id == customer.Id);
                                        booking.StartDate = StartDate;
                                        booking.EndDate = EndDate;
                                        Room room = Database.Database.rooms.Find(room =>room.customer.Id==customer.Id);
                                        room.booking.StartDate = StartDate;
                                        room.booking.EndDate = EndDate;
                                        Console.WriteLine("The update has been successfull");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (customer == null)
                                    {
                                        Console.WriteLine("There is no such customer");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (customer.IsCheckedOut)
                                    {
                                        Console.WriteLine("There is no such customer");
                                        Thread.Sleep(2000);

                                    }
                                    else if (customer.Booking == null && !customer.IsCheckedOut) { Console.WriteLine("The customer hasn't booked any room yet"); Thread.Sleep(2000); break; }

                                    break;
                                case 1:
                                    customer = Utility.SearchCustomer();
                                    if (customer != null && !customer.IsCheckedOut && customer.Booking != null)
                                    {
                                        customer.Booking.IsCancelled = true;
                                        customer.Booking = null;
                                        customer.room.booking = null;
                                        customer.room.customer = null;
                                        customer.room.IsBooked = false;
                                        customer.room = null;
                                        customer.HasBooking = false;
                                        Console.WriteLine("The booking has been cancelled!");
                                        Utility.TillEnterPressed();
                                        break;
                                    }
                                    else if (customer == null)
                                    {
                                        Console.WriteLine("There is no such customer");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (customer.Booking == null) { Console.WriteLine("The customer hasn't booked any room yet"); Thread.Sleep(2000); break; }
                                    Console.WriteLine("There is no such customer");
                                    Thread.Sleep(2000);
                                    break;
                                case 2:
                                    customer = Utility.SearchCustomer();
                                    if (customer != null && !customer.IsCheckedOut)
                                    {
                                        string extraInfoForCustomer;
                                        if (customer.Booking != null)
                                        {
                                            extraInfoForCustomer = $"Booked room {customer.room.Id} from {customer.Booking.StartDate.ToString("dd/MM/yyyy")} to {customer.Booking.EndDate.ToString("dd/MM/yyyy")}";
                                        }
                                        else extraInfoForCustomer = "The customer has not booked a room yet";
                                        Console.WriteLine($"Name:{customer.Name},Phone:{customer.Phone} Registration date:{customer.EnterDate}\n" +
                                            $"PIN:{customer.PIN}.{extraInfoForCustomer} ");
                                        Utility.TillEnterPressed();
                                        break;
                                    }
                                    if (customer == null)
                                    {
                                        Console.WriteLine("There is no such customer");
                                        Thread.Sleep(2000);
                                    }
                                    else if (customer.IsCheckedOut)
                                    {
                                        Console.WriteLine("There is no such customer");
                                        Thread.Sleep(2000);
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;
                    case 9:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Menu menuForBooking = new Menu(new string[] { "Search by customer", "Search by date", "Search by room" }, prompt);
                            int selectedIndexForBooking = 0;
                            selectedIndexForBooking = menuForBooking.Run(selectedIndexForBooking);
                            switch (selectedIndexForBooking)
                            {
                                case 0:
                                    Customer customer = Utility.SearchCustomer();
                                    if (customer != null && !customer.IsCheckedOut && customer.Booking != null)
                                    {
                                        Booking booking = customer.Booking;
                                        Console.WriteLine($"Booked room {customer.room.Id} from {customer.Booking.StartDate.ToString("dd/MM/yyyy")} to {customer.Booking.EndDate.ToString("dd/MM/yyyy")}\n" +
                                            $"By {customer.Name}");
                                        Utility.TillEnterPressed();
                                    }
                                    else if (customer == null)
                                    {
                                        Console.WriteLine("There is no such customer");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (customer.Booking == null && !customer.IsCheckedOut) { Console.WriteLine("The customer hasn't booked a room yet"); Thread.Sleep(2000); break; }
                                    else if (customer.IsCheckedOut) { Console.WriteLine("There is no such customer"); Thread.Sleep(2000); break; }
                                    break;
                                case 1:
                                    Console.WriteLine("Enter start date (From which date). For example, 03.04.2021");
                                    DateTime StartDate = Utility.Date();
                                    Console.WriteLine("Enter second date (to which date).");
                                    DateTime EndDate = Utility.Date();
                                    while (StartDate >= EndDate)
                                    {
                                        Console.WriteLine("Start date cannot be greater than end date!\n");
                                        Console.WriteLine("Enter start date (From which date). For example, 03.04.2021");
                                        StartDate = Utility.Date();
                                        Console.WriteLine("Enter second date (to which date).");
                                        EndDate = Utility.Date();
                                    }
                                    List<Booking> bookings = Database.Database.bookings.FindAll(booking => booking.StartDate >= StartDate && booking.EndDate <= EndDate);
                                    if (bookings.Count == 0)
                                    {
                                        Console.WriteLine("There is no booking in this interval");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    foreach (Booking booking in bookings)
                                    {
                                        if (booking.IsCancelled)
                                        {
                                            Console.Write("Cancelled booking---->");
                                        }
                                        Console.WriteLine($"Booked room {booking.room.Id} from {booking.StartDate.ToString("dd/MM/yyyy")} to {booking.EndDate.ToString("dd/MM/yyyy")}\n" +
                                            $"By {booking.customer.Name}");
                                    }
                                    Utility.TillEnterPressed();
                                    break;
                                case 2:
                                    Room room = Utility.SearchRoom();
                                    if (room != null && room.booking != null)
                                    {
                                        Console.WriteLine($"The room is booked from {room.booking.StartDate} to {room.booking.EndDate} by {room.customer.Name}\n" +
                                            $"Booking id:{room.booking.Id}");
                                        Utility.TillEnterPressed();
                                        break;
                                    }
                                    else if (room == null)
                                    {
                                        Console.WriteLine("There is no such room");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (room.booking == null)
                                    {
                                        Console.WriteLine("The room has not been booked yet!");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 10:
                        if (Utility.IsUserLoggedIn(user))
                        {
                            Console.WriteLine("Are you sure that you want to log out? if Yes press Enter if no press any key to go back");
                            ConsoleKeyInfo consoleKey1 = Console.ReadKey();
                            if (consoleKey1.Key == ConsoleKey.Enter)
                            {
                                if (menu1.Prompt.Contains("Hello"))
                                {
                                    menu1.Prompt = menu1.Prompt.Remove(menu1.Prompt.IndexOf("Hello") - 1);
                                }
                                user = null;
                            }
                            break;
                        }
                        break;
                    default:
                        break;
                }
                selectedIndex = menu1.Run(selectedIndex);
            } while (true);
        }
    }
}
