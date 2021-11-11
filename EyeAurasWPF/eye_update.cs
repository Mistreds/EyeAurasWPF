using System.Net;
using System.IO;
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
                        Log(line);
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
                if(vers_from_file!=vers)
                {
                    is_new = true;
                }
                else
                {
                Log("Обновление не требуется");
                }
                
            }
            if (is_new)
            {
              using (System.Net.WebClient wc = new System.Net.WebClient())
                         {
                          string path = Environment.ExpandEnvironmentVariables("%appdata%/EyeAuras/EyeSquad/");
                           path = $"{path}\\EyeAurasWPF.dll";
                             wc.DownloadFile("https://github.com/Mistreds/EyeAurasWPF/releases/latest/download/EyeAurasWPF.dll", path);
                             Log("Успешно обновлено");
                         }
                using (FileStream fstream = new FileStream($"{path_json}\\version", FileMode.Create))
                {
                
                    // преобразуем строку в байты
                    byte[] array = System.Text.Encoding.Default.GetBytes(vers);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                 
                }
            }