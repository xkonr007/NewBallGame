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
using System.Media;

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
        private SoundPlayer player = new SoundPlayer();

        DispatcherTimer gameTimer = new DispatcherTimer();

        public GameWindow(int levelId,bool music)
        {
            InitializeComponent();
            this.levelId = levelId;
            gameTimer.Interval = TimeSpan.FromMilliseconds(50);
            gameTimer.Tick += Loop;
            gameTimer.Start();
            MyCanvas.Focus();
            AddSoundtrack(music);

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


        private void PlaySound(string path)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = path;
            sp.Load();
            sp.Play();
        }

        private void PlayWinSound(bool win)
        {
            if (win)
            {
                PlaySound(@"Sounds\win.wav");
            }
            else
            {
                PlaySound(@"Sounds\gameover.wav");
            }
        }

        private void Loop(object sender, EventArgs e)
        {
            if (level.Field.MagicBalls > 0 && timer.Elapsed.TotalSeconds < level.LevelData.Seconds)
            {
                Go();
            }
            else
            {
                if (level.Field.MagicBalls == 0)
                {
                    PlayWinSound(true);
                    MessageBox.Show("You won!");
                    EndGame();
                }
                else if (level.Field.MagicBalls > 0)
                {
                    PlayWinSound(false);
                    MessageBox.Show("You lost!");
                    EndGame();
                }
            }
        }

        private void Go()
        {
            level.Field.MoveBall();
            Print(level.Field, MyCanvas);
            level.Field.cursor.Draw(level.Field, MyCanvas);
            score.Content = $"Time: {level.LevelData.Seconds - timer.Elapsed.Seconds} Score: {(level.LevelData.MagicBalls - level.Field.MagicBalls) * 5} Balls: {level.Field.MagicBalls}";
            MyCanvas.Children.Add(score);
            MyCanvas.Children.Add(control);
            MyCanvas.Children.Add(exit);
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
                    field[x, y].Draw(myCanvas, field, x, y, i, j);

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
            player.Stop();
            EndGame();
        }

        private void AddSoundtrack(bool music)
        {
            if (music)
            {
                player.SoundLocation = @"Sounds\soundtrack.wav";
                player.Load();
                player.Play();
            }
            else
            {
                return;
            }
        }
    }
}
