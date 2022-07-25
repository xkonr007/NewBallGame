using System.Drawing;
using NewBallGameWPF.Field;
using NewBallGameWPF.Models;
using NewBallGameWPF.Models.Abstract;
using Newtonsoft.Json;
using NewBallGameWPF.Level.Data;
using System.IO;

namespace NewBallGameWPF.Level
{
    public class Level
    {
        public FieldModel Field { get; set; }
        public JsonLevel LevelData { get; set; }

        public void GetLevelField()
        {
            Field = new FieldModel(LevelData.CharField.GetLength(0), LevelData.CharField.GetLength(01));
            for (int i = 0; i < Field.Width; i++)
            {
                for (int j = 0; j < Field.Height; j++)
                {
                    Field[i, j] = CreateFieldElement(LevelData.CharField[i, j]);
                }
            }
            Field.ball = new Ball() { Position = new Point(LevelData.BallPosition.X, LevelData.BallPosition.Y) };
            Field[Field.ball.Position.X, Field.ball.Position.Y] = Field.ball;
            Field.MagicBalls = LevelData.MagicBalls;
            Field.cursor = new GameCursor(new Point(3, 3));
        }

        private Basic CreateFieldElement(char element)
        {
            switch (element)
            {
                case '#':
                    return new Brick();
                case '/':
                    return new Slash();
                case '\\':
                    return new Backslash();
                case '@':
                    return new MagicBall();
                default:
                    return new Empty();
            }
        }

        public static Level GetLevel(int number)
        {
            var path = @$"data\Level{number}.json";
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
