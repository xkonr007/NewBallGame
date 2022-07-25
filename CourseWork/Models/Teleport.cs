using CourseWork.Models.Abstract;
using System.Drawing;

namespace CourseWork.Models
{
    public class Teleport : BasicModel
    {
        public Teleport(ConsoleColor consoleColor = ConsoleColor.Blue) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '0';
    }
}
