﻿using System;
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
            PRESENTACION();
            List<Personaje> jugadores0=new List<Personaje>();
            int opc=1;
            while (opc!=0)
            {
                menu();
                Console.Write("Seleccion: ");
                opc= Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (opc)
                {
                    case 0:
                        opc=0;
                        break;
                    case 1:
                        List<Personaje> jugadores=Iniciar();
                        jugadores0=jugadores;
                        break;
                    case 2:
                        MostrarPersonaje(/*jugadores0[0]*/);
                        break;
                    case 3:
                        Jugar();
                        break; 
                }
            }
            Console.WriteLine();
            Console.WriteLine("\nFIN.");
        }
        //funciones o metodos
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
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Continuar();
        }

        private static void Continuar()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER para IR al MENU");
            Console.ReadLine();
            Console.WriteLine();
        }

        //***************************************************************
        private static void menu()
        {
            Console.WriteLine("------MENU-----");
            Console.WriteLine();
            Console.WriteLine("1 -> INICIAR");
            Console.WriteLine("2 -> MOSTRAR PERSONAJE PRINCIPAL");
            //Console.WriteLine("2 -> CARGAR JUEGO");
            Console.WriteLine("3 -> JUGAR");
            //Console.WriteLine("4 -> GUARDAR");
            Console.WriteLine("0 -> SALIR");
            Console.WriteLine();
        }
        //***************************************************************
        private static List<Personaje> Iniciar()
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
            List<Personaje> jugadores=new List<Personaje>();
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
            GuardarPartida(jugadores);
            Console.Write("PARTIDA CREADA...");
            Continuar();
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
                            RootP ListProvincias = JsonSerializer.Deserialize<RootP>(responseBody);
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
        //**************************************************
        private static void GuardarPartida(List<Personaje> jugadores)
        {
            //para json
            const string NombreArchivo = "jugadores.json";
            var miHelperdeArchivos = new HelperDeJson();
            //Guardo el archivo de jugadores formato JSON
            string jugadoresJson = JsonSerializer.Serialize(jugadores);
            miHelperdeArchivos.GuardarArchivoTexto(NombreArchivo, jugadoresJson);

        }
        private static List<Root> CargarJugadores()
        {
            List<Root> jugadores0=new List<Root>();
            string dir = @"C:\DDD\PU\2-TallerDeLenguaje1\TPTL1\TP72022RPG\rpg-2022-gudiino\";
            const string NombreArchivo = "jugadores.json";
            string ruta=dir+NombreArchivo;
            if (File.Exists(ruta))
            {
                var miHelperdeArchivos = new HelperDeJson();
                string jsonDocument = miHelperdeArchivos.AbrirArchivoTexto(ruta);
                //Root jugadoresRecuperado = JsonSerializer.Deserialize<List<Root>>(jsonDocument);
                var jRecuperado = JsonSerializer.Deserialize<List<Root>>(jsonDocument);
                return jRecuperado;
            }else{
                Console.WriteLine("No se genero ninguna Partida");
                Console.WriteLine("Salga e inicie el Juego");
                Continuar();
                return jugadores0;
            }
            
        }
        //*******************************************************
        private static void MostrarPersonaje(/*Personaje jugador*/)
        {
            string dir = @"C:\DDD\PU\2-TallerDeLenguaje1\TPTL1\TP72022RPG\rpg-2022-gudiino\";
            const string NombreArchivo = "jugadores.json";
            string ruta=dir+NombreArchivo;
            if (File.Exists(ruta))
            {
                List<Root> jugadores=CargarJugadores();
                Root jugador;
                jugador=jugadores[0];
                Console.WriteLine("Datos y Carateristicas de los Personaje");
                Personaje.MuestraDatos(jugador);
                Console.WriteLine();
                Personaje.MuestraCaracter(jugador);
            }else{
                Console.WriteLine("No se genero ninguna Partida");
                Console.WriteLine("Salga e inicie el Juego");
            }
            Continuar();
        }
        //****************************************************
        private static void Jugar(/*List<Personaje> jugadores*/)
        {
            List<Root> jugadores=CargarJugadores();
            if (jugadores.Count==0)
            {
                return;
            }
            int cantEnf=1;//para mostrar los primeros enfrentamientos
            Console.WriteLine("INICIO DEL ENFRENTAMIENTO");
            Root jugador1=jugadores[0];
            Root jugador2=jugadores[2];
            while(Personaje.ActSalud(jugador1)!=0 && Personaje.ActSalud(jugador2)!=0)
            {
                if(cantEnf==1){
                    Console.WriteLine("..............................");
                    Console.WriteLine("Ronda "+(cantEnf));
                    Console.WriteLine("Ataque jugador {0} a jugador {1}", jugador1.Nombre, jugador2.Nombre);
                    Personaje.Atacar(jugador1,jugador2);
                    Console.WriteLine("Salud jugador {0}: {1}",jugador2.Nombre,Personaje.ActSalud(jugador2));
                    if (Personaje.ActSalud(jugador2)!=0)
                    {
                        Console.WriteLine("Ataque jugador {0} a jugador {1}", jugador2.Nombre, jugador1.Nombre);
                        Personaje.Atacar(jugador2,jugador1);
                        Console.WriteLine("Salud jugador {0}: {1}",jugador1.Nombre,Personaje.ActSalud(jugador1));  
                    }
                    Console.WriteLine("..............................");
                    Console.Write("Rondas:");
                }else{
                    Personaje.Atacar(jugador1,jugador2);
                    if (Personaje.ActSalud(jugador2)!=0){
                        Personaje.Atacar(jugador2,jugador1); 
                    }
                    Console.Write("-{0}",cantEnf);
                }
                cantEnf++;
            }
            //muestra final de la salud de los jugadores
            Console.WriteLine("\nSalud jugador {0}: {1}",jugador2.Nombre,Personaje.ActSalud(jugador2));
            Console.WriteLine("Salud jugador {0}: {1}",jugador1.Nombre,Personaje.ActSalud(jugador1));
            
            Console.WriteLine("\n________________GANADOR_______________");
            Console.WriteLine("|                                    |");
            if (jugador1.Salud>jugador2.Salud)
            {
                Console.WriteLine($"              {jugador1.Nombre}                 ");
                Console.WriteLine("|____________________________________|");
                jugadores.Remove(jugador2);
                Personaje.CargarPremio(jugador1);

            }else{
                Console.WriteLine($"              {jugador2.Nombre}                ");
                Console.WriteLine("|____________________________________|");
                jugadores.Remove(jugador1);
                Personaje.CargarPremio(jugador2);
            }
        }
        private static void GuardarGanador(List<Root> jugadores)
        {//******************* guardar dato en csv
            string archivo = "Ganadores.csv";
            if (!File.Exists(archivo))//validacion de su existencia
            {
                File.Create(archivo);
            }
            //abrir el archivo para su escritura
            FileStream Fstream = new FileStream(archivo, FileMode.Open);
            // string exten="";
            DateTime actual = DateTime.Now;
            using (StreamWriter StreamW = new StreamWriter(Fstream))
            {
                foreach (Root juga in jugadores)
                {
                    StreamW.WriteLine(actual.ToString()+','+juga.Nombre);
                }
            }
            //cerrar el archivo
            Fstream.Close(); 
        }
    }
}