using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NewBallGameWPF.Field;

namespace NewBallGameWPF.Models.Abstract
{
    public class Basic
    {
        public char ArrayChar { get; set; }
        public string ImagePath { get; set; }

        public Basic(char arrayChar, string imagePath)
        {
            ArrayChar = arrayChar;
            ImagePath = imagePath;
        }

        public void Draw(Canvas myCanvas, FieldModel field, int x, int y,int i, int j)
        {
            System.Windows.Shapes.Rectangle cell = new System.Windows.Shapes.Rectangle()
            {
                Width = 20,
                Height = 20
            };

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(field[x, y].ImagePath, UriKind.Relative));
            cell.Fill = brush;

            Canvas.SetLeft(cell, i);
            Canvas.SetTop(cell, j);

            myCanvas.Children.Add(cell);
        }
    }
}
