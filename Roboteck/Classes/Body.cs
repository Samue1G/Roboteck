using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Roboteck
{
    public class Body
    {
        public static Canvas BodyItem = new Canvas();
        public BodyBinding[] binding = new BodyBinding[6];
        public static int[] _usedConnector = new int[6];
        public Body() { }
        public Body(int index)
        {
            Game.playArea.Children.Remove(BodyItem);
            switch (index)
            {
                case 0:
                    BodyItem = new Canvas()
                    {
                        Margin = new Thickness(375, 250, 0, 0),
                        Height = 124,
                        Width = 160,
                        Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Body.png"))),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    Game.playArea.Children.Add(BodyItem);
                    binding[0] = new BodyBinding(0, 10, 110.5);
                    binding[1] = new BodyBinding(1, -10, 50);
                    binding[2] = new BodyBinding(2, 10, -4.5);
                    binding[3] = new BodyBinding(3, 130, 110.5);
                    binding[4] = new BodyBinding(4, 145, 50);
                    binding[5] = new BodyBinding(5, 120, -4.5);
                    break;
            }
            Panel.SetZIndex(BodyItem, 0);
        }

        
        public bool GetConnect(int id)
        {
            return binding[id].GetConnect();
        }

        public void SetConnect(int id, bool flag)
        {
            binding[id].SetConnect(flag);
        }

        public Point GetPosition(int id)
        {
            return binding[id].GetPosition(id);
        }

        public void SetConnector(int id, int connect)
        {
            _usedConnector[id] = connect;
        }

        public int GetConnector(int id)
        {
            return _usedConnector[id];
        }
    }
}
