using System;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;

namespace AdamPowerTool
{
    public static class ErrorHandler
    {
        private static readonly string CrashLogsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CrashLogs");
        private static readonly string DeveloperEmail = "developer@example.com"; // Senin e-posta adresin

        public static class ErrorMessages
        {
            public const string DataFetchError = "Sistem verileri alınırken bir hata oluştu.";
            public const string GraphRenderError = "Grafik çizimi sırasında bir hata oluştu.";
            public const string UnknownError = "Bilinmeyen bir hata oluştu.";
        }

        static ErrorHandler()
        {
            if (!Directory.Exists(CrashLogsPath))
            {
                Directory.CreateDirectory(CrashLogsPath);
            }
        }

        public static void HandleError(Exception ex, string errorMessage)
        {
            // Hata raporunu oluştur
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string logFileName = Path.Combine(CrashLogsPath, $"CrashLog_{timestamp}.txt");
            string report = $"Hata Zamanı: {DateTime.Now}\n" +
                           $"Hata Mesajı: {errorMessage}\n" +
                           $"Detaylar: {ex.Message}\n" +
                           $"Stack Trace: {ex.StackTrace}\n" +
                           "----------------------------------------\n";

            // Hata raporunu dosyaya yaz
            File.AppendAllText(logFileName, report);

            // Kullanıcıya bildirim göster
            DialogResult result = MessageBox.Show(
                $"{errorMessage}\nSorunu bildirmek ister misiniz?",
                "Hata Oluştu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error
            );

            if (result == DialogResult.Yes)
            {
                SendErrorReport(logFileName, report);
            }
        }

        private static void SendErrorReport(string logFileName, string report)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("adamspowertool@example.com");
                    mail.To.Add(DeveloperEmail);
                    mail.Subject = "Adam's Power Tool - Hata Raporu";
                    mail.Body = report;
                    mail.Attachments.Add(new Attachment(logFileName));

                    using (var smtp = new SmtpClient("smtp.example.com"))
                    {
                        smtp.Port = 587;
                        smtp.Credentials = new System.Net.NetworkCredential("username", "password");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                MessageBox.Show("Hata raporu başarıyla gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata raporu gönderilirken bir sorun oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}