using System;
namespace Hotel_Management_System.ConsoleMenu
{
    class Menu : IMenuServices
    {
        public int SelectedIndex { get; set; }
        public string[] Options { get; set; }
        public string Prompt { get; set; }
        public string Title { get; set; }
        public Menu(string[] Options, string Prompt)
        {
            this.Options = Options;
            this.Prompt = Prompt;
            this.SelectedIndex = 0;
        }
        public void Display()
        {

            this.Title = @"
░█─░█ █▀▀█ ▀▀█▀▀ █▀▀ █── 　 ░█▀▄▀█ █▀▀█ █▀▀▄ █▀▀█ █▀▀▀ █▀▀ █▀▄▀█ █▀▀ █▀▀▄ ▀▀█▀▀ 　 ░█▀▀▀█ █──█ █▀▀ ▀▀█▀▀ █▀▀ █▀▄▀█ 
░█▀▀█ █──█ ──█── █▀▀ █── 　 ░█░█░█ █▄▄█ █──█ █▄▄█ █─▀█ █▀▀ █─▀─█ █▀▀ █──█ ──█── 　 ─▀▀▀▄▄ █▄▄█ ▀▀█ ──█── █▀▀ █─▀─█ 
░█─░█ ▀▀▀▀ ──▀── ▀▀▀ ▀▀▀ 　 ░█──░█ ▀──▀ ▀──▀ ▀──▀ ▀▀▀▀ ▀▀▀ ▀───▀ ▀▀▀ ▀──▀ ──▀── 　 ░█▄▄▄█ ▄▄▄█ ▀▀▀ ──▀── ▀▀▀ ▀───▀";
            Console.WriteLine(Title);
            Console.WriteLine("\n\n\n");
            Console.SetCursorPosition((Console.WindowWidth - this.Prompt.Length) / 2, Console.CursorTop);
            Console.Write(this.Prompt);
            Console.WriteLine("\n");
            for (int i = 0; i < this.Options.Length; i++)
            {
                string currentOption = this.Options[i];
                if (this.SelectedIndex == i)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.SetCursorPosition((Console.WindowWidth - currentOption.Length) / 2, Console.CursorTop);
                Console.Write($"<< {currentOption} >>");
                Console.WriteLine();
            }
        }

        public int Run(int selectedIndex)
        {
            this.SelectedIndex = SelectedIndex;
            ConsoleKeyInfo pressedKey;
            do
            {
                Console.Clear();
                Display();
                pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    this.SelectedIndex++;
                    if (this.SelectedIndex == this.Options.Length)
                    {
                        this.SelectedIndex = 0;
                    }

                }
                else if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    this.SelectedIndex--;
                    if (this.SelectedIndex == -1)
                    {
                        this.SelectedIndex = this.Options.Length - 1;

                    }

                }
                Console.ResetColor();

            } while (pressedKey.Key != ConsoleKey.Enter);
            Console.Clear();
            Console.ResetColor();

            return this.SelectedIndex;
        }
    }
}
