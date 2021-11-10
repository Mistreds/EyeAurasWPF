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
            public bool logpass { get; set; }
            public string path { get; set; }
            public string arg { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public bool Telegram { get; set; }
            public string token { get; set; }
            public string chatid { get; set; }
            public JsonCreate(bool logpass, string path, string arg, string login, string password, bool telegram, string token, string chatid)
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
            }
            public async Task CreateJson()
            {
                try
                {

                
                    var path_json = Environment.ExpandEnvironmentVariables("%appdata%/EyeAuras/EyeSquad/");
                    if (!Directory.Exists(path_json)) Directory.CreateDirectory(path_json);
                    path_json = $"{path_json}//id.config";
                    if (File.Exists(path_json)) File.Delete(path_json);

                    using (FileStream fs = new FileStream($"{path_json}", FileMode.OpenOrCreate))
                {
                    
                    await JsonSerializer.SerializeAsync<JsonCreate>(fs, this);
                    
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
            JsonCreate json = new JsonCreate((bool)Login_Arg_toogle.IsChecked, Path.Text, Arg.Text, Login.Text, Password.Text, (bool)telegram.IsChecked, Token.Text,Chatid.Text) ;
            _=json.CreateJson();
        }
    }
}
