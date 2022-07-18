using NewBallGameWPF.Models.Abstract;

namespace NewBallGameWPF.Models
{
    public class Slash : Basic
    {
        public Slash(char arrayChar = '/', string imagePath = @"images\Slash.png") : base(arrayChar, imagePath) { }//копирование
    }
}
