using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AulaSenac.Banco
{
    public class GravaLog
    {

        public void grava(String texto)
        {
            using (StreamWriter outputFile = new StreamWriter("log.dat", true))
            {
                String data = DateTime.Now.ToShortDateString();
                String hora = DateTime.Now.ToShortTimeString();
                String computador = Dns.GetHostName();
                outputFile.WriteLine(data + " " + hora + " (" + computador + ")" + texto);
            }
        }
    }
}
