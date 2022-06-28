using CourseWork.Actions;

namespace CourseWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameProcess gameProcess = new GameProcess();
            gameProcess.Start();
        }
    }
}






//Level level2 = new Level();
//level2.LevelData = new LevelRepositoryModel();
//level2.LevelData.RepositoryFieldModel = new char[50, 20];
//for (int i = 0; i < 50; i++)
//{
//    level2.LevelData.RepositoryFieldModel[i, 0] = '#';
//    level2.LevelData.RepositoryFieldModel[i, 19] = '#';
//}

//for (int i = 0; i < 20; i++)
//{
//    level2.LevelData.RepositoryFieldModel[0, i] = '#';
//    level2.LevelData.RepositoryFieldModel[49, i] = '#';
//}

//for(int i = 20; i < 30; i++)
//{
//    for(int j = 5; j < 15; j++)
//    {
//        level2.LevelData.RepositoryFieldModel[i,j] = '@';
//    }
//}
//level2.LevelData.LevelId = 2;
//level2.LevelData.Seconds = 100;
//level2.LevelData.BallStartingCoordinates = new Point(1, 1);
//level2.LevelData.MagicBalls = 100;
//string serialized = JsonConvert.SerializeObject(level2.LevelData);

//using (StreamWriter sw = new StreamWriter(@"D:\учеба\курсач\CourseWork\CourseWork\Levels\LevelsData\Level3.json"))
//{
//    sw.Write(serialized);
//}