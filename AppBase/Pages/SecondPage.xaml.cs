using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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

namespace AppBase.Pages
{
    /// <summary>
    /// Interaction logic for SecondPage.xaml
    /// </summary>
    public partial class SecondPage : Page
    {
        public SecondPage()
        {
            InitializeComponent();
        }

        private void BackPage_Click(object sender, RoutedEventArgs e)
        {
            // basically just grabs the MainWindow instance since the Frame that hosts this page is a child of the MainWindow, therefore this is the easiest way I've found to access it.
            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // name of page to send to function
                var navigateToPage = "StartingPage";

                // call function in MainWindow with page name that we want to navigate to.
                mainWindow.NextPage(navigateToPage);
            }
        }
    }
}
