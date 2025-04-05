using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AppBase
{
    // hello! if you're reading this, that means you have cloned my repository from Git. this is a simple app base that shows an example of correct page navigation, and
    // all round app development in wpf/c#. throughout the code, i am going to leave comments in order for people to understand what the code is actually doing. you aren't
    // going to learn if you don't know what it does! you are also free to use this for starting an app, just credit me where credit is due. thanks!

    // this app mainly focuses on the C# side of WPF app development, but some XAML will be touched up on and mentioned in the green text (comments) in the code.
    // there may be issues in this code! it is not a perfect base, it's just a decent one. I have only been writing C# and making apps in WPF for about 6 months!

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OnAppStartup();
            //event handler to capture when the app is resized.
            this.StateChanged += AppExample_StateChanged;
        }

        // function to call stuff the second the Window opens.
        private void OnAppStartup()
        {
            // open page content using a Frame in the XAML, a Frame can be used to host pages inside of the Window, meaning you don't need to make multiple windows, and you
            // only need one.

            MainAppFrame.Navigate(new Pages.StartingPage());
        }

        // button functions

        public void NextPage(string page)
        {
            var Page = Type.GetType($"AppBase.Pages.{page}");

            if (Page != null)
            {
                var PageInstance = Activator.CreateInstance(Page);
                MainAppFrame.Navigate(PageInstance);
            }
        }

        // caption bar logic, (bugged try and fix this if u can idek how to do it)

        // this function checks if you have maximized the window by dragging on the top bar and maximizing by snapping the window by pushing it into the top of your screen.
        // (hard to explain)
        private void AppExample_StateChanged(object? sender, EventArgs e)
        {
            if (this.WindowState is WindowState.Maximized)
            {
                // WindowGrid = all content in the window, margining it is basically changing the size of it, and the value represents pixels. you can do a value of value,value,value,value.
                // so if your margin is 100,200,300,400, you are changing the dimensions by 100 pixels off of the left, 100 pixels off of the top, 300 pixels off of the right and 400 pixels off
                // of the bottom. margin only affects the size of the ui element if you have defined no height or no width, and if you havent set HorizontalAlignment or VerticalAlignment.
                WindowGrid.Margin = new Thickness(8);
            }
            else if (this.WindowState is WindowState.Normal)
            {
                WindowGrid.Margin = new Thickness(0);
            }
        }

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            // the if block will only be used IF the window is maximized, the way it knows this is by checking the property of WindowState.
            // this code is basically to make the top of the app respond to holding down the left click and dragging it around, it detects being left clicked and held by
            // using MouseButtonEventArgs, and the DragMove function name is from the MouseLeftButtonDown on the DockPanel in the XAML being DragMove.

            if (this.WindowState == WindowState.Maximized)
            {
                // grab the mouse position.
                Point mousePos = PointToScreen(e.GetPosition(this));
                // get the window position.
                double ratio = e.GetPosition(this).X / this.ActualWidth;
                double curHeight = this.ActualHeight;
                // make the window contents margin 0, so the content is where it should be and not pushed in by 8 pixels on each side. windows for some reason cuts off the window
                // content when maximized, and from my knowledge it's by 8 pixels. i don't know if this only happens to wpf apps, but this is an easy way to fix the issue.
                WindowGrid.Margin = new Thickness(0);
                // set the window state to normal instead of maximized.
                this.WindowState = WindowState.Normal;
                double restoredWidth = this.Width;
                this.Left = mousePos.X - (restoredWidth * ratio);
                this.Top = mousePos.Y - (e.GetPosition(this).Y);

                // call DragMove on the UI thread.
                Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
                {
                    this.DragMove();
                }));
            }

            //this is attached to the if statement, say the WindowState is anything else other than WindowState.Maximized, it will just DragMove.
            else
            {
                this.DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // shuts down the entire application.
            Application.Current.Shutdown();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            // checks if the window is currently maximized, if it is, it sets the WindowStat to Normal. if it is Normal already though, before it's even set it to 
            // WindowState.Normal, it sets it to WindowState.Maximized.
            // the other line of code under it, is setting a margin of the entire Window content. The reason for this is that some content gets cut off when maximizing, therefore
            // this eliminates that. "WindowGrid" is a UI element defined in the XAML that holds all of the apps content. It usually isn't named, but I named it so I could do this.

            if (this.WindowState is WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                WindowGrid.Margin = new Thickness(0);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                WindowGrid.Margin = new Thickness(8);
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            // simply sets the WindowState to Minimized.
            this.WindowState = WindowState.Minimized;
        }
    }
}