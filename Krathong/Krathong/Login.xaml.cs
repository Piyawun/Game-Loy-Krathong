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

namespace Krathong
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlCommand command;
        SqlConnection con;
        SqlDataReader DataReader;
        Home home = new Home();
        
        public Login()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bigco\OneDrive\Desktop\KrathongGameCP214\Krathong\Krathong\Database.mdf;Integrated Security=True ");
            con.Open();
            InitializeComponent();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Login1_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password.ToString() != string.Empty || username.Text != string.Empty)
            {
                string NandPass = String.Format("select * from members where username = '{0}' and password= '{1}'", username.Text, password.Password.ToString());
                command = new SqlCommand(NandPass, con);
                DataReader = command.ExecuteReader();
                if (DataReader.Read())
                {
                    DataReader.Close();
                    this.Close();
                    string name = username.Text;
                    KrathongGame game = new KrathongGame(name);
                    game.Show();
                   
                }
                else
                {
                    DataReader.Close();
                    MessageBox.Show("ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
                }

            }
            else
            {
                MessageBox.Show("โปรดระบุข้อมูลให้ครบถ้วน");
            }
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            home.Show();
        }
    }
}
