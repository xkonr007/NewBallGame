using System;
using System.Windows;
using System.Windows.Controls;
using System.Media;

namespace NewBallGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int levelId = 1;
        private bool music = false;
        public MainWindow()
        {
            InitializeComponent();

            AddLevels();

            //SoundPlayer sp = new SoundPlayer();
            //sp.SoundLocation = @"D:\Downloads\myusli-ua-vasia-charisma-dobrij-den-everybody-mp3.wav";
            //sp.Load();
            //sp.PlayLooping();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            GameWindow window = new GameWindow(levelId,music);
            window.Show();
        }

        private void AddLevels()
        {
            Level.Text = "Level";
            Level.Items.Add("1");
            Level.Items.Add("2");
            Level.Items.Add("3");
        }

        private void Level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int comboText = Level.SelectedIndex;

            levelId = comboText + 1;
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void AddMusic(object sender, RoutedEventArgs e)
        {
            if (music)
            {
                music = false;
                MusicButton.Content = "Add Music";
            }
            else
            {
                music = true;
                MusicButton.Content = "Remove Music";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
