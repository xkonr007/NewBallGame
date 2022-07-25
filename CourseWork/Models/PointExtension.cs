using System.Drawing;

namespace CourseWork.Models
{
    public static class PointExtension
    {
        public static Point MakeNewPos(this Point point, Point speed)
        {
            return new Point(point.X + speed.X, point.Y+speed.Y);
        }
    }
}
