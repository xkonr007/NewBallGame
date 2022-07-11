
namespace NewBallGameWPF.Models.Abstract
{
    public class Basic
    {
        public char ArrayChar { get; set; }
        public string ImagePath { get; set; }

        public Basic(char arrayChar, string imagePath)
        {
            ArrayChar = arrayChar;
            ImagePath = imagePath;
        }
    }
}
