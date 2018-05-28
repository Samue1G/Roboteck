using Roboteck.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Roboteck
{
    /// <summary>
    /// Логика взаимодействия для SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        DBClass database = new DBClass();
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RoboteckMainPage.settingsFrame.Visibility = Visibility.Hidden;
            RoboteckMainPage.MenuBlock.Visibility = Visibility.Visible;
        }

        private void AccDelButton_Click(object sender, RoutedEventArgs e)
        {
            database.DeleteAccount();
        }
    }
}
