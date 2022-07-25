using CourseWork.Models.Abstract;

namespace CourseWork.Models
{
    public class MagicPillModel : BasicModel
    {
        public MagicPillModel(ConsoleColor consoleColor = ConsoleColor.Blue) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '@';
    }
}
