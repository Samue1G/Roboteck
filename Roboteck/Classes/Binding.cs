using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Roboteck.Classes
{
    class Binding
    {
        public  static Canvas[]         ItemBinding = new Canvas[10000];
        private static int                  BindingId { get; set; }
        private static bool[]                 isConnect = new bool[10000];
        private static Point                point;
        private static bool[] _availability = new bool[10000];
        public Binding(int id, double wPosition, double hPosition)
        {
            BindingId = id;
            isConnect[BindingId] = false;
            Item.ItemCanvas[BindingId].Children.Remove(ItemBinding[BindingId]);
            ItemBinding[BindingId] = new Canvas()
            {
                RenderTransformOrigin = new Point(0.5, 0.5),
                Height = 24,
                Width = 24,
                Margin = new Thickness(wPosition, hPosition, 0, 0)
            };
            point = new Point(wPosition, hPosition);
            Panel.SetZIndex(ItemBinding[BindingId], 2);
            Item.ItemCanvas[BindingId].Children.Add(ItemBinding[BindingId]);
        }

        public void SetAvailability(int id, bool flag)
        {
            _availability[id] = flag;
        }

        public bool GetAvailability(int id)
        {
            return _availability[id];
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
            point = ItemBinding[id].PointToScreen(new Point(0, 0));
            return point;
        }

        public void SetRotation(int id, double value)
        {
            ItemBinding[id].RenderTransform = new RotateTransform(value);
        }
    }
}
