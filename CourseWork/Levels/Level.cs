using CourseWork.Models;
using CourseWork.Field;
using CourseWork.Models.Abstract;
using CourseWork.Levels.LevelsData;
using Newtonsoft.Json;
using System.Drawing;

namespace CourseWork.Levels
{
    public class Level 
    { 
        public FieldModel LevelField { get; set; }
        public JsonLevel LevelData { get; set; }

        public void GetLevelField()
        {
            LevelField = new FieldModel(LevelData.CharField.GetLength(0), LevelData.CharField.GetLength(01));
            for (int i = 0; i < LevelField.Width; i++)
            {
                for (int j = 0; j < LevelField.Height; j++)
                {
                    LevelField[i, j] = CreateFieldElement(LevelData.CharField[i, j]);
                }
            }
            LevelField.ball = new BallModel() { Position = new Point(LevelData.BallPosition.X, LevelData.BallPosition.Y) };
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
                case '*':
                    return new DeathTrap();
                default:
                    return new EmptyModel();
            }
        }

        public static Level GetLevel(int number)
        {
            var path = @$"LevelsData\Level{number}.json";
            using (var reader = new StreamReader(path))
            {
                var deserializedLevel = JsonConvert.DeserializeObject<JsonLevel>(reader.ReadToEnd());
                Level level = new Level();
                level.LevelData = deserializedLevel;
                return level;
            }
        }
    }
}
