using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Concesionaria
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new FrmLogin());
             // Application.Run(new FrmRegistrarAgenda ());
          //    //   Application.Run(new FrmConsultaAgenda());
           // A//pplication.Run(new FrmAbmEntidad());
           //   Application.Run(new FrmBorrarTablas ());
         //   Application.Run(new FrmVistaPrevia  ());
            //   Application.Run(new FrmIngresoCheque());
        }
    }
}
