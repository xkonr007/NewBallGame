using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Diagnostics;
using NewBallGameWPF.Models;
using NewBallGameWPF.Field;
using NewBallGameWPF.Controller;

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

            MainController.CheckKey(key, level.Field);
        }

        private void OnKeyUp(object sender, KeyEventArgs e) { }

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
                MyCanvas.Children.Add(exit);
            }
            else
            {
                if (level.Field.MagicBalls == 0)
                {                  
                    MessageBox.Show("You won!");
                    EndGame();
                }
                else if (level.Field.MagicBalls > 0)
                {
                    MessageBox.Show("You lost!");
                    EndGame();
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

                    var typeName = field[x, y].GetType().Name.ToString();
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri($@"C:\Users\Irina\Desktop\c#\CourseWork\NewBallGame\NewBallGameWPF\bin\Debug\net6.0-windows\images\{typeName}.png", UriKind.Relative));
                    cell.Fill = brush;

                    Canvas.SetLeft(cell, i);
                    Canvas.SetTop(cell, j);

                    myCanvas.Children.Add(cell);

                    i += 20;
                }
                j += 20;
            }
        }

        private void EndGame()
        {
            gameTimer.Stop();
            Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }
    }
}
