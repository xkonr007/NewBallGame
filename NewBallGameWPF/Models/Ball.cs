using NewBallGameWPF.Models.Abstract;
using System.Drawing;

namespace NewBallGameWPF.Models
{
    public class Ball : Basic
    {
        public Ball(char arrayChar = 'o', string imagePath = @"images\Ball.png") : base(arrayChar, imagePath) { }
        public Point Position { get; set; }
        public int DX { get; set; } = 0;
        public int DY { get; set; } = 1;

        public void Move(Point speed, Basic[,] field)
        {
            var toClear = Position;
            Position = new Point(Position.X + speed.X, Position.Y + speed.Y);
            field[toClear.X, toClear.Y] = new Empty();
        }
    }
}