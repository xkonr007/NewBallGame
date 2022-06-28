using System.Drawing;
using CourseWork.Levels;
using System.Diagnostics;
using CourseWork.Models;

namespace CourseWork.Actions
{
    internal class GameProcess
    {
        private Stopwatch timer;
        public Level level;
        public Menu menu;
        public void Start()
        {
            Console.CursorVisible = false;
            Console.Title = "New Ball Game";
            RunMenu();
        }

        private void RunMenu()
        {
            string prompt = @"
 _        _______             ______   _______  _        _          _______  _______  _______  _______ 
( (    /|(  ____ \|\     /|  (  ___ \ (  ___  )( \      ( \        (  ____ \(  ___  )(       )(  ____ \
|  \  ( || (    \/| )   ( |  | (   ) )| (   ) || (      | (        | (    \/| (   ) || () () || (    \/
|   \ | || (__    | | _ | |  | (__/ / | (___) || |      | |        | |      | (___) || || || || (__    
| (\ \) ||  __)   | |( )| |  |  __ (  |  ___  || |      | |        | | ____ |  ___  || |(_)| ||  __)   
| | \   || (      | || || |  |    \ \ | (   ) || |      | |        | | \_  )| (   ) || |   | || (      
| )  \  || (____/\| () () |  | )___) )| )   ( || (____/\| (____/\  | (___) || )   ( || )   ( || (____/\
|/    )_)(_______/(_______)  |/ \___/ |/     \|(_______/(_______/  (_______)|/     \||/     \|(_______/                                                                                                                                                                                                         
";
            menu = new Menu(prompt);
            int selectedIndex = menu.RunMainMenu();

            switch (selectedIndex)
            {
                case 0:
                    Play();
                    break;
                case 1:
                    About();
                    break;
                case 2:
                    Exit();
                    break;
            }
        }

        private void Play()
        {
            timer = new Stopwatch();
            Console.Clear();
            Console.CursorVisible = false;
            level = Level.GetLevel(ChooseLevel());
            Console.Clear();
            level.GetLevelField();
            Cursor cursor = new Cursor(new Point(3, 3));
            level.LevelField.PrintField();
            level.LevelField.cursor = cursor;
            cursor.Draw();
            timer.Start();
            while (level.LevelField.MagicBalls > 0 && timer.Elapsed.TotalSeconds < level.LevelData.Seconds)
            {
                level.LevelField.MoveBall();
                level.LevelField.ball.Draw();
                level.LevelField.MoveCursor();
                DrawResults(timer.Elapsed, (level.LevelData.MagicBalls-level.LevelField.MagicBalls)*5, level.LevelField.MagicBalls);
                DrawInstructions();
                Thread.Sleep(100);
            }
            GetWinOrLose();
        }

        private int ChooseLevel()
        {
            string prompt = @"
 _        _______             ______   _______  _        _          _______  _______  _______  _______ 
( (    /|(  ____ \|\     /|  (  ___ \ (  ___  )( \      ( \        (  ____ \(  ___  )(       )(  ____ \
|  \  ( || (    \/| )   ( |  | (   ) )| (   ) || (      | (        | (    \/| (   ) || () () || (    \/
|   \ | || (__    | | _ | |  | (__/ / | (___) || |      | |        | |      | (___) || || || || (__    
| (\ \) ||  __)   | |( )| |  |  __ (  |  ___  || |      | |        | | ____ |  ___  || |(_)| ||  __)   
| | \   || (      | || || |  |    \ \ | (   ) || |      | |        | | \_  )| (   ) || |   | || (      
| )  \  || (____/\| () () |  | )___) )| )   ( || (____/\| (____/\  | (___) || )   ( || )   ( || (____/\
|/    )_)(_______/(_______)  |/ \___/ |/     \|(_______/(_______/  (_______)|/     \||/     \|(_______/                                                                                                                                                                                                         
";
            int selectedIndex = menu.RunLevelMenu();

            switch (selectedIndex)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                default:
                    return 3;
            }
        }

        private void GetWinOrLose()
        {
            if (level.LevelField.MagicBalls == 0)
            {
                string message =
 @"__     __                                    _ 
     \ \   / /                                   | |
      \ \_/ /___   _   _  __      __ ___   _ __  | |
       \   // _ \ | | | | \ \ /\ / // _ \ | '_ \ | |
        | || (_) || |_| |  \ V  V /| (_) || | | ||_|
        |_| \___/  \__,_|   \_/\_/  \___/ |_| |_|(_)";
                FinishLevel(message);
            }
            else if (level.LevelField.MagicBalls > 0)
            {
                string message =
 @"__     __             _              _    _ 
     \ \   / /            | |            | |  | |
      \ \_/ /___   _   _  | |  ___   ___ | |_ | |
       \   // _ \ | | | | | | / _ \ / __|| __|| |
        | || (_) || |_| | | || (_) |\__ \| |_ |_|
        |_| \___/  \__,_| |_| \___/ |___/ \__|(_)";
                FinishLevel(message);
            }
        }

        private void FinishLevel(string message)
        {
            Console.SetCursorPosition(5, 5);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Thread.Sleep(5000);
            Console.Clear();
            Console.ResetColor();
            RunMenu();
        }

        private void Exit()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void About()
        {
            Console.Clear();
            Console.WriteLine("https://www.google.com/");
            Console.ReadKey(true);
            RunMenu();
        }

        private void DrawResults(TimeSpan seconds, int score, int balls)
        {
            Console.SetCursorPosition(2, 22);
            Console.Write($"Timer:{seconds} Score:{score} MagicBalls:{balls}");
        }

        private void DrawInstructions()
        {
            Console.SetCursorPosition(2, 21);
            Console.Write("Press arrows to move cursor! Z - \\, X - /, C - clear ");
        }
    }
}
