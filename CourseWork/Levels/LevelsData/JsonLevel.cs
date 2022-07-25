using System.Drawing;
using CourseWork.Models.Abstract;

namespace CourseWork.Levels.LevelsData
{
    [Serializable]
    public class JsonLevel
    {
        public int LevelId { get; set; }
        public char[,] CharField { get; set; }
        public Point BallPosition { get; set; }
        public int Seconds { get; set; }
        public int MagicBalls { get; set; }
        public List<BasicModel>? Teleports { get; set; }//добавить в модели
    }
}
