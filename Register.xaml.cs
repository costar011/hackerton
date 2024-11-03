using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace hackerton
{
    public partial class Register : Window
    {
        // 전역에서 접근할 수 있도록 static으로 선언
        public static List<User> Users = new List<User>();

        public Register()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string studentId = StudentIdTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = PasswordBox2.Password;

            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("아이디, 비밀번호, 비밀번호 재입력을 모두 입력하세요.");
                return;
            }

            // 특수 기호 포함 여부 검사
            if (!IsValidStudentId(studentId))
            {
                MessageBox.Show("아이디에는 알파벳과 숫자만 사용할 수 있습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
                return;
            }

            // Users 목록에서 아이디 중복 여부 확인
            if (Users.Any(u => u.StudentId == studentId))
            {
                MessageBox.Show("이미 가입된 회원입니다.");
                return;
            }

            // 중복이 없을 경우 사용자 추가
            Users.Add(new User { StudentId = studentId, Password = password });
            MessageBox.Show("회원가입이 완료되었습니다.");

            // 입력 필드 초기화
            StudentIdTextBox.Clear();
            PasswordBox.Clear();
            PasswordBox2.Clear();
            PasswordPlaceholder.Visibility = Visibility.Visible;
            PasswordPlaceholder2.Visibility = Visibility.Visible;

            this.Close();
        }

        // 첫 번째 PasswordBox Placeholder 처리
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(PasswordBox.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        // 두 번째 PasswordBox Placeholder 처리
        private void PasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder2.Visibility = string.IsNullOrEmpty(PasswordBox2.Password) ? Visibility.Visible : Visibility.Collapsed;
        }

        // 유효한 아이디인지 검사하는 메서드 (알파벳과 숫자만 허용)
        private bool IsValidStudentId(string studentId)
        {
            string pattern = @"^[a-zA-Z0-9]+$"; // 알파벳과 숫자로만 구성된 문자열
            return Regex.IsMatch(studentId, pattern);
        }
    }

    public class User
    {
        public string StudentId { get; set; }
        public string Password { get; set; }
    }
}
