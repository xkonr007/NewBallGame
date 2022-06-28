using CourseWork.Models.Abstract;
using CourseWork.Models;
using System.Drawing;

namespace CourseWork.Field.Abstract
{
    public abstract class AbstractField 
    {
        public BasicModel[,]? field;
        public BallModel ball;
        public Cursor cursor;
        public int Width { get; set; }
        public int Height { get; set; }
        public int MagicBalls { get; set; }
        public BasicModel this[int x, int y]
        {
            get
            {
                if (IsCoordinatesOK(x, y))
                {
                    return field[x, y];
                }
                else
                {
                    return null;
                }  
            }
            set
            {
                if (IsCoordinatesOK(x, y))
                {
                    field[x, y] = value;
                }
            }
        }

        public AbstractField(int width, int height)
        {
            this.field = new BasicModel[width, height];
            this.Width = width;
            this.Height = height;
        }

        public void PrintField()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Console.SetCursorPosition(x, y);
                    field[x, y].Draw();
                }
            }
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
                MakeStep();
                cursor.Draw();
            }
            else
            {
                if (CheckNextPoint())
                {
                    var coordinatesToClear = ball.Position;
                    MakeStep();
                    RedrawElement(coordinatesToClear);
                }
                else
                {
                    var coordinatesToClear = ball.Position;
                    GetSpeed(field[ball.Position.X + ball.SpeedX, ball.Position.Y + ball.SpeedY]);
                    RedrawElement(coordinatesToClear);
                }
            }
        }
    
        private void MakeStep()
        {
            SwitchCells(new Point(ball.Position.X, ball.Position.Y), new Point(ball.Position.X + ball.SpeedX, ball.Position.Y + ball.SpeedY));
        }

        private void ClearCell(Point point)
        {
            field[point.X, point.Y] = new EmptyModel();
        }

        private bool CheckSlash(Point point)
        {
            return field[point.X, point.Y] is BackslashShieldModel || field[point.X, point.Y] is ForwardSlashShieldModel;
        }

        private void GetSpeed(BasicModel nextPoint)
        {
            switch (nextPoint)
            {
                case BrickModel:
                    ball.SpeedX = -ball.SpeedX;
                    ball.SpeedY = -ball.SpeedY;
                    MakeStep();
                    break;

                case BackslashShieldModel:
                    BackslashShieldTurn();
                    break;

                case ForwardSlashShieldModel:
                    ForwardSlashShieldTurn();
                    break;
            }
        }

        private bool IsCoordinatesOK(int x ,int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private bool CheckNextPoint()
        {
            var nextPointModel = field[ball.Position.X+ball.SpeedX,ball.Position.Y+ball.SpeedY];
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

        private void BackslashShieldTurn()
        {
            int[] ballSpeed = { ball.SpeedX, ball.SpeedY };
            var coordinates = ball.Position;
            var temp = ballSpeed[0];
            ball.SpeedX = ballSpeed[1];
            ball.SpeedY = temp;
            var sumSpeed = ball.SpeedX + ball.SpeedY;
            if (CheckBrick(new Point(ball.Position.X + sumSpeed, ball.Position.Y + sumSpeed)))
            {
                ball.SpeedX = -ballSpeed[0];
                ball.SpeedY = -ballSpeed[1];
                MakeStep();
                ClearCell(coordinates);
            }
            else
            {
                SwitchCells(coordinates, new Point(ball.Position.X + sumSpeed, ball.Position.Y + sumSpeed));
                ClearCell(coordinates);
            }
       
        }

        private void ForwardSlashShieldTurn()
        {
            int[] ballSpeed = { ball.SpeedX, ball.SpeedY };
            var coordinates = ball.Position;
            var temp = ballSpeed[0];
            ball.SpeedX = -ballSpeed[1];
            ball.SpeedY = -temp;
            var sumSpeed = ball.SpeedX - ball.SpeedY;
            if (CheckBrick(new Point(ball.Position.X + sumSpeed, ball.Position.Y - sumSpeed)))
            {
                ball.SpeedX = -ballSpeed[0];
                ball.SpeedY = -ballSpeed[1];
                MakeStep();
                ClearCell(coordinates);
            }
            else
            {
                SwitchCells(new Point(ball.Position.X, ball.Position.Y), new Point(ball.Position.X + sumSpeed, ball.Position.Y - sumSpeed));
                ClearCell(coordinates);
            }
        }

        private void SwitchCells( Point point1,Point point2)
        {
            var temp = field[point1.X, point1.Y];
            field[point1.X, point1.Y] = new EmptyModel();
            field[point2.X, point2.Y] = temp;
            ball.Position = point2;
        }

        private ConsoleKey ReadMovement()
        {
            if (!Console.KeyAvailable)
            {
                return ConsoleKey.S;
            }
            else
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow: return ConsoleKey.UpArrow;
                    case ConsoleKey.DownArrow: return ConsoleKey.DownArrow;
                    case ConsoleKey.LeftArrow: return ConsoleKey.LeftArrow;
                    case ConsoleKey.RightArrow: return ConsoleKey.RightArrow;
                    case ConsoleKey.Z: return ConsoleKey.Z;
                    case ConsoleKey.X: return ConsoleKey.X;
                    case ConsoleKey.C: return ConsoleKey.C;
                    default: return ConsoleKey.S;
                }
            }
        }

        public void MoveCursor()
        {
            var key = ReadMovement();

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (IsCoordinatesOK(cursor.Position.X, cursor.Position.Y - 1))
                    {
                        RedrawElement(cursor.Position);
                        cursor.Position = new Point(cursor.Position.X, cursor.Position.Y - 1);
                        cursor.Draw();

                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (IsCoordinatesOK(cursor.Position.X, cursor.Position.Y +1))
                    {
                        RedrawElement(cursor.Position);
                        cursor.Position = new Point(cursor.Position.X, cursor.Position.Y + 1);
                        cursor.Draw();
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (IsCoordinatesOK(cursor.Position.X - 1, cursor.Position.Y))
                    {
                        RedrawElement(cursor.Position);
                        cursor.Position = new Point(cursor.Position.X- 1, cursor.Position.Y);
                        cursor.Draw();
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (IsCoordinatesOK(cursor.Position.X + 1, cursor.Position.Y))
                    {
                        RedrawElement(cursor.Position);
                        cursor.Position = new Point(cursor.Position.X+1, cursor.Position.Y);
                        cursor.Draw();
                    }
                    break;
                case ConsoleKey.Z:
                    if (CheckEmptyPoint(new Point(cursor.Position.X, cursor.Position.Y)))
                    {
                        field[cursor.Position.X, cursor.Position.Y] = new BackslashShieldModel();  
                    }
                    break;
                case ConsoleKey.X:
                    if (CheckEmptyPoint(new Point(cursor.Position.X, cursor.Position.Y)))
                    {
                        field[cursor.Position.X, cursor.Position.Y] = new ForwardSlashShieldModel();
                    }
                    break;
                case ConsoleKey.C:
                    if (CheckSlash(new Point(cursor.Position.X, cursor.Position.Y)))
                    {
                        ClearCell(new Point(cursor.Position.X, cursor.Position.Y));
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
