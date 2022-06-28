using CourseWork.Models;
using CourseWork.Field;
using CourseWork.Models.Abstract;
using CourseWork.Levels.LevelsData;
using Newtonsoft.Json;

namespace CourseWork.Levels
{
    public class Level 
    { 
        public FieldModel LevelField { get; set; }
        public LevelRepositoryModel LevelData { get; set; }

        public void GetLevelField()
        {
            LevelField = new FieldModel(LevelData.RepositoryFieldModel.GetLength(0),LevelData.RepositoryFieldModel.GetLength(01));
            for (int i = 0; i < LevelField.Width;i++)
            {
                for (int j = 0;j < LevelField.Height; j++)
                {
                    LevelField[i,j] = CreateFieldElement(LevelData.RepositoryFieldModel[i,j]);
                }
            }
            LevelField.ball = new BallModel() { Position = new System.Drawing.Point(LevelData.BallStartingCoordinates.X, LevelData.BallStartingCoordinates.Y) };
            LevelField[LevelField.ball.Position.X, LevelField.ball.Position.Y] = LevelField.ball;
            LevelField.MagicBalls = LevelData.MagicBalls;
        }

        private BasicModel CreateFieldElement(char element)
        {
            switch (element)
            {
                case '#':
                    return new BrickModel();
                case '/':
                    return new ForwardSlashShieldModel();
                case '\\':
                    return new BackslashShieldModel();
                case '@':
                    return new MagicPillModel();
                default:
                    return new EmptyModel();
            }
        }

        public static Level GetLevel(int number)
        {
            using (var reader = new StreamReader(@$"D:\учеба\курсач\CourseWork\CourseWork\Levels\LevelsData\Level{number}.json"))
            {
                var deserializedLevel = JsonConvert.DeserializeObject<LevelRepositoryModel>(reader.ReadToEnd());
                Level level = new Level();
                level.LevelData = deserializedLevel;
                return level;
            }
        }
    }
}
