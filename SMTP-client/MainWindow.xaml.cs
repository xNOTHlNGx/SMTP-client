using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace SmtpClientApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Initialize the state based on the checkbox's initial state
            UpdateAuthFields();
        }

        private void SendEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var smtpClient = new SmtpClient(HostnameTextBox.Text)
                {
                    Port = int.Parse(PortTextBox.Text),
                    EnableSsl = TlsCheckBox.IsChecked == true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };

                if (AuthCheckBox.IsChecked == true)
                {
                    smtpClient.Credentials = new NetworkCredential(UsernameTextBox.Text, PasswordBox.Password);
                }

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromTextBox.Text),
                    Subject = SubjectTextBox.Text,
                    Body = BodyTextBox.Text,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(ToTextBox.Text);

                smtpClient.Send(mailMessage);
                MessageBox.Show("Email sent successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AuthCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateAuthFields();
        }

        private void AuthCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateAuthFields();
        }

        private void UpdateAuthFields()
        {
            bool isEnabled = AuthCheckBox.IsChecked == true;
            UsernameTextBox.IsEnabled = isEnabled;
            PasswordBox.IsEnabled = isEnabled;
        }
    }
}