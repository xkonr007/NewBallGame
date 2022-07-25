using CourseWork.Models.Abstract;
using System.Drawing;

namespace CourseWork.Models
{
    public class BrickModel : BasicModel
    {
        public BrickModel(ConsoleColor consoleColor = ConsoleColor.Magenta) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '#';
    }
}
