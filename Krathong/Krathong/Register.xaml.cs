using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
namespace Krathong
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        SqlCommand command;
        SqlConnection con;
        SqlDataReader DataReader;
        Home home = new Home();

        public Register()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bigco\OneDrive\Desktop\KrathongGameCP214\Krathong\Krathong\Database.mdf;Integrated Security=True");
            con.Open();
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTxt.Text != string.Empty || usernameTxt.Text != string.Empty)
            {
                command = new SqlCommand(String.Format("select * from members where username='{0}'", usernameTxt.Text), con);
                DataReader = command.ExecuteReader();
                if (DataReader.Read())
                {
                    DataReader.Close();
                    MessageBox.Show("มีชื่อในระบบอยู่แล้ว");
                }
                else
                {
                    try { DataReader.Close();
                    command = new SqlCommand("insert into members values(@username,@password,@score)", con);
                    command.Parameters.AddWithValue("username", usernameTxt.Text);
                    command.Parameters.AddWithValue("password", passwordTxt.Text);
                    command.Parameters.AddWithValue("score", 0);
                    command.ExecuteNonQuery();
                    MessageBox.Show("สำเร็จ");
                    this.Hide();
                    home.Show();
                }catch(Exception a)
                    {
                        MessageBox.Show(a.Message);
                    }
                    }
                    
            }
            else
            {
                MessageBox.Show("กรุณากรอกให้ครบทุกช่อง");
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            home.Show();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
