using System;

namespace Hotel_Management_System.ConsoleMenu
{
    interface IMenuServices
    {
        int Run(int selectedIndex);
        void Display();
    }
}
