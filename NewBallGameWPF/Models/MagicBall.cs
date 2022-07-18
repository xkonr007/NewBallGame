using NewBallGameWPF.Models.Abstract;

namespace NewBallGameWPF.Models
{
    public class MagicBall : Basic
    {
        public MagicBall(char arrayChar = '@', string imagePath = @"images\MagicBall.png") : base(arrayChar, imagePath) { }
    }
}
