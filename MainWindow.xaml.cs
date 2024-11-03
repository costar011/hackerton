using System.Globalization;
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

namespace hackerton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Button[] buttons = new Button[29];
        public static int ButtonIndex = -1;
        public static bool[] buttonColorsSet = new bool[29];

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = (Button)FindName($"seat{i + 1}");
                buttons[i].Content = "빈 좌석";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            ButtonIndex = System.Array.IndexOf(buttons, clickedButton);

            // 버튼이 이미 색상이 설정된 경우 (buttonColorsSet[ButtonIndex]가 true인 경우)
            if (ButtonIndex >= 0 && buttonColorsSet[ButtonIndex])
            {
                MessageBox.Show("이 좌석은 이미 예약되었습니다.", "예약 불가", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // 창을 열지 않고 종료
            }

            // 버튼 색상이 설정되지 않은 경우에만 adminItem 창을 열기
            Item item = new Item();

            caldata.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            // 메인 캘린더의 선택된 날짜가 있을 경우 보조 창의 캘린더와 동기화
            if (caldata.SelectedDate.HasValue)
            {
                item.SetDate(caldata.SelectedDate.Value);
            }

            item.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}