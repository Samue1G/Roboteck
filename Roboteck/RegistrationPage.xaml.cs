using Roboteck.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Roboteck
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page, IDataErrorInfo
    {
        private static Regex loginRegex = new Regex(@"\w");
        //генерируемые поля
        private static Grid RegisterGroupBox;
        private static Grid LoginGroupBox;
        private static string trueSimbols = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        //программные кнопки
        private static Button SignInButton;
        private static Button SignUpButton;
        //Регистрация
        private static TextBox login;

        private static PasswordBox password;
        private static PasswordBox checkPassword;      
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "loginBox":
                        if (login.Text.Length < 3 && login.Text.Length > 20)
                        {
                            error = "Логин должен содержать от 3 до 20 символов";
                        }
                        else if (!loginRegex.IsMatch(login.Text))
                        {
                            error = "Логин может содержать только буквы алфавита и цифры";
                        }
                        break;
                    case "passwordBox":
                        if (password.Password.Length < 8 )
                        {
                            error = "Пароль должен сожердать минимум 8 символов";
                        }
                        else if (!loginRegex.IsMatch(password.Password))
                        {
                            error = "Пароль может содержать только буквы алфавита и цифры";
                        }
                        break;
                    case "checkPasswordBox":
                        if (checkPassword.Password != password.Password)
                        {
                            error = "Подтверждение пароля не введено/введено неверно";
                        }
                        break;
                }
                return error;
            }
        }
        //Вход
        private static TextBox loginON;
        private static PasswordBox passwordON;
        //вспомогательные классы
        DBClass database;
        public RegistrationPage()
        {
            InitializeComponent();
            database = new DBClass();
            database.GetUserInformation();
            //buttonStyle.Setters.Add(buttonSetter);
        }            
        private void SignIn_OnClick(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation goAway = new ThicknessAnimation
            {
                From = AutoriztionGroupBox.Margin,
                To = new Thickness(AutoriztionGroupBox.Margin.Left, -400, AutoriztionGroupBox.Margin.Right, 400),
                Duration = TimeSpan.FromSeconds(1),
            };
            AutoriztionGroupBox.BeginAnimation(MarginProperty, goAway);
            GenerateLoginGroupBox();
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation goAway = new ThicknessAnimation
            {
                From = AutoriztionGroupBox.Margin,
                To = new Thickness(AutoriztionGroupBox.Margin.Left, 400, AutoriztionGroupBox.Margin.Right, -410),
                Duration = TimeSpan.FromSeconds(1)
            };
            AutoriztionGroupBox.BeginAnimation(MarginProperty, goAway);
            GenerateRegisterGroupBox();
        }
        private void GenerateLoginGroupBox()
        {
            LoginGroupBox = new Grid
            {
                Margin = new Thickness(50, 400, 0, -400),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterBack.png")))
            };
            loginON = new TextBox
            {
                Name = "loginBox",
                Width = 100,
                Height = 50,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\NameBack.png"))),
                Margin = new Thickness(0, 52.5, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 32,
                Foreground = new SolidColorBrush(Color.FromRgb(85, 40, 14)),
                FontFamily = new FontFamily("Georgia"),
                BorderBrush = null
            };

            passwordON = new PasswordBox
            {
                Name = "passwordBox",
                Width = 100,
                Height = 50,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\NameBack.png"))),
                Margin = new Thickness(0, 5, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderBrush = null,
                FontSize = 32,
                Foreground = new SolidColorBrush(Color.FromRgb(85, 40, 14)),
                FontFamily = new FontFamily("Georgia")
            };
            SignInButton = new Button
            {
                Name = "SignInButton",
                Width = 100,
                Height = 50,
                Margin = new Thickness(0, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\SignInButton.png"))),
                BorderBrush = null

                //Style = buttonStyle
            };
            SignInButton.Click += SignIn_Click;

            Button BackButton = new Button
            {
                Name = "BackButton",
                Width = 100,
                Height = 50,
                Margin = new Thickness(0, 30, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\BackButton.png"))),
                BorderBrush = null
            };

            BackButton.Click += delegate (object sender, RoutedEventArgs args)
            {
                ThicknessAnimation goAway = new ThicknessAnimation
                {
                    From = LoginGroupBox.Margin,
                    To = new Thickness(LoginGroupBox.Margin.Left, 400, LoginGroupBox.Margin.Right, -400),
                    Duration = TimeSpan.FromSeconds(1),
                };
                goAway.Completed += delegate (object o, EventArgs eventArgs)
                {
                    AccountWindow.Children.Remove(LoginGroupBox);
                };
                LoginGroupBox.BeginAnimation(MarginProperty, goAway);
                AutoriztionGroupBox.BeginAnimation(MarginProperty, AnimateGrid(AutoriztionGroupBox));
                AnimateGearsDown();
            };

            StackPanel sp = new StackPanel();

            sp.Children.Add(loginON);
            sp.Children.Add(passwordON);
            sp.Children.Add(SignInButton);
            sp.Children.Add(BackButton);
            LoginGroupBox.Children.Add(sp);
            AccountWindow.Children.Add(LoginGroupBox);
            LoginGroupBox.BeginAnimation(MarginProperty, AnimateGrid(LoginGroupBox));
            AnimateGearsUp();
        }

        private void GenerateRegisterGroupBox()
        {
            RegisterGroupBox = new Grid
            {
                Margin = new Thickness(50, -400, 0, 400),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterBack.png")))
            };
            login = new TextBox
            {
                Name = "loginBox",
                Width = 100,
                Height = 50,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\NameBack.png"))),
                Margin = new Thickness(0, 50, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                MaxLength = 20,
                BorderBrush = null,
                FontSize = 32,
                Foreground = new SolidColorBrush(Color.FromRgb(85, 40, 14)),
                FontFamily = new FontFamily("Georgia")
            };
            login.PreviewTextInput += login_PreviewTextInput;

            password = new PasswordBox
            {
                Name = "passwordBox",
                Width = 100,
                Height = 50,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\NameBack.png"))),
                Margin = new Thickness(0, 5, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                MaxLength = 20,
                BorderBrush = null,
                FontSize = 32,
                Foreground = new SolidColorBrush(Color.FromRgb(85, 40, 14)),
                FontFamily = new FontFamily("Georgia")
            };

            checkPassword = new PasswordBox
            {
                Name = "checkPasswordBox",
                Width = 100,
                Height = 50,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\NameBack.png"))),
                Margin = new Thickness(0, 5, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderBrush = null,
                FontSize = 32,
                Foreground = new SolidColorBrush(Color.FromRgb(85, 40, 14)),
                FontFamily = new FontFamily("Georgia")
            };
            SignUpButton = new Button
            {
                Name = "SignUpButton",
                Width = 100,
                Height = 50,
                Margin = new Thickness(0, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\RegisterButton.png"))),
                BorderBrush = null
            };
            SignUpButton.Click += SignUpButton_Click;

            Button BackButton = new Button
            {
                Name = "BackButton",
                Width = 100,
                Height = 50,
                Margin = new Thickness(0, 30, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Properties\BackButton.png"))),
                BorderBrush = null
            };

            BackButton.Click += delegate (object sender, RoutedEventArgs args)
            {
                ThicknessAnimation goAway = new ThicknessAnimation
                {
                    From = RegisterGroupBox.Margin,
                    To = new Thickness(RegisterGroupBox.Margin.Left, -400, RegisterGroupBox.Margin.Right, 400),
                    Duration = TimeSpan.FromSeconds(1),
                };
                goAway.Completed += delegate (object o, EventArgs eventArgs)
                {
                    AccountWindow.Children.Remove(RegisterGroupBox);
                };
                RegisterGroupBox.BeginAnimation(MarginProperty, goAway);
                AutoriztionGroupBox.BeginAnimation(MarginProperty, AnimateGrid(AutoriztionGroupBox));
                AnimateGearsUp();
            };
            StackPanel sp = new StackPanel();

            sp.Children.Add(login);
            sp.Children.Add(password);
            sp.Children.Add(checkPassword);
            sp.Children.Add(SignUpButton);
            sp.Children.Add(BackButton);
            RegisterGroupBox.Children.Add(sp);
            AccountWindow.Children.Add(RegisterGroupBox);
            RegisterGroupBox.BeginAnimation(MarginProperty, AnimateGrid(RegisterGroupBox));
            AnimateGearsDown();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private ThicknessAnimation AnimateGrid(Grid control)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                From = control.Margin,
                To = new Thickness(control.Margin.Left, 10, control.Margin.Right, 10),
                Duration = TimeSpan.FromSeconds(1)
            };
            return animation;
        }
        private void AnimateGearsUp()
        {
            GearTopLeft.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 360,
                To = 240,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearBotLeft.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 360,
                To = 240,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearCenterLeft.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 360,
                To = 240,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearTopRight.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 120,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearBotRight.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 120,
                Duration = TimeSpan.FromSeconds(1),
            });

            GearCenterRight.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 120,
                Duration = TimeSpan.FromSeconds(1),
            });
        }
        private void AnimateGearsDown()
        {
            GearTopLeft.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 120,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearBotLeft.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 120,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearCenterLeft.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 0,
                To = 120,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearTopRight.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 360,
                To = 240,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearBotRight.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 360,
                To = 240,
                Duration = TimeSpan.FromSeconds(1),
            });
            GearCenterRight.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation
            {
                From = 120,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
            });
        }
        private void login_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!trueSimbols.Contains(e.Text))
            {
                e.Handled = true;
            }
            else
            {

                e.Handled = false;
            }
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (database.GetEnterStatus(loginON.Text, passwordON.Password.GetHashCode().ToString()) == true)
            {
                MenuPage menuPage = new MenuPage();
                RoboteckMainPage.MenuBlock.NavigationService.Navigate(menuPage);
                menuPage.SetUserName(database.GetNowName());
            }
            else
            {
                MessageBox.Show("Проверьте введенные данные",
                     "Error 1", MessageBoxButton.OK);
            }

          
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            database.GetUserInformation();
            bool isLoginCorrect = true;
            bool isPassConfirm = true;
            if(login.Text.Length < 4 || login.Text.Length > 20)
            {
                login.BorderBrush = Brushes.Red;
                MessageBox.Show("Логин должен содержать от 4 до 20 символов",
                    "Error 4", MessageBoxButton.OK);
                isLoginCorrect = false;
            }

            if (password.Password != checkPassword.Password)
            {
                checkPassword.BorderBrush = Brushes.Red;
                MessageBox.Show("Пароли не совпадают", "Error 5", MessageBoxButton.OK);
                isPassConfirm = false;
            }

            if (isLoginCorrect && isPassConfirm)
            {
                database.SetUserInformation(login.Text, password.Password);
                database.GetUserInformation();
            }

            if (database.GetRegistratorStatus() == false)
            {
                MessageBox.Show("Регистрация не завершилась",
                    "Error 3", MessageBoxButton.OK);
            }
            else
            {
                login.Clear();
                password.Clear();
                checkPassword.Clear();
            }
        }

    }
}
