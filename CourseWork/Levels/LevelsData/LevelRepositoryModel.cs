using System.Drawing;
using CourseWork.Models.Abstract;

namespace CourseWork.Levels.LevelsData
{
    [Serializable]
    public class LevelRepositoryModel
    {
        public int LevelId { get; set; }
        public char[,] RepositoryFieldModel { get; set; }
        public Point BallStartingCoordinates { get; set; }
        public int Seconds { get; set; }
        public int MagicBalls { get; set; }
        public List<BasicModel>? Teleports { get; set; }
    }
}
