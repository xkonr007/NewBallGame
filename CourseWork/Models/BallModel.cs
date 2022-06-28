using System.Drawing; 
using CourseWork.Models.Abstract;

namespace CourseWork.Models
{
    public class BallModel : BasicModel
    {
        public BallModel(ConsoleColor consoleColor = ConsoleColor.Red) : base(consoleColor) { }

        public Point Position { get; set; }
        public override char SymbolForOutput { get; } = 'o';
        public int SpeedX { get; set; } = 0;
        public int SpeedY { get; set; } = 1;

        public new void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.ForegroundColor = ConsoleColor;
            Console.Write(SymbolForOutput);
            Console.ResetColor();
        }
    }
}
