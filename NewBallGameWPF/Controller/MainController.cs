using System.Collections.Generic;
using System.Windows.Input;
using NewBallGameWPF.Models;
using NewBallGameWPF.Models.Abstract;
using NewBallGameWPF.Field;

namespace NewBallGameWPF.Controller
{
    public class MainController
    {
        public static Dictionary<Key?, System.Drawing.Point> keySpeed = new Dictionary<Key?, System.Drawing.Point>
        {
            { Key.Left, new System.Drawing.Point(-1, 0) },
            { Key.Right, new System.Drawing.Point(1, 0) },
            { Key.Up, new System.Drawing.Point(0, -1) },
            { Key.Down, new System.Drawing.Point(0, 1) },
        };

        public static Dictionary<Key?, Basic> keyModels = new Dictionary<Key?, Basic>
        {
            { Key.Z, new Backslash() },
            { Key.X, new Slash() },
            { Key.C, new Empty() }
        };

        public static void CheckKey(Key key, FieldModel field)
        {
            switch (key)
            {
                case Key.Up:
                case Key.Down:
                case Key.Left:
                case Key.Right:
                    var speed = keySpeed[key];
                    if (field.IsCoordinatesOK(field.cursor.Position.X + speed.X, field.cursor.Position.Y + speed.Y))
                    {
                        field.cursor.Position = new System.Drawing.Point(field.cursor.Position.X + speed.X, field.cursor.Position.Y + speed.Y);
                    }
                    break;
                case Key.Z:
                case Key.X:
                    if (field.CheckEmptyPoint(new System.Drawing.Point(field.cursor.Position.X, field.cursor.Position.Y)))
                    {
                        field[field.cursor.Position.X, field.cursor.Position.Y] = keyModels[key];
                    }
                    break;
                case Key.C:
                    if (field.CheckSlash(new System.Drawing.Point(field.cursor.Position.X, field.cursor.Position.Y)))
                    {
                        field[field.cursor.Position.X, field.cursor.Position.Y] = keyModels[key];
                    }
                    break;
                default:
                    field.cursor.Position = new System.Drawing.Point(field.cursor.Position.X, field.cursor.Position.Y);
                    break;
            }
        }
    }
}
