using System.Linq;
using System.Windows;

namespace hackerton
{
    public partial class Login : Window
    {
        // 관리자 계정 정보 (예시로 "admin" 아이디와 "admin123" 비밀번호 설정)
        private const string AdminUsername = "ping";
        private const string AdminPassword = "1234";

        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // 관리자 계정인지 확인
            if (username == AdminUsername && password == AdminPassword)
            {
                MessageBox.Show("관리자님 환영합니다!", "관리자 로그인");
                adminMain admin = new adminMain(); // 관리자 전용 윈도우로 변경 가능
                admin.Show();
                this.Close(); // 로그인 창 닫기
                return;
            }

            // 일반 사용자 로그인 확인
            var user = Register.Users.FirstOrDefault(u => u.StudentId == username && u.Password == password);

            if (user != null) // 일반 사용자로 로그인 성공
            {
                MessageBox.Show($"{user.StudentId}님 환영합니다!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // 로그인 창 닫기
            }
            else
            {
                MessageBox.Show("아이디 또는 비밀번호가 잘못되었습니다.");
            }
        }

        public void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.ShowDialog(); // 회원가입 창을 모달로 표시
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
