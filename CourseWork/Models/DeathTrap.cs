using CourseWork.Models.Abstract;
using System.Drawing;

namespace CourseWork.Models
{
    public class DeathTrap : BasicModel
    {
        public DeathTrap(ConsoleColor consoleColor = ConsoleColor.Magenta) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '*';
    }
}
