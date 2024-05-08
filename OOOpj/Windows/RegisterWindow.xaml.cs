using OOOpj.Entity;
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
using System.Windows.Shapes;

namespace OOOpj.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            App.DB = new Entity.OOOpjEntities();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string LoginInp = login.Text;
            string PasswordInp = password.Text;

            if (login.Text == "" || password.Text == "")
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                SqlParameter pLogin = new SqlParameter(@"login", LoginInp);
                SqlParameter pPassword = new SqlParameter(@"password", PasswordInp);
                string sql = $"SELECT * FROM [Login] WHERE [Login].UserName = @login";
                Login userSearch = App.DB.Login.SqlQuery(sql, pLogin).ToList().FirstOrDefault();
                if (userSearch != null)
                {
                    MessageBox.Show("Вы не можете использовать этот логин!");
                    login.Text = "";
                    password.Text = "";
                }
                else
                {
                    MessageBox.Show("Вы успешно создали аккаунт!");
                    App.RName = LoginInp;
                    App.RPassword = PasswordInp;
                    sql = $"insert into [Login] (UserName, UserPassword) values ('{LoginInp}', '{PasswordInp}')";
                    string connectionstring = "Data Source=DESKTOP-0TA89EB;Initial Catalog=OOOpj;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                        SqlCommand command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                    this.Close();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            OOOpj.MainWindow windows = new MainWindow();
            windows.Show();
        }
    }
}
