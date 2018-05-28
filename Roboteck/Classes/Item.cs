using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Roboteck.Classes
{
    class Item
    {       
        public  static Canvas[]     ItemCanvas          = new Canvas[10000];        // массив деталей
        public  static Hinge[]      _hinge              = new Hinge[10000];         // массив шарниров
        public  static Binding[]    _binding            = new Binding[10000];       // массив креплений
        private static int          _ItemId;                                        // id объекта
        private static bool         _isMove             = false;                    // флаг движения
        private static bool         _isCreate           = false;                    // флаг создания
        private static bool[]       _isConnect          = new bool[10000];
        private static double[]     _rotation           = new double[10000];        // значение поворота объекта
        private        double       _hingeCenter;                                   // точка вращения
        public  static int[]        _usedConnection     = new int[10000];           // массив используемых подключений (да, я храню значения в точке, очень удобно)
        public  static int[]        _usedConnector      = new int[10000];
        public static int[] _BodyBind = new int[6];
        public Item(){}
        public Item (int id)
        {
            _ItemId = id;
            _usedConnection[_ItemId]= 10000;
            _usedConnector[_ItemId] = 10000;
            _isConnect[_ItemId] = false;
        }

        public void SetCreateStatus(bool status)
        {
            _isCreate = status;
        }

        public bool GetConnect(int id)
        {
            return _isConnect[id];
        }

        public void SetConnect(int id, bool flag)
        {
            _isConnect[id] = flag;
        }

        public void SearchConnect(int id, int cost)
        {
            for (int i = 0; i < cost; i++)
            {
                if (_binding[i].GetAvailability(i) == true)
                {
                    if (_hinge[id].GetPosition(id).X + 12 >= _binding[i].GetPosition(i).X && _hinge[id].GetPosition(id).X + 12 <= _binding[i].GetPosition(i).X + 24)
                    {
                        if (_hinge[id].GetPosition(id).Y + 12 >= _binding[i].GetPosition(i).Y && _hinge[id].GetPosition(id).Y + 12 <= _binding[i].GetPosition(i).Y + 24)
                        {
                            if (_hinge[id].GetConnect(id) == false && _binding[i].GetConnect(i) == false)
                            {
                                _hinge[id].SetConnect(id, true);
                                _binding[i].SetConnect(i, true);
                                if (i != _usedConnection[id] && id != _usedConnector[i])
                                {
                                    _usedConnection[id] = i; // записываем к детали с шарниром его порт
                                    _usedConnector[i] = id;   // записываем к детали с портом его шарнир
                                    double XBPosition = _binding[i].GetPosition(i).X - 252.75;
                                    double YBPosition = _binding[i].GetPosition(i).Y - 103;
                                    ItemCanvas[id].Margin = new Thickness(XBPosition, YBPosition, 0, 0);
                                    _isMove = false;
                                    _isConnect[id] = true;
                                }
                            }
                        }
                    }
                }
            }

            if (Game.bodyCreate == true)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (_hinge[id].GetPosition(id).X + 12 >= Game.body.GetPosition(i).X && _hinge[id].GetPosition(id).X + 12 <= Game.body.GetPosition(i).X + 24)
                    {
                        if (_hinge[id].GetPosition(id).Y + 12 >= Game.body.GetPosition(i).Y && _hinge[id].GetPosition(id).Y + 12 <= Game.body.GetPosition(i).Y + 24)
                        {
                            if (Game.body.GetConnect(i) == false)
                            {
                                _hinge[id].SetConnect(id, true);
                                _usedConnection[id] = 4000 + Game.body.binding[i].GetName(i);
                                _BodyBind[Game.body.binding[i].GetName(i)] = Game.body.binding[i].GetName(i);
                                Game.body.SetConnector(Game.body.binding[i].GetName(i), id);
                                Game.body.SetConnect(Game.body.binding[i].GetName(i), true);
                                double XBPosition = Game.body.GetPosition(Game.body.binding[i].GetName(i)).X - 252.75;
                                double YBPosition = Game.body.GetPosition(Game.body.binding[i].GetName(i)).Y - 103;
                                ItemCanvas[id].Margin = new Thickness(XBPosition, YBPosition, 0, 0);
                                _isMove = false;
                                _isConnect[id] = true;
                            }
                        }
                    }
                }
            }
        }
        
        public void Following(int hinge)
        {
            for (int i = 0; i <= _ItemId; i++)
            {
                if (_usedConnector[i] != 10000)
                {
                    double XBPosition = _binding[i].GetPosition(i).X - 252.75;
                    double YBPosition = _binding[i].GetPosition(i).Y- 103;
                    ItemCanvas[_usedConnector[i]].Margin = new Thickness(XBPosition, YBPosition, 0, 0);
                }
            }
        }

        public bool GetCreateStatus()
        {
            return _isCreate;
        }

        public void DeleteItem(int id)
        {
            Game.playArea.Children.Remove(ItemCanvas[id]);
        }

        public double GetRotation(int id)
        {
            return _rotation[id];
        }

        public void SetRotation(int id, double value)
        {
            ItemCanvas[id].RenderTransform= new RotateTransform(value);
            _rotation[id] = value;
            _binding[id].SetRotation(id, -value);
            _hinge[id].SetRotation(id, -value);
        }

        public bool GetMove()
        {
            return _isMove;
        }

        public void SetMove(bool Movement)
        {
            _isMove = Movement;
        }

        public double GetCenter(int id)
        {
            _hingeCenter = ItemCanvas[id].Height - 12;
            return _hingeCenter;
        }

        public void CreateItem(int itemIndex)
        {
            Game.playArea.Children.Remove(ItemCanvas[_ItemId]);
            switch (itemIndex)
            {
                case 0:
                    ItemCanvas[_ItemId] = new Canvas()
                    {
                        Name = "i" + _ItemId.ToString(),
                        Margin = new Thickness(78.25, 248.75, 0, 0),
                        Height = 53.5,
                        Width = 30,
                        RenderTransformOrigin = new Point(0.5, 0.229),
                        Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Detail_Average.png")))
                    };
                    _hinge[_ItemId] = new Hinge(_ItemId, 2.75);
                    _binding[_ItemId] = new Binding(_ItemId, 2.75, 40.5);
                    _binding[_ItemId].SetAvailability(_ItemId, true);
                    Game.playArea.Children.Add(ItemCanvas[_ItemId]);
                    break;
                case 1:
                    ItemCanvas[_ItemId] = new Canvas()
                    {
                        Name = "i" + _ItemId.ToString(),
                        Margin = new Thickness(78.25, 238, 0, 0),
                        Height = 74,
                        Width = 30,
                        RenderTransformOrigin = new Point(0.5, 0.162),
                        Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Detail_Long.png")))
                    };
                    _hinge[_ItemId] = new Hinge(_ItemId, 2.75);
                    _binding[_ItemId] = new Binding(_ItemId, 2.75, 62);
                    _binding[_ItemId].SetAvailability(_ItemId, true);
                    Game.playArea.Children.Add(ItemCanvas[_ItemId]);
                    break;
                case 2:
                    ItemCanvas[_ItemId] = new Canvas()
                    {
                        Name = "i" + _ItemId.ToString(),
                        Margin = new Thickness(78.25, 255.5, 0, 0),
                        Height = 39,
                        Width = 30,
                        RenderTransformOrigin = new Point(0.5, 0.308),
                        Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Detail_Short.png")))
                    };
                    _hinge[_ItemId] = new Hinge(_ItemId, 2.75);
                    _binding[_ItemId] = new Binding(_ItemId, 2.75, 27);
                    _binding[_ItemId].SetAvailability(_ItemId, true);
                    Game.playArea.Children.Add(ItemCanvas[_ItemId]);
                    break;
                case 3:
                    ItemCanvas[_ItemId] = new Canvas()
                    {
                        Name = "i" + _ItemId.ToString(),
                        Margin = new Thickness(76.25, 222.75, 0, 0),
                        Height = 98,
                        Width = 30,
                        RenderTransformOrigin = new Point(0.5, 0.115),
                        Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Drill.png")))
                    };
                    _hinge[_ItemId] = new Hinge(_ItemId, 4.75);
                    _binding[_ItemId] = new Binding(_ItemId, 2.75, 27);
                    _binding[_ItemId].SetConnect(_ItemId, true);
                    Game.playArea.Children.Add(ItemCanvas[_ItemId]);
                    break;
                case 4:
                    ItemCanvas[_ItemId] = new Canvas()
                    {
                        Name = "i" + _ItemId.ToString(),
                        Margin = new Thickness(78, 222.5, 0, 0),
                        Height = 105,
                        Width = 30,
                        RenderTransformOrigin = new Point(0.5, 0.114),
                        Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Spike.png")))
                    };
                    _hinge[_ItemId] = new Hinge(_ItemId, 3);
                    _binding[_ItemId] = new Binding(_ItemId, 2.75, 27);
                    _binding[_ItemId].SetConnect(_ItemId, true);
                    Game.playArea.Children.Add(ItemCanvas[_ItemId]);
                    break;
            }
            Panel.SetZIndex(ItemCanvas[_ItemId], 1);
        }
    }
}
