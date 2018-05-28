using System;
using Roboteck.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media.Animation;

namespace Roboteck
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        public  static Canvas       playArea;                                       // зона сборки
        private static Canvas       _spawnArea;                                     // косметический элемент зоны спана деталей
        public static Canvas        _rightPiston;
        public static Canvas        _rightLittlePiston;
        public static Canvas        _sliderPiston;
        public static Canvas        _controlsPiston;
        public static ScrollViewer  _scrollingItemArea;                             // "скроллинг" панели деталей
        private static StackPanel   _itemSelectionArea;                             // зона выбора деталей
        private static Canvas[]     _selectableItemWindow = new Canvas[10000];      // массив окон, выбираемых деталей
        private static Canvas[]     _selectabbleBodyWindow = new Canvas[10000];
        private static Canvas[]     _fillingSelectedBodyWindow = new Canvas[10000];
        private static Canvas[]     _fillingSelectedWindow = new Canvas[10000];     // массив визуальных вспомогательных контент-окон
        private static Item[]       ArrayItem = new Item[10000];                    // вспомогательный класс массива деталей
        public static Slider        _itemRotation;                                  // изменение угла детали
        public static Button        _deleteItem;
        public static Button        _saveRobot;
        public static Button        _loadRobot;
        public static Button        _exitButton;
        public static TextBox       _robotname;
        public static  Body         body;
        public static Canvas        Gear;
        public static Canvas        ControlsBack;
        private static int          _spawnSelectionItemIndex;                       // индекс детали для спавна
        private static int          _spawnSelectionBodyIndex;
        private static int          _costItems = 0;                                 // счетик деталей, отсчет с 0
        private static int          _selectNowItemIndex;                            // индекс текущей выбранной детали 
        public  static bool bodyCreate = false;
        public Game()
        {
            InitializeComponent();
            CreateWorkArea();
            CreateControlsArea();
            CreatePistons();
            CreateGear();
        }

        // создание сборочной зоны
        private void CreateWorkArea()
        {
            playArea = new Canvas()
            {   
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\PlayAreaBack.png"))),
                Margin = new Thickness(1600, 100, 0, 0), //150 100 0 0
                Height = 703,
                Width = 953,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            GameArea.Children.Add(playArea);

            _spawnArea = new Canvas()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\back.png"))),
                Margin = new Thickness(50, 200, 0, 0),
                Height = 150,
                Width = 86, 
            };
            playArea.Children.Add(_spawnArea);
        }

        //Поршни
        private void CreatePistons()
        {
            _rightPiston = new Canvas()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Piston.png"))),
                Margin = new Thickness(2553, 334, 0, 0),
                Height = 235,
                Width = 500,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _rightLittlePiston = new Canvas()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Piston.png"))),
                Margin = new Thickness(1700, 100, 0, 0),
                Height = 100,
                Width = 365,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _sliderPiston = new Canvas()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\PistonVertical.png"))),
                Margin = new Thickness(250, 910, 0, 0),
                Height = 390,
                Width = 125,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _controlsPiston = new Canvas()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\PistonVertical.png"))),
                Margin = new Thickness(961, 1180, 0, 0),
                Height = 465,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            GameArea.Children.Add(_rightPiston);
            GameArea.Children.Add(_rightLittlePiston);
            GameArea.Children.Add(_sliderPiston);
            GameArea.Children.Add(_controlsPiston);
        }
        private void CreateGear()
        {
            Gear = new Canvas()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\gear.png"))),
                Margin = new Thickness(-100, -100, 0, 0),
                Height = 200,
                Width = 200,
                RenderTransformOrigin = new Point(0.5, 0.5),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            RotateTransform d = new RotateTransform(0);
            Gear.RenderTransform = d;
            GameArea.Children.Add(Gear);
        }
        public static void GearAnim()
        {
           Gear.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 240,
                Duration = TimeSpan.FromSeconds(2.5)
            });
        }
        //создание группы элементов управления
        private void CreateControlsArea()
        {
            _exitButton = new Button()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\exit.png"))),
                BorderBrush = null,
                Width = 100,
                Height = 50,
                Margin = new Thickness(1600, 125, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _exitButton.Click += _exitButton_Click;
            GameArea.Children.Add(_exitButton);

            ControlsBack = new Canvas()
            {
                Width = 282,
                Height = 82,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\ControlsBack.png"))),
                Margin = new Thickness(920, 900, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _robotname = new TextBox()
            {
                Width = 128,
                Height = 28,
                Background = Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\NameBack.png"))),
                Margin = new Thickness(12, 12, 0, 0),
                BorderBrush = null,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 18,
                Foreground = new SolidColorBrush(Color.FromRgb(85, 40, 14)),
                FontFamily = new FontFamily("Georgia")
            };
            ControlsBack.Children.Add(_robotname);

            _saveRobot = new Button()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\SaveButton.png"))),
                Width = 128,
                Height = 28,
                BorderBrush = null,
                Margin = new Thickness(144, 12, 0, 0), // 1035 1080
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _saveRobot.Click += SaveRobot;
            ControlsBack.Children.Add(_saveRobot);

            _deleteItem = new Button()
            {
                Margin = new Thickness(12, 44, 0, 0), // 900 1080
                Width = 128,
                Height = 28,
                BorderBrush = null,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DeleteButton.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _deleteItem.Click += DeleteItem;
            ControlsBack.Children.Add(_deleteItem);

            _loadRobot = new Button()
            {
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\LoadButton.png"))),
                Width = 128,
                Height = 28,
                BorderBrush = null,
                Margin = new Thickness(144, 44, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _loadRobot.Click += LoadRobot;
            ControlsBack.Children.Add(_loadRobot);
            GameArea.Children.Add(ControlsBack);
            //создание слайдера
            _itemRotation = new Slider()
            {
                Width = 125,
                Height = 20,
                Margin = new Thickness(250, 900, 0, 0), //150 800 0 0
                Minimum = -180,
                Maximum = 180,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemRotation.ValueChanged += RotationValueSlider;
            GameArea.Children.Add(_itemRotation);

            // создание массива окон деталей
            _scrollingItemArea = new ScrollViewer()
            {
                Width = 140,
                Height = 900,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                Margin = new Thickness(100, -900, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            GameArea.Children.Add(_scrollingItemArea);

            _itemSelectionArea = new StackPanel()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Height = 1090,
                Width = 131,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\AllDetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _scrollingItemArea.Content = _itemSelectionArea;

            //первая деталь
            _selectableItemWindow[0] = new Canvas()
            {
                Name = "i0",
                Margin = new Thickness(21, 10, 0, 0),
                Height = 100,
                Width = 100,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemSelectionArea.Children.Add(_selectableItemWindow[0]);
            _fillingSelectedWindow[0] = new Canvas()
            {
                Margin = new Thickness(35.25, 23.75, 0, 0),
                Height = 52.5,
                Width = 29.5,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Detail_Average.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _selectableItemWindow[0].Children.Add(_fillingSelectedWindow[0]);
            _selectableItemWindow[0].MouseDown += SpawnItem;

            //вторая деталь
            _selectableItemWindow[1] = new Canvas()
            {
                Name = "i1",
                Margin = new Thickness(21, 10, 0, 0),
                Height = 100,
                Width = 100,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemSelectionArea.Children.Add(_selectableItemWindow[1]);
            _fillingSelectedWindow[1] = new Canvas()
            {
                Margin = new Thickness(35.25, 13, 0, 0),
                Height = 74,
                Width = 29.5,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Detail_Long.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _selectableItemWindow[1].Children.Add(_fillingSelectedWindow[1]);
            _selectableItemWindow[1].MouseDown += SpawnItem;

            //третья деталь
            _selectableItemWindow[2] = new Canvas()
            {
                Name = "i2",
                Margin = new Thickness(21, 10, 0, 0),
                Height = 100,
                Width = 100,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemSelectionArea.Children.Add(_selectableItemWindow[2]);
            _fillingSelectedWindow[2] = new Canvas()
            {
                Margin = new Thickness(35.25, 30.5, 0, 0),
                Height = 39,
                Width = 29.5,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Detail_Short.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _selectableItemWindow[2].Children.Add(_fillingSelectedWindow[2]);
            _selectableItemWindow[2].MouseDown += SpawnItem;

            //четвертая деталь
            _selectableItemWindow[3] = new Canvas()
            {
                Name = "i3",
                Margin = new Thickness(21, 10, 0, 0),
                Height = 100,
                Width = 100,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemSelectionArea.Children.Add(_selectableItemWindow[3]);
            _fillingSelectedWindow[3] = new Canvas()
            {
                Margin = new Thickness(39.875, 14.5625, 0, 0),
                Height = 70.875,
                Width = 20.25,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Drill.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _selectableItemWindow[3].Children.Add(_fillingSelectedWindow[3]);
            _selectableItemWindow[3].MouseDown += SpawnItem;

            //пятая деталь
            _selectableItemWindow[4] = new Canvas()
            {
                Name = "i4",
                Margin = new Thickness(21, 10, 0, 0),
                Height = 100,
                Width = 100,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemSelectionArea.Children.Add(_selectableItemWindow[4]);
            _fillingSelectedWindow[4] = new Canvas()
            {
                Margin = new Thickness(39.875, 14.5625, 0, 0),
                Height = 70.875,
                Width = 20.25,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Spike.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _selectableItemWindow[4].Children.Add(_fillingSelectedWindow[4]);
            _selectableItemWindow[4].MouseDown += SpawnItem;

            //первое тело
            _selectabbleBodyWindow[0] = new Canvas()
            {
                Name = "i0",
                Margin = new Thickness(21, 10, 0, 0),
                Height = 100,
                Width = 100,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\DetailsBack.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _itemSelectionArea.Children.Add(_selectabbleBodyWindow[0]);
            _fillingSelectedBodyWindow[0] = new Canvas()
            {
                Margin = new Thickness(20, 25, 0, 0),
                Height = 50,
                Width = 60,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\Body.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            _selectabbleBodyWindow[0].Children.Add(_fillingSelectedBodyWindow[0]);
            _selectabbleBodyWindow[0].MouseDown += SpawnBody;
        }

        private void _exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // спавн детали
        private void SpawnItem(object sender, MouseButtonEventArgs e)
        {
            string getIndex = sender.GetType().GetProperties()[14].GetValue(sender, null).ToString();
            getIndex = getIndex.Remove(0, 1);
            _spawnSelectionItemIndex = Convert.ToInt32(getIndex);
            ArrayItem[_costItems] = new Item(_costItems);
            ArrayItem[_costItems].CreateItem(_spawnSelectionItemIndex);
            ArrayItem[_costItems].SetCreateStatus(false);
            Item.ItemCanvas[_costItems].MouseMove += MovementItem;
            Item.ItemCanvas[_costItems].MouseDown += PressItem;
            Item.ItemCanvas[_costItems].MouseUp += ReleaseItem;
            Item.ItemCanvas[_costItems].MouseLeave += BackMouse;
        }

        // спавн тела
        private void SpawnBody(object sender, MouseButtonEventArgs e)
        {
            string getIndex = sender.GetType().GetProperties()[14].GetValue(sender, null).ToString();
            getIndex = getIndex.Remove(0, 1);
            _spawnSelectionBodyIndex = Convert.ToInt32(getIndex);
            body = new Body(_spawnSelectionBodyIndex);
            bodyCreate = true;

        }

        // событие выхода мыши за пределы детали
        private void BackMouse(object sender, MouseEventArgs e)
        {
            if (ArrayItem[_selectNowItemIndex].GetMove() == true && ArrayItem[_selectNowItemIndex].GetConnect(_selectNowItemIndex) == false)
            {
                ArrayItem[_selectNowItemIndex].Following(_selectNowItemIndex);
                System.Windows.Point pointerPosition = e.GetPosition(null);
                System.Windows.Point relativePosition = GameArea.TransformToVisual(playArea).Transform(pointerPosition);
                Item.ItemCanvas[_selectNowItemIndex].Margin = new Thickness(relativePosition.X - Item.ItemCanvas[_selectNowItemIndex].ActualWidth / 2, relativePosition.Y - Item.ItemCanvas[_selectNowItemIndex].ActualHeight + ArrayItem[_selectNowItemIndex].GetCenter(_selectNowItemIndex), 0, 0);
            }         
        }

        // событие отпущеной кнопки мыши
        private void ReleaseItem(object sender, MouseButtonEventArgs e)
        {
            ArrayItem[_selectNowItemIndex].SetMove(false);
        }

        // событие нажатия мыши
        private void PressItem(object sender, MouseButtonEventArgs e)
        {
            Panel.SetZIndex(Item.ItemCanvas[_selectNowItemIndex], 1);
            string getname = sender.GetType().GetProperties()[14].GetValue(sender, null).ToString();
            getname = getname.Remove(0, 1);
            _selectNowItemIndex = Convert.ToInt32(getname);
            ArrayItem[_selectNowItemIndex].SetMove(true);
            double value = ArrayItem[_selectNowItemIndex].GetRotation(_selectNowItemIndex);
            _itemRotation.Value = value;
            Panel.SetZIndex(Item.ItemCanvas[_selectNowItemIndex], 2);

        }

        // событие движения мыши
        private void MovementItem(object sender, MouseEventArgs e)
        {          
            if (ArrayItem[_selectNowItemIndex].GetCreateStatus() == false)
            {
                _costItems++;
            }
            ArrayItem[_selectNowItemIndex].SetCreateStatus(true);
            if (ArrayItem[_selectNowItemIndex].GetMove() == true && ArrayItem[_selectNowItemIndex].GetConnect(_selectNowItemIndex) == false)
            {
                ArrayItem[_selectNowItemIndex].Following(_selectNowItemIndex);
                System.Windows.Point pointerPosition = e.GetPosition(null);
                System.Windows.Point relativePosition = GameArea.TransformToVisual(playArea).Transform(pointerPosition);
                Item.ItemCanvas[_selectNowItemIndex].Margin = new Thickness(relativePosition.X - Item.ItemCanvas[_selectNowItemIndex].ActualWidth / 2, relativePosition.Y - Item.ItemCanvas[_selectNowItemIndex].ActualHeight + ArrayItem[_selectNowItemIndex].GetCenter(_selectNowItemIndex), 0, 0);
                ArrayItem[_selectNowItemIndex].SearchConnect(_selectNowItemIndex, _costItems);
            }
        }

        // задача поворота детали
        private void RotationValueSlider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                ArrayItem[_selectNowItemIndex].SetRotation(_selectNowItemIndex, _itemRotation.Value);
                ArrayItem[_selectNowItemIndex].Following(_selectNowItemIndex);
            }
            catch(Exception)
            {
                MessageBox.Show("Создайте деталь",
                    "Error 10", MessageBoxButton.OK);
            }
        }

        // удаление выбранной детали 
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            int getsecondbinding = Item._usedConnection[_selectNowItemIndex];
            int getthirdhinge = Item._usedConnector[_selectNowItemIndex];
            Item._usedConnection[_selectNowItemIndex] = 10000;
            Item._usedConnector[_selectNowItemIndex] = 10000;           
            if (getsecondbinding != 10000)
            {
                if (getsecondbinding >= 4000 && getsecondbinding <=4006) 
                {
                    int index = Item._BodyBind[getsecondbinding-4000];
                    body.SetConnect(index, false);
                    Item._usedConnector[getsecondbinding] = 10000;
                    Item._BodyBind[getsecondbinding-4000] = 4000;
                }
                else
                {
                    Item._binding[getsecondbinding].SetConnect(getsecondbinding, false);
                    Item._usedConnector[getsecondbinding] = 10000;
                }
            }          
            if (getthirdhinge != 10000)
            {
                Item._hinge[getthirdhinge].SetConnect(getthirdhinge, false);
                Item._usedConnection[getthirdhinge] = 10000;
                ArrayItem[getthirdhinge].SetConnect(getthirdhinge, false);
            }
            Item.ItemCanvas[_selectNowItemIndex].Margin = new Thickness(-1000, -1000,0,0);          
        }

        private void SaveRobot(object sender, RoutedEventArgs e)
        {
            if(_robotname.Text.Length==0)
            {
                MessageBox.Show("Блок имени не заполнен",
                    "Error 11", MessageBoxButton.OK);
            }
            else
            {
                try
                {
                    Transform transform = playArea.LayoutTransform;
                    playArea.LayoutTransform = null;
                    Size size = new Size(1600, 900);
                    playArea.Measure(size);
                    playArea.Arrange(new Rect(size));

                    var rtb = new RenderTargetBitmap(
                        (int) size.Width, //width 
                        (int) size.Height, //height 
                        96, //dpi x 
                        96, //dpi y 
                        PixelFormats.Pbgra32 // pixelformat 
                        );
                    rtb.Render(playArea);
                    BitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    byte[] bitmapBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);                       
                        ms.Position = 0;
                        bitmapBytes = ms.ToArray();
                    }
                    DBClass database = new DBClass();
                    database.SaveRobot(bitmapBytes, _robotname.Text);
                    MessageBox.Show("Работа сохранена");
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка загрузки база данных. Перезапустите приложение.",
                   "Error 12", MessageBoxButton.OK);
                }
            }
        }
        private void LoadRobot(object sender, RoutedEventArgs e)
        {
            ResultRobots result = new ResultRobots();
            result.ShowDialog();           
        }
    }
}