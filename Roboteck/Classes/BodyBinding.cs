using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Roboteck
{
    public class BodyBinding
    {
        public static Canvas[] bodyBinding = new Canvas[1000];
        private bool _connect = false;
        private Point point;
        public static int[] Name = new int[6];
        public BodyBinding(int id, double wPosition, double hPosition)
        {
            bodyBinding[id] = new Canvas()
            {
                Height = 24,
                Width = 24,
                Margin = new Thickness(wPosition, hPosition, 0, 0)
            };
            Name[id] = id;
            Panel.SetZIndex(bodyBinding[id], 0);
            Body.BodyItem.Children.Add(bodyBinding[id]);
        }

        public int GetName(int id)
        {
            return Name[id];
        }

        public bool GetConnect()
        {
            return _connect;
        }

        public void SetConnect(bool flag)
        {
            _connect = flag;
        }

        public Point GetPosition(int id)
        {
            point = bodyBinding[id].PointToScreen(new Point(0, 0));
            return point;
        }
    }
}
