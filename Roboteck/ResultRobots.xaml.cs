using Roboteck.Classes;
using System.Windows;

namespace Roboteck
{
    /// <summary>
    /// Логика взаимодействия для ResultRobots.xaml
    /// </summary>
    public partial class ResultRobots : Window
    {
        public ResultRobots()
        {
            InitializeComponent();
            DBClass database = new DBClass();
            library.Height = scroller.Height;
            library.Width = scroller.Width;
            database.loadWorksInList();
            for (int i = 0; i < database.GetCountWorks(); i++)
            {
                
                library.Children.Add(database.loadNameWork(i));
                library.Children.Add(database.LoadRobots(i));
            }
            library.Visibility = Visibility.Visible;
        }       

    }
}
