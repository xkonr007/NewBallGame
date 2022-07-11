using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;
using NewBallGameWPF.Models;
using NewBallGameWPF.Models.Abstract;
using NewBallGameWPF.Field;

namespace NewBallGameWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Stopwatch timer;
        public Level.Level level;
        public Menu menu;
        private int levelId;

        DispatcherTimer gameTimer = new DispatcherTimer();

        
        public GameWindow(int levelId)
        {
            InitializeComponent();
            this.levelId = levelId;
            gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            gameTimer.Tick += Loop;
            gameTimer.Start();
            MyCanvas.Focus();

            Play();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            switch (key)
            {
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                    var speed = keySpeed[key];
                    if (level.Field.IsCoordinatesOK(level.Field.cursor.Position.X + speed.X, level.Field.cursor.Position.Y + speed.Y))
                    {
                        level.Field.cursor.Position = new System.Drawing.Point(level.Field.cursor.Position.X + speed.X, level.Field.cursor.Position.Y + speed.Y);
                    }
                    break;
                case Key.Z:
                case Key.X:
                    if (level.Field.CheckEmptyPoint(new System.Drawing.Point(level.Field.cursor.Position.X, level.Field.cursor.Position.Y)))
                    {
                        level.Field[level.Field.cursor.Position.X, level.Field.cursor.Position.Y] = keyModels[key];
                    }
                    break;
                case Key.C:
                    if (level.Field.CheckSlash(new System.Drawing.Point(level.Field.cursor.Position.X, level.Field.cursor.Position.Y)))
                    {
                        level.Field[level.Field.cursor.Position.X, level.Field.cursor.Position.Y] = keyModels[key];
                    }
                    break;
                default:
                    level.Field.cursor.Position = new System.Drawing.Point(level.Field.cursor.Position.X, level.Field.cursor.Position.Y);
                    break;
            }
        }

        public static Dictionary<Key?, System.Drawing.Point> keySpeed = new Dictionary<Key?, System.Drawing.Point>
        {
            { Key.Left, new System.Drawing.Point(-1, 0) },
            { Key.Right, new System.Drawing.Point(1, 0) },
            { Key.Up, new System.Drawing.Point(0, -1) },
            { Key.Down, new System.Drawing.Point(0, 1) },
        };

        public static Dictionary<Key?, Basic> keyModels = new Dictionary<Key?, Basic>
        {
            { Key.Z, new Backslash() },
            { Key.X, new Slash() },
            { Key.C, new Empty() }
        };

        private void OnKeyUp(object sender, KeyEventArgs e)
        {

        }

        private void PrepareLevel()
        {
            level = Level.Level.GetLevel(levelId);
            timer = new Stopwatch();
            level.GetLevelField();
            GameCursor cursor = new GameCursor(new System.Drawing.Point(3, 3));
        }

        private void Play()
        {
            PrepareLevel();
            timer.Start();
        }

        private void Loop(object sender, EventArgs e)
        {
            if (level.Field.MagicBalls > 0 && timer.Elapsed.TotalSeconds < level.LevelData.Seconds)
            {
                level.Field.MoveBall();
                Print(level.Field, MyCanvas);
                level.Field.cursor.Draw(level.Field, MyCanvas);
                score.Content = $"Time: {level.LevelData.Seconds - timer.Elapsed.Seconds} Score: {(level.LevelData.MagicBalls - level.Field.MagicBalls) * 5} Balls: {level.Field.MagicBalls}";
                MyCanvas.Children.Add(score);
                MyCanvas.Children.Add(control);
            }
            else
            {
                if (level.Field.MagicBalls == 0)
                {
                    MessageBox.Show("You won!");
                    Close();
                    gameTimer.Stop();
                    MainWindow mw = new MainWindow();
                    mw.Show();
                }
                else if (level.Field.MagicBalls > 0)
                {
                    MessageBox.Show("You lost!");
                    Close();
                    gameTimer.Stop();
                    MainWindow mw = new MainWindow();
                    mw.Show();
                }
            }
        }

        private void Print(FieldModel field, Canvas myCanvas)
        {
            myCanvas.Children.Clear();

            int i = 0, j = 0;
            for (int y = 0; y < field.Width; y++)
            {
                i = 0;
                for (int x = 0; x < field.Height; x++)
                {
                    System.Windows.Shapes.Rectangle cell = new System.Windows.Shapes.Rectangle()
                    {
                        Width = 20,
                        Height = 20
                    };

                    if (field[x, y] is Ball)
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Irina\Desktop\WpfApp1\WpfApp1\bin\Debug\net6.0-windows\images\redball.png", UriKind.Relative));
                        cell.Fill = brush;
                    }

                    else if (field[x, y] is Backslash)
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Irina\Desktop\WpfApp1\WpfApp1\bin\Debug\net6.0-windows\images\backslash.png", UriKind.Relative));
                        cell.Fill = brush;
                    }

                    else if (field[x, y] is Slash)
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Irina\Desktop\WpfApp1\WpfApp1\bin\Debug\net6.0-windows\images\slash.png", UriKind.Relative));
                        cell.Fill = brush;
                    }

                    else if (field[x, y] is Brick)
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Irina\Desktop\WpfApp1\WpfApp1\bin\Debug\net6.0-windows\images\wall.png", UriKind.Relative));
                        cell.Fill = brush;
                    }

                    else if (field[x, y] is MagicBall)
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Irina\Desktop\WpfApp1\WpfApp1\bin\Debug\net6.0-windows\images\magicball.png", UriKind.Relative));
                        cell.Fill = brush;
                    }

                    else if (field[x, y] is Empty)
                    {
                        ImageBrush brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\Irina\Desktop\WpfApp1\WpfApp1\bin\Debug\net6.0-windows\images\empty.png", UriKind.Relative));
                        cell.Fill = brush;
                    }

                    Canvas.SetLeft(cell, i);
                    Canvas.SetTop(cell, j);

                    myCanvas.Children.Add(cell);

                    i += 20;
                }
                j += 20;
            }
        }
    }
}
