using System;
namespace Roboteck.Classes
{
    public class RobotsImages
    {
        public string name { get; set; }
        public Byte[] bytes { get; set; }
        public RobotsImages(string str, Byte[] bt)
        {
            bytes = bt;
            name = str;
        }
    }
}
