using System.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

using (System.Net.WebClient wc = new System.Net.WebClient())
     {
          string path = Environment.ExpandEnvironmentVariables("%appdata%/EyeAuras/EyeSquad/");
          wc.Headers.Add("a", "a");
           try
                {
                     path = $"{path}\\EyeAurasWPF.dll";
                     if (!File.Exists(path))
                          wc.DownloadFile("https://github.com/Mistreds/EyeAurasWPF/releases/latest/download/EyeAurasWPF.dll", path);
                       while(!File.Exists(path)){}
                      Assembly a = Assembly.Load(File.ReadAllBytes(path));
                      Type t = a.GetType("EyeAurasWPF.Start", true, true);
                      object obj = Activator.CreateInstance(t);
                       MethodInfo method = t.GetMethod("create");
                       object result = method.Invoke(obj,new object[] {AuraContext.Id});
                       }
           catch (Exception ex)
              {
                                Log(ex.ToString());
                            }
     }   
                
                
            
            