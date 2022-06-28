using CourseWork.Models.Abstract;

namespace CourseWork.Models
{
     public class ForwardSlashShieldModel : BasicModel
    {
        public ForwardSlashShieldModel(ConsoleColor consoleColor = ConsoleColor.Green) : base(consoleColor) { }

        public override char SymbolForOutput { get; } = '/';
    }
}
