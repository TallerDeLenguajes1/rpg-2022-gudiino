using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace rpg
{
    static public class HelperCsv
    {
        public static List<string[]> LeerCsv(string ruta, char caracter)
        {
            FileStream MiArchivo = new FileStream(ruta, FileMode.Open);
            StreamReader StrReader = new StreamReader(MiArchivo);

            string Linea = "";
            List<string[]> LecturaDelArchivo = new List<string[]>();

            while ((Linea = StrReader.ReadLine()) != null)
            {
                string[] Fila = Linea.Split(caracter);
                LecturaDelArchivo.Add(Fila);
            }

            return LecturaDelArchivo;
        }

    }
}
