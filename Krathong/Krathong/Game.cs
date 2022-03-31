using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Krathong
{
    public partial class KrathongGame : Form
    {
        SqlCommand command;
        SqlConnection con;
        SqlDataReader DataReader;
        String sqlstr = "";
        int scoreuser = 0;

        string username;
        //เช็คซ้ายขวา
        bool goleft;
        bool goright;

        //set ค่าเริ่มต้น
        int speed = 8;
        int score = 0;
        int missed = 0;

        //สร้างเอาไว้แกน x และ y
        Random rndY = new Random();
        Random rndX = new Random();

        //สร้างรูปภาพไว้ใช้
        PictureBox trash = new PictureBox();
        public KrathongGame(string username)
        {
            this.username = username;
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bigco\OneDrive\Desktop\KrathongGameCP214\Krathong\Krathong\Database.mdf;Integrated Security=True");
            con.Open();
            InitializeComponent();
            reset();
            
        }
        
        
       
        private void catchkrathong_Click(object sender, EventArgs e)
        {

        }

        private void KrathongGame_Load(object sender, EventArgs e)
        {

        }

        private void keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }

        private void keydown(object sender, KeyEventArgs e)
        {
            //เหมือน java แต่ java เป็น keyevent.VK
            if (e.KeyCode == Keys.Left)
            { 
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }
        private void reset()
        {
            foreach (Control X in this.Controls)
            {
                // เช็ครูปโดยใช้แท็ค Krathong
                if (X is PictureBox && X.Tag == "Krathong")
                {
                    //สุ่มให้ตำแหน่งให้กระทง
                    X.Top = rndY.Next(80, 300) * -1;
                    X.Left = rndX.Next(5, this.ClientSize.Width - X.Width);
                }
            }

            people.Left = this.ClientSize.Width / 2; //setให้ตัวละครอยู่ตรงกลาง


            score = 0;
            missed = 0;
            speed = 10;

            goleft = false;
            goright = false;
            gameTimer.Start();
        }

        private void endgame()
        {
            command = new SqlCommand(String.Format("select score from members where username='{0}'", this.username), con);
            var scoreindb = command.ExecuteScalar();
            gameTimer.Stop();
            DialogResult dialogResult = MessageBox.Show($"{username} ได้คะแนน {score} คะแนน " + " \n     เล่นต่อหรือไม่", "KrathongGame", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (Convert.ToInt32(scoreindb) > score)
                {
                    command = new SqlCommand("UPDATE members SET score = @score where username=@username", con);
                    command.Parameters.AddWithValue("score", score);
                    command.Parameters.AddWithValue("username", this.username);
                    command.ExecuteNonQuery();
                    reset();
                }
                else
                {
                    reset();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                if (Convert.ToInt32(scoreindb) > score)
                {
                    Application.ExitThread();
                }
                else
                {
                    command = new SqlCommand("UPDATE members SET score = @score where username=@username", con); 
                    command.Parameters.AddWithValue("score", score);
                    command.Parameters.AddWithValue("username", this.username);
                    command.ExecuteNonQuery();
                    Application.ExitThread();
                }
               

            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            
            catchkrathong.Text = "กระทงที่เก็บได้: " + score;
            miss.Text = "กระทงที่ตกแม่น้ำ : " + missed;


            if (goleft == true)
            {
                people.Left -= 10;
                people.Image = Properties.Resources.people_normal;
            }

            if (goright == true)
            {
                people.Left += 12;
                people.Image = Properties.Resources.people_normal;
            }



            foreach (Control X in this.Controls)
            {
                // เช็ครูปโดยใช้แท็ค Krathong
                if (X is PictureBox && X.Tag == "Krathong")

                {

                    X.Top += speed;

                    // ถ้ากระทงชนขอบล่าง 
                    if (X.Top + X.Height > this.ClientSize.Height)
                    {
                        //ถ้ากระทงชนขอบล่างให้โชว์ภาพขยะ
                        trash.Image = Properties.Resources.trash;
                        trash.Location = X.Location;
                        trash.Height = 59;
                        trash.Width = 60;
                        trash.BackColor = System.Drawing.Color.Transparent;

                        this.Controls.Add(trash);

                        X.Top = rndY.Next(80, 300) * -1;
                        X.Left = rndX.Next(5, this.ClientSize.Width - X.Width);
                        missed++;
                        people.Image = Properties.Resources.people_hurt;
                    }

                    //ถ้ากระทงชนกับคน
                    //IntersectsWith คือ method ที่เทียบตำแหน่งว่าตรงกันหรือไม่
                    if (X.Bounds.IntersectsWith(people.Bounds))
                    {
                        //สุ่มค่า
                        X.Top = rndY.Next(100, 300) * -1;
                        X.Left = rndX.Next(5, this.ClientSize.Width - X.Width);
                        score++;
                    }

                    // เพิ่มความยากให้การตกลงมาของกระทงมากขึ้น
                    if (score >= 20)
                    {
                        speed = 16;
                    }
                    // เช็คจำนวนที่พลาด
                
                    if (missed > 5)
                    {
                        endgame();
                    }

                }
            }
        }
    }
}
