using System.Drawing;
using CourseWork.Models.Abstract;

namespace CourseWork.Models
{
    public  class Trap : BasicModel
    {
        public Trap(ConsoleColor consoleColor = ConsoleColor.Magenta) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '*';
    }
}
