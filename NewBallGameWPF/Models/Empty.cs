using NewBallGameWPF.Models.Abstract;

namespace NewBallGameWPF.Models
{
    public class Empty : Basic
    {
        public Empty(char arrayChar = ' ', string imagePath = @"\images\empty.png") : base(arrayChar, imagePath)
        {

        }
    }
}
