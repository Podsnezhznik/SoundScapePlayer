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

namespace Test1_Base
{
    /// <summary>
    /// Логика взаимодействия для AuthRegWindow.xaml
    /// </summary>
    public partial class AuthRegWindow : Window
    {
        public AuthRegWindow()
        {
            InitializeComponent();

            //frame.Navigate(new RegistrationPage());
            if (MainWindow.DataStorage.IsLogged)
            {
                frame.Navigate(new ProfilePage());
            }
            else
            {
                frame.Navigate(new RegistrationPage());
            }
        }
    }
}
