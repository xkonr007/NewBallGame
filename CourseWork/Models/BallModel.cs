using System.Drawing; 
using CourseWork.Models.Abstract;
using CourseWork.Field;

namespace CourseWork.Models
{
    public class BallModel : BasicModel
    {
        public BallModel(ConsoleColor consoleColor = ConsoleColor.Red) : base(consoleColor) { }

        public Point Position { get; set; }
        public override char SymbolForOutput { get; } = 'o';
        public int DX { get; set; } = 0;
        public int DY { get; set; } = 1;

        public new void Draw()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.ForegroundColor = ConsoleColor;
            Console.Write(SymbolForOutput);
            Console.ResetColor();
        }

        public void Move(Point speed, BasicModel[,] field)
        {
            var toClear = Position;
            Position = new Point(Position.X + speed.X, Position.Y + speed.Y);
            field[toClear.X, toClear.Y] = new EmptyModel();
        }
    }
}
