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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace TestForEveryOne
{
    public class Test
    {
        public string testName { get; set; }
        public string description { get; set; }
        public int time { get; set; }
        public string Autor { get; set; }
    }
    public partial class MainWindow : Window
    {
        public class Test
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Time { get; set; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
            Init();
            /*var conv = new BrushConverter();
            ObservableCollection<Test> tests = new ObservableCollection<Test>();
            tests.Add(new Test { testName = "FirstTest", description = "The First Test", time = 30, Autor = "Me" });*/
           
        }

        private List<Test> GetTestList()
        {
            List<Test> testList = new List<Test>();
            testList.Add(new Test() { Name = "Test 1", Description = "Description 1", Time = "10:00" });
            testList.Add(new Test() { Name = "Test 2", Description = "Description 2", Time = "12:00" });
            testList.Add(new Test() { Name = "Test 3", Description = "Description 3", Time = "14:00" });
            return testList;
        }

        private bool isStatusNow = true;
        private void Init()
        {
            Border border = statusLine;
            if (isStatusNow) border.Background = new SolidColorBrush(Colors.Green);
            FullName.Text = "Full Name"; //var str
            standingStatus.Text = "ADMIN"; //variable str
            if (standingStatus.Text == "Student")
            {
               // eyeForTeacher.Visibility = Visibility.Collapsed; //Colum eye
                Users.Visibility = Visibility.Collapsed; //UsersListsButtom
            }


            /*
                                     <DataGridTemplateColumn CanUserResize="False" Width="auto" x:Name="eyeForTeacher" Visibility="Visible">
                            <DataGridTemplateColumn.HeaderTemplate>
                                <DataTemplate>
                                    <StackPanel Orientation="Horizontal">
                                        <Border Background="#00CED1">
                                            <Button>
                                                <Icon:PackIconMaterial Kind="Eye" Foreground="#00CED1"/>
                                            </Button>
                                        </Border>
                                    </StackPanel>
                                </DataTemplate>
                            </DataGridTemplateColumn.HeaderTemplate>
                        </DataGridTemplateColumn>
             */

        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();

            }
        }

        private bool IsMaximized = true;        

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if(e.ClickCount == 2)
            {
                if(IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }

        }

        private void statusLine_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if(border != null)
            {
                SolidColorBrush brush = border.Background as SolidColorBrush;
                if(brush != null)
                {
                    if (brush.Color == Colors.Green)
                    {
                        border.Background = new SolidColorBrush(Colors.Red);
                        isStatusNow = false;
                    }
                    else
                    {
                        border.Background = new SolidColorBrush(Colors.Green);
                        isStatusNow = true;
                    }
                }
            }
        }

        
        private void Tests_Click(object sender, RoutedEventArgs e)
        {
            pageTitle.Text = Tests.Content.ToString();
            // устанавливаем BorderBrush на кнопке Tests в выбранный цвет
            Tests.BorderBrush = new SolidColorBrush(Color.FromRgb(120, 79, 242));

            // возвращаем BorderBrush на кнопке Users в исходный белый цвет
            Users.BorderBrush = new SolidColorBrush(Colors.Transparent);
            txtFilter.Text = "Search in Tests...";

        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            pageTitle.Text = Users.Content.ToString();
            // устанавливаем BorderBrush на кнопке Users в выбранный цвет
            Users.BorderBrush = new SolidColorBrush(Color.FromRgb(120, 79, 242));

            // возвращаем BorderBrush на кнопке Tests в исходный цвет dae2ea
            Tests.BorderBrush = new SolidColorBrush(Colors.Transparent);//(Color.FromRgb(218, 226, 234));
            txtFilter.Text = "Search in Users...";
        }
    }
}
