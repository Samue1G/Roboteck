using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Roboteck.Classes
{
    class Hinge
    {
        public  static Canvas[]           ItemHinge = new Canvas[10000];
        private static int                HingeId { get; set; }
        private static bool[]             isConnect = new bool[10000];
        private static Point              point;
        public Hinge(int id, double wPosition)
        {
            HingeId = id;
            Item.ItemCanvas[HingeId].Children.Remove(ItemHinge[HingeId]);
            ItemHinge[HingeId] = new Canvas()
            {
                RenderTransformOrigin = new Point(0.5, 0.5),
                Height = 24,
                Width = 24,
                Margin = new Thickness(wPosition, 0, 0, 0)
            };
            isConnect[HingeId] = false;
            point = new Point(wPosition, 0);
            Panel.SetZIndex(ItemHinge[HingeId], 2);
            Item.ItemCanvas[HingeId].Children.Add(ItemHinge[HingeId]);
        }

        public void SetConnect(int id, bool Connect)
        {
            isConnect[id] = Connect;
        }

        public bool GetConnect(int id)
        {
            return isConnect[id];
        }

        public Point GetPosition(int id)
        {
            point = ItemHinge[id].PointToScreen(new Point(0,0));
            return point;
        }

        public void SetRotation(int id, double value)
        {
            ItemHinge[id].RenderTransform = new RotateTransform(value);
        }

    }
}
