using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeAurasWPF
{
    public class Start
    {
        public void create()
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                MainWindow window = new MainWindow();
                window.Show();
            });
        }
    }
}
