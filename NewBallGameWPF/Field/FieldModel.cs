using NewBallGameWPF.Models.Abstract;
using System.Drawing;
using NewBallGameWPF.Models;

namespace NewBallGameWPF.Field
{
    public class FieldModel
    {
        public Basic[,] field;
        public Ball ball;
        public GameCursor cursor;

        public int Width { get; set; }
        public int Height { get; set; }
        public int MagicBalls { get; set; }
        public Basic this[int x, int y]
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
            this.field = new Basic[width, height];
            this.Width = width;
            this.Height = height;
        }

        public void MoveBall()
        {
            if (ball.Position == cursor.Position)
            {
                ball.Move(new Point(ball.DX, ball.DY), field);
                field[ball.Position.X, ball.Position.Y] = ball;
            }
            else
            {
                if (CheckNextPoint())
                {
                    ball.Move(new Point(ball.DX, ball.DY), field);
                    field[ball.Position.X, ball.Position.Y] = ball;
                }
                else
                {
                    GetSpeed(field[ball.Position.X + ball.DX, ball.Position.Y + ball.DY]);
                    field[ball.Position.X, ball.Position.Y] = ball;
                }
            }
        }//extension метод для поинта

        private void GetSpeed(Basic nextPoint)
        {
            switch (nextPoint)
            {
                case Brick:
                    ball.DX = -ball.DX;
                    ball.DY = -ball.DY;
                    ball.Move(new Point(ball.DX, ball.DY), field);
                    field[ball.Position.X, ball.Position.Y] = ball;
                    break;
                case Backslash:
                case Slash:
                    ShieldTurn(nextPoint);
                    field[ball.Position.X, ball.Position.Y] = ball;
                    break;
            }
        }

        public bool IsCoordinatesOK(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool CheckSlash(Point point)
        {
            return field[point.X, point.Y] is Backslash || field[point.X, point.Y] is Slash;
        }

        private bool CheckNextPoint()
        {
            var nextPointModel = field[ball.Position.X + ball.DX, ball.Position.Y + ball.DY];
            if (nextPointModel is MagicBall)
            {
                MagicBalls -= 1;
                
                //SoundPlayer sp = new SoundPlayer();
                //sp.SoundLocation = @"D:\Downloads\msg-1536012805-103197.wav";
                //sp.Load();
                //sp.Play();
            }
            return nextPointModel is MagicBall || nextPointModel is Empty;
        }

        public bool CheckEmptyPoint(Point point)
        {
            return field[point.X, point.Y] is Empty;
        }

        public bool CheckBrick(Point point)
        {
            return field[point.X, point.Y] is Brick;
        }

        private void ShieldTurn(Basic nextPointmodel)
        {
            var ballSpeed = new Point(ball.DX, ball.DY);//кладем в поинт текущие показатели скорости
            var sumSpeed = GetSumSpeed(nextPointmodel, ballSpeed);
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

        private Point GetSumSpeed(Basic nextPointModel, Point ballSpeed)//функция устанавливает дальнейшую скорость мяча в зависимости от типа щита 
        {
            if (nextPointModel is Backslash)
            {
                ball.DX = ballSpeed.Y;
                ball.DY = ballSpeed.X;
                return new Point(ball.DX + ball.DY, ball.DX + ball.DY);//а также возвращает переменную со координатами смещения мяча,при back мяч будет всегда идти по диагонали
            }
            ball.DX = -ballSpeed.Y;
            ball.DY = -ballSpeed.X;
            return new Point(ball.DX - ball.DY, ball.DY - ball.DX);//при forward будет идти по диагонали,но с одной отзеркаленной осью координат в зависимости от начальной скорости
        }
    }
}

