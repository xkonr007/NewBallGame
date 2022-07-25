using CourseWork.Models.Abstract;

namespace CourseWork.Models
{
    public class BackslashShieldModel : BasicModel
    {
        public BackslashShieldModel(ConsoleColor consoleColor = ConsoleColor.Green) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '\\';
    }
}
