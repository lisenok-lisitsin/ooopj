using OOOpj.Entity;
using OOOpj.Windows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOOpj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.DB = new Entity.OOOpjEntities();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            OOOpj.Windows.RegisterWindow registration = new RegisterWindow();
            registration.Show();
            this.Hide();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string LoginInp = LoginInput.Text;
            string PasswordInp = PasswordInput.Text;

            if (LoginInput.Text == "" || PasswordInput.Text == "")
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                SqlParameter pLogin = new SqlParameter(@"login", LoginInp);
                SqlParameter pPassword = new SqlParameter(@"password", PasswordInp);
                string sql = $"SELECT * FROM [Login] WHERE [Login].UserName = @login AND [Login].UserPassword = @password";
                Login userSearch = App.DB.Login.SqlQuery(sql, pLogin, pPassword).ToList().FirstOrDefault();
                if (userSearch != null)
                {
                    MessageBox.Show($"Добро пожаловать, {LoginInp}!");
                    App.Name = LoginInput.Text;
                    App.Password = PasswordInput.Text;
                    LoginInput.Text = "";
                    PasswordInput.Text = "";
                    //initialize передача
                    //Login.EntryPoint entryPoint = new EntryPoint(NameEntAcc.Text);
                    //entryPoint.Show();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Такого нет. Может создадите аккаунт?");
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
