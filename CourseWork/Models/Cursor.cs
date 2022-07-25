using CourseWork.Models.Abstract;
using System.Drawing;

namespace CourseWork.Models
{
    public class Cursor 
    {
        public ConsoleColor CursorColor { get; set; } = ConsoleColor.White;
        public Point Position { get; set; }
        public char SymbolForOutput { get; } = ' ';
        public Cursor(Point position)
        {
            Position = position;
        }

        public void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.BackgroundColor = CursorColor;
            Console.Write(SymbolForOutput);
            Console.ResetColor();
        }
    }
}

