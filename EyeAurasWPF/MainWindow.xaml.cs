using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            
            public JsonCreate(bool logpass, string path, string arg, string login, string password, bool telegram, string token, string chatid,string host)
            {
                this.logpass = !logpass;
                if(logpass==true)
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
                    path_json = $"{path_json}//id.config";
                    if (File.Exists(path_json)) File.Delete(path_json);

                    using (FileStream fs = new FileStream($"{path_json}", FileMode.OpenOrCreate))
                {
                    
                    await JsonSerializer.SerializeAsync<JsonCreate>(fs, this, options);
                    
                }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JsonCreate json = new JsonCreate((bool)Login_Arg_toogle.IsChecked, Path.Text, Arg.Text, Login.Text, Password.Text, (bool)telegram.IsChecked, Token.Text,Chatid.Text, Host.Text) ;
            _=json.CreateJson();
            Close();
        }
    }
}
