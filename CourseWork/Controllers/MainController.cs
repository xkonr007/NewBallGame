using CourseWork.Models;
using System.Drawing;
using CourseWork.Models.Abstract;
using CourseWork.Field;

namespace CourseWork.Controllers
{
    public class MainController
    {
        public static ConsoleKey? ReadMovement()
        {
            if (!Console.KeyAvailable)
            {
                return null;
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                case ConsoleKey.Z:
                case ConsoleKey.X:
                case ConsoleKey.C:
                    return key;
                default:
                    return null;
            }
        }

        public static Dictionary<ConsoleKey?, Point> keySpeed = new Dictionary<ConsoleKey?, Point>
        {
            { ConsoleKey.LeftArrow, new Point(-1, 0) },
            { ConsoleKey.RightArrow, new Point(1, 0) },
            { ConsoleKey.UpArrow, new Point(0, -1) },
            { ConsoleKey.DownArrow, new Point(0, 1) },
        };

        public static Dictionary<ConsoleKey?, BasicModel> keyModels = new Dictionary<ConsoleKey?, BasicModel>
        {
            { ConsoleKey.Z, new BackslashShieldModel() },
            { ConsoleKey.X, new ForwardSlashShieldModel() },
            { ConsoleKey.C, new EmptyModel() }
        };

        public static void PrintField(FieldModel field)
        {
            for (int x = 0; x < field.Width; x++)
            {
                for (int y = 0; y < field.Height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    field[x, y].Draw();
                }
            }
        }
    }
}
