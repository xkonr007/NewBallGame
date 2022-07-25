using System.Drawing;
using CourseWork.Levels;
using System.Diagnostics;
using CourseWork.Models;
using CourseWork.Controllers;
using CourseWork.Field;

namespace CourseWork.Actions
{
    internal class GameProcess
    {
        private Stopwatch timer;
        public Level level;
        public Menu menu;
        private int gameSpeed = 100;
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
                    ChooseSpeed();
                    RunMenu();
                    break;
                case 3:
                    Exit();
                    break;
            }
        }

        private void PrepareLevel()
        {
            level = Level.GetLevel((ChooseLevel()));
            timer = new Stopwatch();
        }

        private void PrepareField()
        {
            Console.Clear();
            Console.CursorVisible = false;
            level.GetLevelField();
            Cursor cursor = new Cursor(new Point(3, 3));
            MainController.PrintField(level.LevelField);
            level.LevelField.cursor = cursor;
            cursor.Draw();
            timer.Start();
        }

        private void Go()
        {
            while (level.LevelField.MagicBalls > 0 && timer.Elapsed.TotalSeconds < level.LevelData.Seconds && level.LevelField[level.LevelField.ball.Position.X, level.LevelField.ball.Position.Y] is not DeathTrap )
            {
                level.LevelField.MoveBall();
                level.LevelField.ball.Draw();
                level.LevelField.MakeAction();
                DrawResults(level.LevelData.Seconds - timer.Elapsed.Seconds, (level.LevelData.MagicBalls - level.LevelField.MagicBalls) * 5, level.LevelField.MagicBalls);
                DrawInstructions();
                Thread.Sleep(gameSpeed);
            }
        }

        private void Play()
        {
            PrepareLevel();
            PrepareField();
            Go();
            GetWinOrLose();
        }

        private int ChooseLevel()
        {
            int selectedIndex = menu.RunLevelMenu();

            switch (selectedIndex)
            {
                case 0:
                case 1:
                default:
                    return selectedIndex+1;
            }
        }

        private void ChooseSpeed()
        {
            int selectedIndex = menu.RunSpeedMenu();

            switch (selectedIndex)
            {
                case 0:
                    gameSpeed = 150;
                    return;
                case 2:
                    gameSpeed = 50;
                    return;
                default:
                    gameSpeed = 100;
                    return;
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

        private void DrawResults(int seconds, int score, int balls)
        {
            Console.SetCursorPosition(2, 22);
            Console.Write($"Timer: {seconds} Score: {score} MagicBalls: {balls} ");
        }

        private void DrawInstructions()
        {
            Console.SetCursorPosition(2, 21);
            Console.Write("Press arrows to move cursor! Z - \\, X - /, C - clear ");
        }
    }
}
