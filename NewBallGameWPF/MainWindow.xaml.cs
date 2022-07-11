using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewBallGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Level.Text = "Level";
            Level.Items.Add("1");
            Level.Items.Add("2");
            Level.Items.Add("3");
            
        }

        private int levelId = 1;
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
    }
}
