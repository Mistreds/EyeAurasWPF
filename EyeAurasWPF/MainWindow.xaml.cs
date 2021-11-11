using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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

namespace EyeAurasWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class JsonCreate
        {
            public string host { get; private set; }
            public bool logpass { get; private set; }
            public string path { get; private set; }
            public string arg { get; private set; }
            public string Login { get; private set; }
            public string Password { get; private set; }
            public bool Telegram { get; private set; }
            public string token { get; private set; }
            public string chatid { get; private set; }
            private string id;
            public JsonCreate(bool logpass, string path, string arg, string login, string password, bool telegram, string token, string chatid, string host, string id)
            {
                this.logpass = !logpass;
                if (logpass == true)
                {
                    this.arg = arg;
                }
                else
                {
                    Login = login;
                    Password = password;
                }
                this.path = path;


                Telegram = telegram;
                this.token = token;
                this.chatid = chatid;
                this.host = host;
                this.id = id;
            }
            public async Task CreateJson()
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };


                    var path_json = Environment.ExpandEnvironmentVariables("%appdata%/EyeAuras/EyeSquad/");
                    if (!Directory.Exists(path_json)) Directory.CreateDirectory(path_json);
                    path_json = $"{path_json}//{id}";
                    if (File.Exists(path_json)) File.Delete(path_json);

                    using (FileStream fs = new FileStream($"{path_json}", FileMode.OpenOrCreate))
                    {

                        await JsonSerializer.SerializeAsync<JsonCreate>(fs, this, options);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private string id;
        public MainWindow(string id)
        {
            this.id = id;
            

            InitializeComponent();
        }
        public MainWindow()
        {

            id = "id.config";
            

            InitializeComponent();

            
            WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/Mistreds/EyeAurasWPF/master/version");
            WebResponse response = request.GetResponse();
            string vers = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        vers = line;
                    }
                }
            }
            var path_json = Environment.ExpandEnvironmentVariables("%appdata%/EyeAuras/");
            if (!Directory.Exists(path_json)) Directory.CreateDirectory(path_json);
            bool is_new = false;
            using (FileStream fstream = new FileStream($"{path_json}\\version", FileMode.OpenOrCreate))
            {
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string vers_from_file = System.Text.Encoding.Default.GetString(array);
                if (vers_from_file != vers)
                {
                    is_new = true;
                }
                else
                {
                    Console.WriteLine("Обновление не требуется");
                }

            }
            if (is_new)
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string path = Environment.ExpandEnvironmentVariables("%appdata%/EyeAuras/EyeSquad/");
                    path = $"{path}\\EyeAurasWPF.dll";
                    wc.DownloadFile("https://github.com/Mistreds/EyeAurasWPF/releases/latest/download/EyeAurasWPF.dll", path);
                    Console.WriteLine("Успешно обновлено");
                }
                using (FileStream fstream = new FileStream($"{path_json}\\version", FileMode.Create))
                {

                    // преобразуем строку в байты
                    byte[] array = System.Text.Encoding.Default.GetBytes(vers);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);

                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JsonCreate json = new JsonCreate((bool)Login_Arg_toogle.IsChecked, Path.Text, Arg.Text, Login.Text, Password.Text, (bool)telegram.IsChecked, Token.Text, Chatid.Text, Host.Text, id);
            _ = json.CreateJson();
            Close();
        }
    }
}
