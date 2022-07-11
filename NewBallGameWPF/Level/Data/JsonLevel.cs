using System;
using System.Collections.Generic;
using System.Drawing;
using NewBallGameWPF.Models.Abstract;

namespace NewBallGameWPF.Level.Data
{
    [Serializable]
    public class JsonLevel
    {
        public int LevelId { get; set; }
        public char[,] CharField { get; set; }
        public Point BallPosition { get; set; }
        public int Seconds { get; set; }
        public int MagicBalls { get; set; }
        public List<Basic>? Teleports { get; set; }//добавить в модели
    }
}
