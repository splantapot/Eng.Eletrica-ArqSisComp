using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gerenciamento_memoria {
    internal static class Program {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main() {
            Comunicacao comMsp = new Comunicacao();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 app = new Form1();
            app.SetCom(comMsp);
            Application.Run(app);
        }
    }
}