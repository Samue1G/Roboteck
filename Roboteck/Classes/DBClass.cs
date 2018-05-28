using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Roboteck.Classes
{
    public class DBClass
    {       
        private List<User> _userInformation;
        private List<RobotsImages> _userWorks;
        private static string _nowName;
        private bool _registrationStatus = false;
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\RoboteckBase.mdf';Integrated Security=True";
        private Canvas roboImage;
        private Label name;
        public DBClass()
        {
            GetUserInformation();
        }
              
        public bool GetEnterStatus(string login, string password)
        {
            bool flag = false;
            foreach (User item in _userInformation)
            {
                if (login == item._login && password == item._password)
                {
                    flag = true;
                    _nowName = login;
                }
            }
            return flag;
        }
        public string GetNowName()
        {
            string name = "";
            foreach(var item in _userInformation)
            {
                if(item._login == _nowName)
                {
                    name = _nowName;
                }
            }
            return name;
        }
        public void GetUserInformation()
        {
            SqlDataReader sqlDataReader = null;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                _userInformation = new List<User>();
                connection.Open();
                _userInformation.Clear();
                SqlCommand command2 = new SqlCommand();
                command2.Connection = connection;
                command2.CommandText = @"SELECT * FROM[Users]";
                sqlDataReader = command2.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    _userInformation.Add(new User(sqlDataReader["login"].ToString(), sqlDataReader["password"].ToString()));
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }           
        }
        public void SetUserInformation(string login, string password)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            connection.Open();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO [Users] VALUES (@login, @password)";
            command.Parameters.Add("@login", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@password", SqlDbType.NVarChar, 50);
            command.Parameters["@login"].Value = login;
            command.Parameters["@password"].Value = password.GetHashCode().ToString();
            bool flag = false;
            foreach(User item in _userInformation)
            {
                if (item._login == login)
                {
                    flag = true;
                }
            }

            if (flag == true)
            {
                MessageBox.Show("Такой пользователь уже существует",
                    "Error 2", MessageBoxButton.OK);
                _registrationStatus = false;
            }
            else
            {
                command.ExecuteNonQuery();                
                connection.Close();
                _registrationStatus = true;
            }
        }
        public bool GetRegistratorStatus()
        {
            return _registrationStatus;
        }

        public void DeleteAccount()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command2 = new SqlCommand();
            command2.Connection = connection;
            command2.CommandText = @"Delete [Assemblies] WHERE [USERNAME] = @USERNAME";
            command2.Parameters.Add("@USERNAME", SqlDbType.NVarChar, 50);
            command2.Parameters["@USERNAME"].Value = _nowName;
            command2.ExecuteNonQuery();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"Delete [Users] WHERE [login] = @login";
            command.Parameters.Add("@login", SqlDbType.NVarChar, 50);
            command.Parameters["@login"].Value = _nowName;
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Аккаунт удален");
            Environment.Exit(0);
        }

        public void SaveRobot(Byte[] img, string filename)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string commandText = "INSERT INTO [Assemblies] (USERNAME, WORKNAME, ROBOT) VALUES(@USERNAME, @WORKNAME, @ROBOT)";
            SqlCommand command = new SqlCommand(commandText, connection);
            command.Parameters.Add("@USERNAME", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@WORKNAME", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@ROBOT", SqlDbType.Image);
            command.Parameters["@USERNAME"].Value = _nowName;
            command.Parameters["@WORKNAME"].Value = filename;
            command.Parameters["@ROBOT"].Value = img;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Label loadNameWork(int id)
        {
            name = new Label()
            {
                Margin = new Thickness(200, 20, 0, 0),
                Width = 200,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Content = _userWorks[id].name,
                FontSize = 32,
                Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(85, 40, 14)),
                FontFamily = new System.Windows.Media.FontFamily("Georgia")
            };
            return name;
        }
        public void loadWorksInList()
        {
            SqlDataReader sqlDataReader = null;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                _userWorks = new List<RobotsImages>();
                connection.Open();
                _userWorks.Clear();
                SqlCommand command2 = new SqlCommand();
                command2.Connection = connection;
                command2.CommandText = @"SELECT * FROM[Assemblies]";
                sqlDataReader = command2.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["USERNAME"].ToString() == _nowName)
                        _userWorks.Add(new RobotsImages(sqlDataReader["WORKNAME"].ToString(), (byte[])sqlDataReader["ROBOT"]));
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                connection.Close();
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
        }
        public int GetCountWorks()
        {
            loadWorksInList();
            return _userWorks.Count;
        }

        public Bitmap CutImage(Bitmap src)
        {

            Bitmap bmp = new Bitmap(953, 703); //создаем битмап

            Rectangle cropArea = new Rectangle(250,100, 953, 703);
            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, cropArea, GraphicsUnit.Pixel); //перерисовываем с источника по координатам

            return bmp;
        }

        public Canvas LoadRobots(int id)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(_userWorks[id].bytes))
            {
                bmp = new Bitmap(ms);
            }
            Bitmap targetImage = CutImage(bmp);
            ImageSourceConverter c = new ImageSourceConverter();
            
            ImageBrush myBrush = new ImageBrush();
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(targetImage.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            myBrush.ImageSource = bitmapSource;
            roboImage = new Canvas()
            {
                Margin = new Thickness(100, 0, 0, 0),
                Height = 600,
                Width = 800,
                VerticalAlignment = VerticalAlignment.Top,
                Background = myBrush
            };
            return roboImage;

        }
    }
}
