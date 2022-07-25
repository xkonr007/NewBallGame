using CourseWork.Models.Abstract;

namespace CourseWork.Models
{
    public class EmptyModel : BasicModel
    {
        public EmptyModel(ConsoleColor consoleColor = ConsoleColor.Black) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = ' ';
    }
}
