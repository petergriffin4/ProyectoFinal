using Formulario_ENTRADAS.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formulario_ENTRADAS
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InicioSes loginForm = new InicioSes();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
               
                Application.Run(new Form1());
            }


        }
    }
}
