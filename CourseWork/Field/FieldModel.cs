using CourseWork.Models.Abstract;
using CourseWork.Models;
using System.Drawing;
using CourseWork.Controllers;

namespace CourseWork.Field
{
    public class FieldModel 
    {
        public BasicModel[,] field;
        public BallModel ball;
        public Cursor cursor;

        public int Width { get; set; }
        public int Height { get; set; }
        public int MagicBalls { get; set; }
        public BasicModel this[int x, int y]
        {
            get
            {
                if (!IsCoordinatesOK(x, y))
                {
                    return null;
                }
                return field[x, y];
            }
            set
            {
                if (IsCoordinatesOK(x, y))
                {
                    field[x, y] = value;
                }
            }
        }

        public FieldModel(int width, int height)
        {
            this.field = new BasicModel[width, height];
            this.Width = width;
            this.Height = height;
        }

        public void RedrawElement(Point point)
        {
            Console.ResetColor();
            Console.SetCursorPosition(point.X, point.Y);
            field[point.X, point.Y].Draw();
        }

        public void MoveBall()
        {
            if (ball.Position == cursor.Position)
            {
                ball.Move(new Point(ball.DX,ball.DY),field);
                cursor.Draw();
            }
            else
            {
                if (CheckNextPoint())
                {
                    var coordinatesToClear = ball.Position;
                    ball.Move(new Point(ball.DX, ball.DY), field);
                    RedrawElement(coordinatesToClear);
                }
                else
                {
                    var coordinatesToClear = ball.Position;
                    GetSpeed(field[ball.Position.X + ball.DX, ball.Position.Y + ball.DY]);
                    RedrawElement(coordinatesToClear);
                }
            }
        }//extension метод для поинта

        private void GetSpeed(BasicModel nextPoint)
        {
            switch (nextPoint)
            {
                case BrickModel:
                    ball.DX = -ball.DX;
                    ball.DY = -ball.DY;
                    ball.Move(new Point(ball.DX, ball.DY), field);
                    break;
                case BackslashShieldModel:
                case ForwardSlashShieldModel:
                    ShieldTurn(nextPoint);
                    break;
            }
        }

        private bool IsCoordinatesOK(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private bool CheckSlash(Point point)
        {
            return field[point.X, point.Y] is BackslashShieldModel || field[point.X, point.Y] is ForwardSlashShieldModel;
        }

        private bool CheckNextPoint()
        {
            var nextPointModel = field[ball.Position.X + ball.DX, ball.Position.Y + ball.DY];
            if (nextPointModel is MagicPillModel)
            {
                MagicBalls -= 1;
            }
            return nextPointModel is MagicPillModel || nextPointModel is EmptyModel;
        }

        private bool CheckEmptyPoint(Point point)
        {
            return field[point.X, point.Y] is EmptyModel;
        }

        private bool CheckBrick(Point point)
        {
            return field[point.X, point.Y] is BrickModel;
        }

        private void ShieldTurn(BasicModel nextPointmodel)
        {
            var ballSpeed = new Point(ball.DX, ball.DY);//кладем в поинт текущие показатели скорости
            var sumSpeed = GetSumSpeed(nextPointmodel,ballSpeed);
            if (CheckBrick(new Point(ball.Position.X + sumSpeed.X, ball.Position.Y + sumSpeed.Y)))
            {
                ball.DX = -ballSpeed.X;
                ball.DY = -ballSpeed.Y;
                ball.Move(new Point(ball.DX, ball.DY), field);
            }
            else
            {
                ball.Move(new Point(sumSpeed.X, sumSpeed.Y), field);
            }
        }

        private Point GetSumSpeed(BasicModel nextPointModel,Point ballSpeed)//функция устанавливает дальнейшую скорость мяча в зависимости от типа щита 
        {
            if (nextPointModel is BackslashShieldModel)
            {
                ball.DX = ballSpeed.Y;
                ball.DY = ballSpeed.X;
                return new Point(ball.DX + ball.DY, ball.DX + ball.DY);//а также возвращает переменную со координатами смещения мяча,при back мяч будет всегда идти по диагонали
            }
                ball.DX = -ballSpeed.Y;
                ball.DY = -ballSpeed.X;
                return new Point(ball.DX - ball.DY, ball.DY - ball.DX);//при forward будет идти по диагонали,но с одной отзеркаленной осью координат в зависимости от начальной скорости
        }

        public void MakeAction()
        {
            var key = MainController.ReadMovement();

            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    var speed = MainController.keySpeed[key];
                    if (IsCoordinatesOK(cursor.Position.X + speed.X, cursor.Position.Y + speed.Y))
                    {
                        RedrawElement(cursor.Position);
                        cursor.Position = new Point(cursor.Position.X + speed.X, cursor.Position.Y + speed.Y);
                        cursor.Draw();
                    }
                    break;
                case ConsoleKey.Z:
                case ConsoleKey.X:
                    if (CheckEmptyPoint(new Point(cursor.Position.X, cursor.Position.Y)))
                    {
                        field[cursor.Position.X, cursor.Position.Y] = MainController.keyModels[key];
                    }
                    break;
                case ConsoleKey.C:
                    if (CheckSlash(new Point(cursor.Position.X, cursor.Position.Y)))
                    {
                        field[cursor.Position.X, cursor.Position.Y] = MainController.keyModels[key];
                        field[cursor.Position.X, cursor.Position.Y].Draw();
                    }
                    break;
                default:
                    cursor.Position = new Point(cursor.Position.X, cursor.Position.Y);
                    break;
            }
        }
    }
}