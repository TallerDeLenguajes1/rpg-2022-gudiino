using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Net;
namespace rpg
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirr = @"C:\DDD\PU\2-TallerDeLenguaje1\TPTL1\TP72022RPG\rpg-2022-gudiino\";
            
            PRESENTACION();
            List<Personaje> jugadores=INICIAR();
            //para json
            const string NombreArchivo = "jugadores.json";
            var miHelperdeArchivos = new HelperDeJson();
            
            //Guardo el archivo de jugadores formato JSON
            string jugadoresJson = JsonSerializer.Serialize(jugadores);
            miHelperdeArchivos.GuardarArchivoTexto(NombreArchivo, jugadoresJson);
            Console.WriteLine("************************");
            Console.WriteLine();
            Console.WriteLine("Datos y Carateristicas de los personajes");
            int cantEnf=1;
            foreach (var item in jugadores)
            {
                Console.WriteLine("Jugador {0}",cantEnf);
                item.MuestraDatos();
                Console.WriteLine();
                item.MuestraCaracter();
                Console.WriteLine();
                Console.WriteLine("************************");
                cantEnf++;
            }
            cantEnf=1;//para mostrar los primeros enfrentamientos
            Console.WriteLine("INICIO DEL ENFRENTAMIENTO");
            Personaje jugador1=jugadores[0];
            Personaje jugador2=jugadores[2];
            while(jugador2.ActSalud()!=0 && jugador1.ActSalud()!=0)
            {
                if(cantEnf==1){
                Console.WriteLine("..............................");
                Console.WriteLine("Ronda "+(cantEnf));
                Console.WriteLine("Ataque jugador {0} a jugador {1}", jugador1.Nombre, jugador2.Nombre);
                jugador1.Atacar(jugador2);
                Console.WriteLine("Salud jugador {0}: {1}",jugador2.Nombre,jugador2.ActSalud());
                if (jugador2.ActSalud()!=0)
                {
                    Console.WriteLine("Ataque jugador {0} a jugador {1}", jugador2.Nombre, jugador1.Nombre);
                    jugador2.Atacar(jugador1);
                    Console.WriteLine("Salud jugador {0}: {1}",jugador1.Nombre,jugador1.ActSalud());  
                }
                Console.WriteLine("..............................");
                Console.Write("Rondas:");
                }else{
                    jugador1.Atacar(jugador2);
                    if (jugador2.ActSalud()!=0){
                        jugador2.Atacar(jugador1); 
                    }
                    Console.Write("-{0}",cantEnf);
                }
                cantEnf++;
            }
            //muestra final de la salud de los jugadores
            Console.WriteLine("\nSalud jugador {0}: {1}",jugador2.Nombre,jugador2.ActSalud());
            Console.WriteLine("Salud jugador {0}: {1}",jugador1.Nombre,jugador1.ActSalud());
            
            Console.WriteLine("\n________________GANADOR_______________");
            Console.WriteLine("|                                    |");
            if (jugador1.ActSalud()>jugador2.ActSalud())
            {
                Console.WriteLine($"              {jugador1.Nombre}                 ");
                Console.WriteLine("|____________________________________|");
                jugadores.Remove(jugador2);
                jugador1.CargarPremio();

            }else{
                Console.WriteLine($"              {jugador2.Nombre}                ");
                Console.WriteLine("|____________________________________|");
                jugadores.Remove(jugador1);
                jugador2.CargarPremio();
            }
            Console.WriteLine();
            
            //******************* guardar dato en csv
            
            string archivo = "Ganadores.csv";
            string ruta = dirr+archivo;
            
            if (!File.Exists(ruta))//validacion de su existencia
            {
                File.Create(ruta);
            }
            //abrir el archivo para su escritura
            FileStream Fstream = new FileStream(ruta, FileMode.Open);
            // string exten="";
            DateTime actual = DateTime.Now;
            using (StreamWriter StreamW = new StreamWriter(Fstream))
            {
                foreach (Personaje juga in jugadores)
                {
                    StreamW.WriteLine(actual.ToString()+','+juga.Nombre);
                }
            }
            //cerrar el archivo
            Fstream.Close(); 
            //apertura del archivo
            FileStream MiArchivo = new FileStream(ruta, FileMode.Open);
            StreamReader StrReader = new StreamReader(MiArchivo);
            string linea = "";
            List<string[]> LecturaDelArchivo = new List<string[]>();

            while ((linea = StrReader.ReadLine()) != null)
            {
                string[] Fila = linea.Split(',');
                //escribir aqui los datos a mostrar en pantalla
            }
            Fstream.Close();
            Console.WriteLine("**************************************");
            Console.WriteLine("\nFIN.");
        }
        //************************************************************
        private static void PRESENTACION()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("presentacion.txt");
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para CONTINUAR");
                Console.ReadLine();
                sr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        //***************************************************************
        private static void menu()
        {
            Console.WriteLine("1 -> INICIAR");
            Console.WriteLine("2 -> CARGAR JUEGO");
            Console.WriteLine("3 -> JUGAR");
            Console.WriteLine("4 -> SALIR");
        }
        //***************************************************************
        private static List<Personaje> INICIAR()
        {
            //lista de nombres reducida
            string listaNombres = "nombres-2015.csv";//https://datos.gob.ar/dataset/otros-nombres-personas-fisicas/archivo/otros_2.21
            List<string[]> LecturaListaNom= HelperCsv.LeerCsv(listaNombres, ',');
            // foreach (var item in LecturaListaNom)
            // {
            //     Console.WriteLine("Nombre: {0}; Cantidad: {1}; Año: {2}",item[0], item[1], item[2]);
            // }
            Console.WriteLine("Ingrese su Nombre");
            String nom="JORGE GUDIÑO";//PROVISORIO
            Console.WriteLine(nom);
            //nom = Console.ReadLine();
            Console.WriteLine("Elija el numero correspondiente a la Provincia de la cual estara a cargo");
            List<string> nomprov=GetProvinciasArgentinas();
            //muestra provincias en lista
            int cant=0;
            foreach (var item in nomprov)
            {
                Console.WriteLine("Provincia {0}: {1}",cant,item);
                cant++;
            }
            Console.Write("Seleccion: ");
            int seleccion= Convert.ToInt32(Console.ReadLine());
            string provSelec=nomprov[seleccion];
            nomprov.Remove(provSelec);
            Personaje jugador1 = new Personaje(nom,provSelec);
            var jugadores = new List<Personaje> ();
            jugadores.Add(jugador1);
            //carga al azar nombres de pesonajes de una lista de nombres
            cant=0;
            Random numRan= new Random();
            while(cant!=23)
            {    //seleccion de nombres al azar
                int lng=LecturaListaNom.Count;
                int sel=numRan.Next(0,lng);
                string[] nomm=LecturaListaNom[sel];
                LecturaListaNom.Remove(nomm);
                //seleccion de provincias al azar
                int lng2=nomprov.Count;
                int sel2=numRan.Next(0,lng2);
                string noomprov=nomprov[sel2];
                nomprov.Remove(noomprov);
                //pasamos los datos para generar datos personaje
                jugadores.Add(new Personaje(nomm[0],noomprov));
                cant++;
            }
            // cant=1;
            // foreach (var item in jugadores)
            // {
            //     Console.WriteLine("Jugador {0}", cant);
            //     item.MuestraDatos();
            //     cant++;
            // }
            return jugadores;
        }
        //************************************************************
        private static List<string> GetProvinciasArgentinas()
        {
            var url = @"https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            var NomProv= new List<string>();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return NomProv;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            Root ListProvincias = JsonSerializer.Deserialize<Root>(responseBody);
                            foreach (Provincia Prov in ListProvincias.provincias)
                            {
                                NomProv.Add(Prov.nombre);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API");
            }
            return NomProv;
        }
    }
}