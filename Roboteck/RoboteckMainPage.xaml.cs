using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using System;
using System.Windows.Media.Imaging;

namespace Roboteck
{
    public partial class RoboteckMainPage : Window
    {
        public static Frame MenuBlock;
        public static Frame gameFrame;
        public static Frame settingsFrame;
        public static RegistrationPage startRegistration;
        public static Grid bOne;
        public static Grid bTwo;
        public static Grid bThree;
        public static Grid bFour;
        public RoboteckMainPage()
        {
            InitializeComponent();
            startRegistration = new RegistrationPage();
            bOne = new Grid
            {
                Height = 650,
                Width = 810,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterLeft.png"))),
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 0, 950, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            bTwo = new Grid
            {
                Height = 650,
                Width = 810,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterRight.png"))),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(950, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center
            };
            bThree = new Grid
            {
                Height = 345,
                Width = 1920,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterTop.png"))),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, -90, 0, 0)
            };
            bFour = new Grid
            {
                Height = 345,
                Width = 1920,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterBottom.png"))),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, -90)
            };
            MenuBlock = new Frame()
            {
                Margin = new Thickness(0, 10, 0, 0),
                Width = 300,
                Height = 400,
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
            };
            MenuBlock.NavigationService.Navigate(startRegistration);
            MainPage.Children.Add(MenuBlock);
            MainPage.Children.Add(bOne);
            MainPage.Children.Add(bTwo);
            MainPage.Children.Add(bThree);
            MainPage.Children.Add(bFour);


            settingsFrame = new Frame()
            {
                Margin = new Thickness(650, 254, 0, 0),
                Width = 300,
                Height = 392,
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Visibility = Visibility.Hidden
            };
            settingsFrame.NavigationService.Navigate(new SettingPage());
            MainPage.Children.Add(settingsFrame);

            gameFrame = new Frame
            {
                Margin = new Thickness(0, 0, 0, 0),
            };
            gameFrame.NavigationService.Navigate(new Game());
            
            MainPage.Children.Add(gameFrame);
            gameFrame.Visibility = Visibility.Hidden;

        }        
    }
}
