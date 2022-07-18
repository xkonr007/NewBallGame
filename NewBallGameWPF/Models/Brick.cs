using NewBallGameWPF.Models.Abstract;

namespace NewBallGameWPF.Models
{
    public class Brick : Basic
    {
        public Brick(char arrayChar = '#', string imagePath = @"images\Brick.png") : base(arrayChar, imagePath) { }
    }
}
