using CourseWork.Actions;
using CourseWork.Levels;
using CourseWork.Levels.LevelsData;
using System.Drawing;
using Newtonsoft.Json;

namespace CourseWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameProcess gameProcess = new GameProcess();
            gameProcess.Start();

            //Level level2 = new Level();
            //level2.LevelData = new JsonLevel();
            //level2.LevelData.CharField = new char[30, 30];
            //for (int i = 0; i < 30; i++)
            //{
            //    level2.LevelData.CharField[i, 0] = '#';
            //    level2.LevelData.CharField[i, 29] = '#';
            //}

            //for (int i = 0; i < 30; i++)
            //{
            //    level2.LevelData.CharField[0, i] = '#';
            //    level2.LevelData.CharField[29, i] = '#';
            //}

            //level2.LevelData.CharField[15, 15] = '@';
            //level2.LevelData.LevelId = 2;
            //level2.LevelData.Seconds = 30;
            //level2.LevelData.BallPosition = new Point(1, 1);
            //level2.LevelData.MagicBalls = 1;
            //string serialized = JsonConvert.SerializeObject(level2.LevelData);

            //using (StreamWriter sw = new StreamWriter(@"D:\учеба\курсач\CourseWork\CourseWork\Levels\LevelsData\Level5.json"))
            //{
            //    sw.Write(serialized);
            //}
        }
    }
}






