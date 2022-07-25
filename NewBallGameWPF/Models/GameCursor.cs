using System;
using System.Drawing;
using NewBallGameWPF.Field;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NewBallGameWPF.Models
{
    public class GameCursor
    {
        public char ArrayChar { get; set; }
        public string ImagePath { get; set; }
        public Point Position { get; set; }
        public GameCursor(Point position, char arrayChar = ' ', string imagePath = $@"images\Cursor.png")
        {
            Position = position;
            ArrayChar = arrayChar;
            ImagePath = imagePath;
        }

        public void Draw(FieldModel field, Canvas canvas)
        {
            System.Windows.Shapes.Rectangle cell = new System.Windows.Shapes.Rectangle()
            {
                Width = 20,
                Height = 20
            };

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            cell.Fill = brush;

            Canvas.SetLeft(cell, Position.X * 20);
            Canvas.SetTop(cell, Position.Y * 20);

            canvas.Children.Add(cell);
        }
    }
}
