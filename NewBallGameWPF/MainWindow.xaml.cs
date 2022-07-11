using System;
using System.Windows;
using System.Windows.Controls;

namespace NewBallGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int levelId = 1;
        public MainWindow()
        {
            InitializeComponent();

            Level.Text = "Level";
            Level.Items.Add("1");
            Level.Items.Add("2");
            Level.Items.Add("3");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            GameWindow window = new GameWindow(levelId);
            window.Show();
        }

        private void Level_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int comboText = Level.SelectedIndex; 

            switch (comboText)
            {
                case 0:
                    levelId = 1;
                    break;
                case 1:
                    levelId = 2;
                    break;
                default:
                    levelId = 3;
                    break;
            }
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
