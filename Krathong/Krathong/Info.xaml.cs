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

namespace Krathong
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        int i = 1; 
        public Info()
        {
            InitializeComponent();
        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            i--;
            if (i < 1)
            {
                i = 4;
            }
            picholder.Source = new BitmapImage(new Uri(@"pictureinfo/info_" + i + ".png", UriKind.Relative));
        }

        private void Home_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.Show();
        }

        private void Next_Click_1(object sender, RoutedEventArgs e)
        {
            i++;
            if (i > 4)
            {
                i = 1;
            }
            picholder.Source = new BitmapImage(new Uri(@"pictureinfo/info_" + i + ".png", UriKind.Relative));
        }
    }
}
