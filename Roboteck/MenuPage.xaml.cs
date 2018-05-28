using Roboteck.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Roboteck
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private static string username;
        DBClass database = new DBClass();
        public MenuPage()
        {         
            InitializeComponent();            
        }

        public void SetUserName(string name)
        {
            username = name;
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RoboteckMainPage.gameFrame.Visibility = Visibility.Visible;
            ThicknessAnimation animatePlayArea = new ThicknessAnimation
            {
                From = Game.playArea.Margin,
                To = new Thickness(250, Game.playArea.Margin.Top, 0, 0),
                Duration = TimeSpan.FromSeconds(3),
            };
            animatePlayArea.Completed += delegate (object obj, EventArgs ev)
            {
                Game._exitButton.BeginAnimation(MarginProperty, new ThicknessAnimation
                {
                    From = Game._exitButton.Margin,
                    To = new Thickness(1250, Game._exitButton.Margin.Top, 0, Game._exitButton.Margin.Bottom),
                    Duration = TimeSpan.FromSeconds(1),
                });
                Game._rightLittlePiston.BeginAnimation(MarginProperty, new ThicknessAnimation
                {
                    From = Game._rightLittlePiston.Margin,
                    To = new Thickness(1350, Game._rightLittlePiston.Margin.Top, 0, Game._rightLittlePiston.Margin.Bottom),
                    Duration = TimeSpan.FromSeconds(1),
                    AutoReverse = true,
                });
            };
            Game.playArea.BeginAnimation(MarginProperty, animatePlayArea);
            Game._rightPiston.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = Game._rightPiston.Margin,
                To = new Thickness(1203, Game._rightPiston.Margin.Top, 0, Game._rightPiston.Margin.Bottom),
                Duration = TimeSpan.FromSeconds(3),
                AutoReverse = true,
            });
            Game._scrollingItemArea.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = Game._scrollingItemArea.Margin,
                To = new Thickness(Game._scrollingItemArea.Margin.Left, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(2.5)
            });
            Game._itemRotation.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = Game._itemRotation.Margin,
                To = new Thickness(Game._itemRotation.Margin.Left, 820, 0, 0),
                Duration = TimeSpan.FromSeconds(2)
            });
            Game._sliderPiston.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = Game._sliderPiston.Margin,
                To = new Thickness(Game._sliderPiston.Margin.Left, 830, 0, 0),
                Duration = TimeSpan.FromSeconds(2),
                AutoReverse = true
            });
            Game.ControlsBack.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = Game.ControlsBack.Margin,
                To = new Thickness(Game.ControlsBack.Margin.Left, 803, 0, 0),
                Duration = TimeSpan.FromSeconds(2.5)
            });
            Game._controlsPiston.BeginAnimation(MarginProperty, new ThicknessAnimation
            {
                From = Game._controlsPiston.Margin,
                To = new Thickness(Game._controlsPiston.Margin.Left, 890, 0, 0),
                Duration = TimeSpan.FromSeconds(2.5),
                AutoReverse = true
            });
            Game.GearAnim();
        }

        private void MenuPageBlock_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Content = username;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            RoboteckMainPage.MenuBlock.Visibility = Visibility.Hidden;
            RoboteckMainPage.settingsFrame.Visibility = Visibility.Visible;
        }
    }
}
