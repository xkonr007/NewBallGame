using System.Drawing;

namespace CourseWork.Models.Abstract
{
    public abstract class BasicModel
    { 
        public ConsoleColor ConsoleColor { get; set; }
        public abstract char SymbolForOutput { get; }
        public BasicModel(ConsoleColor consoleColor)
        {
            ConsoleColor = consoleColor;
        }

        public void Draw() 
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor;
            Console.Write(SymbolForOutput);
            Console.ResetColor();
        } 
    }
}
