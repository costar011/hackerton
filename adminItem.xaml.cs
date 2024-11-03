using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace hackerton
{
    public partial class adminItem : Window
    {
        private DateTime _selectedDateTime;
        private DateTime _lastselectedDateTime;

        public adminItem()
        {
            InitializeComponent();
            DataContext = this;

            // DateTime tmpdata = ((MainWindow)Application.Current.MainWindow).caldata.SelectedDate.Value;


            // 오늘 이전의 모든 날짜를 선택할 수 없도록 설정
            calendar.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }

        public DateTime SelectedDateTime
        {
            get => _selectedDateTime;
            set
            {
                _selectedDateTime = value;
            }
        }
        public DateTime lastSelectedDateTime
        {
            get => _lastselectedDateTime;
            set
            {
                _lastselectedDateTime = value;
            }
        }

        private void OnConfirmClick(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = calendar.SelectedDate;
            bool isHourValid = int.TryParse(start_hourTextBox.Text, out int hour);
            bool isMinuteValid = int.TryParse(start_minuteTextBox.Text, out int minute);

            if (selectedDate == null)
            {
                MessageBox.Show("날짜를 선택하세요.", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!isHourValid || !isMinuteValid || hour < 0 || hour > 23 || minute < 0 || minute > 59)
            {
                MessageBox.Show("유효한 시간을 입력하세요 (시: 00-23, 분: 00-59).", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime? lastselectedDate = calendar.SelectedDate;
            bool endHourValid = int.TryParse(end_hourTextBox.Text, out int hour2);
            bool endMinuteValid = int.TryParse(end_minuteTextBox.Text, out int minute2);

            if (!isHourValid || !isMinuteValid || hour2 < 0 || hour2 > 23 || minute2 < 0 || minute2 > 59)
            {
                MessageBox.Show("유효한 시간을 입력하세요 (시: 00-23, 분: 00-59).", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            SelectedDateTime = selectedDate.Value.AddHours(hour).AddMinutes(minute);
            lastSelectedDateTime = selectedDate.Value.AddHours(hour2).AddMinutes(minute2);
            MessageBox.Show($"예약이 완료되었습니다:\n{SelectedDateTime}\n{lastSelectedDateTime}", "예약 확인", MessageBoxButton.OK, MessageBoxImage.Information);

            if (adminMain.ButtonIndex >= 0 && adminMain.ButtonIndex <= 5 || adminMain.ButtonIndex > 15)
            {
                adminMain.buttons[adminMain.ButtonIndex].Content = $"    예 약 석\n시작시간 : {hour}:{minute}\n종료시간 : {hour2}:{minute2}";
                adminMain.buttons[adminMain.ButtonIndex].FontSize = 11;
                adminMain.buttons[adminMain.ButtonIndex].BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (adminMain.ButtonIndex >= 0 && adminMain.ButtonIndex >= 6 && adminMain.ButtonIndex <= 15)
            {
                adminMain.buttons[adminMain.ButtonIndex].Content = $"    예 약 석\n시작시간 : {hour}:{minute}\n종료시간 : {hour2}:{minute2}";
                adminMain.buttons[adminMain.ButtonIndex].FontSize = 8;
                adminMain.buttons[adminMain.ButtonIndex].BorderBrush = new SolidColorBrush(Colors.Red);
            }

            this.Close();
        }

        private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                SelectedDateTime = calendar.SelectedDate.Value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SetDate(DateTime date)
        {
            calendar.SelectedDate = date;
        }

        private void Edit_OnConfirmClick(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_OnConfirmClick(object sender, RoutedEventArgs e)
        {
            adminMain.buttons[adminMain.ButtonIndex].BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF626262"));
            adminMain.buttonColorsSet[adminMain.ButtonIndex] = false;
            adminMain.buttons[adminMain.ButtonIndex].Content = "빈 좌석";
            this.Close();
        }
    }
}