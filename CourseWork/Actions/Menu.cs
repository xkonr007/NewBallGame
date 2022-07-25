namespace CourseWork.Actions
{
    public class Menu
    {
        private string[] mainMenuOptions = { "Play", "About", "Settings", "Exit" };
        private string[] levelOptions = { "Level 1", "Level 2", "Level 3" };
        private string[] speedOptions = { "Slow", "Medium", "Quick" };
        private string prompt;
        private int selectedIndex;

        public Menu(string prompt)
        {
            this.prompt = prompt;
            selectedIndex = 0;
        }

        public int RunMainMenu()
        {
            return Run(mainMenuOptions);
        }

        public int RunLevelMenu()
        {
            return Run(levelOptions);
        }

        public int RunSpeedMenu() 
        {
            return Run(speedOptions);
        }

        private void DisplayOptions(string[] options)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(prompt);

            for (int i = 0; i < options.Length; i++)
            {
                string currentOption = options[i];
                string prefix;

                if (i == selectedIndex)
                {
                    prefix = "->";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine($"{prefix} # {currentOption} #");
            }
            Console.ResetColor();
        }

        private int Run(string[] options)
        {
            ConsoleKey keyPressed;

            do
            {
                Console.Clear();
                DisplayOptions(options);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedIndex++; 
                }
                selectedIndex += options.Length;
                selectedIndex %= options.Length;
            } while (keyPressed != ConsoleKey.Enter);

            return selectedIndex;
        }
    }
}
