using NewBallGameWPF.Models.Abstract;

namespace NewBallGameWPF.Models
{
    public class Brick : Basic
    {
        public Brick(char arrayChar = '#', string imagePath = @"\images\wall.png") : base(arrayChar, imagePath)
        {
        }
    }
}
