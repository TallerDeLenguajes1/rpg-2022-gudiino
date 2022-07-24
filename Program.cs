﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
namespace rpg
{
    class Program
    {
        static void Main(string[] args)
        {
            PRESENTACION();
            List<Personaje> jugadores=new List<Personaje>();
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
                        jugadores=IniciarPartida();
                        //List<Personaje> jugadores=Iniciar();
                        //jugadores0=jugadores;
                        break;
                    case 2:
                        MostrarPersonaje(jugadores[0]);
                        break;
                    case 3:
                        menuJugar(jugadores);
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
            String? line;
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
        }

        //***************************************************************
        private static void menu()
        {
            Console.WriteLine("------MENU-----");
            Console.WriteLine();
            Console.WriteLine("1 -> INICIAR NUEVA PARTIDA"); 
            Console.WriteLine("2 -> MOSTRAR PERSONAJE PRINCIPAL");
            //Console.WriteLine("2 -> CARGAR PARTIDA ANTERIOR");
            Console.WriteLine("3 -> JUGAR");
            //Console.WriteLine("3 -> JUGAR PARTIDA ANTERIOS");
            Console.WriteLine("4 -> MOSTRAR JUGADORES");
            Console.WriteLine("5 -> MOSTRAR GANADORES");
            Console.WriteLine("0 -> SALIR");
            Console.WriteLine();
        }
        private static void menuJugar(List<Personaje> jugadores)
        {
            Console.WriteLine("------MENU-----");
            Console.WriteLine();
            Console.WriteLine("1 -> JUGAR NUEVA PARTIDA");
            Console.WriteLine("2 -> JUGAR PARTIDA ANTERIO");
            Console.WriteLine("0 -> SALIR");
            Console.Write("Seleccion: ");
            int opc= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            switch (opc)
            {
                case 0:
                    break; 
                case 1:
                    if (jugadores.Count!=0)
                    {
                        Jugar(jugadores);
                    }else{
                        Console.WriteLine("La lista de jugadores nuevo esta vacia. Inicie una nueva partida");
                        Continuar();
                    }
                    break;
                case 2:
                    if (File.Exists("jugadores.json"))
                    {
                        List<Personaje>? jugadoresPrevio=new List<Personaje>();
                        jugadoresPrevio=CargarJugadores();
                        Jugar(jugadoresPrevio!);   
                    }else{
                        Console.WriteLine("No existe una partida anterior. Inicie una nueva partida");
                        Continuar();
                    }
                    break;
            }
            Console.WriteLine();
        }
        //***************************************************************
        private static List<Personaje> IniciarPartida()
        {
            //lista de nombres reducida
            string listaNombres = "nombres-2015.csv";//https://datos.gob.ar/dataset/otros-nombres-personas-fisicas/archivo/otros_2.21
            List<string[]> LecturaListaNom= HelperCSV.LeerCsv(listaNombres, ',');
            // foreach (var item in LecturaListaNom)
            // {
            //     Console.WriteLine("Nombre: {0}; Cantidad: {1}; Año: {2}",item[0], item[1], item[2]);
            // }
            Console.WriteLine("Ingrese su Nombre");
            String nom="JORGE GUDIÑO";//PROVISORIO
            Console.WriteLine(nom);
            //nom = Console.ReadLine();
            Console.WriteLine("Ingrese su Nombre Clave");
            String nomClav="Estudiante";//PROVISORIO
            Console.WriteLine(nomClav);
            //nomClav = Console.ReadLine();
            Console.WriteLine("Elija el numero correspondiente a la Provincia de la cual estara a cargo");
            List<string> nomprov=GetProvinciasArgentinas();
            List<string> nomClave=GetCivilizacion();
            //muestra provincias en lista
            int cant=0;
            foreach (var item in nomprov)
            {
                Console.WriteLine("Provincia {0}: {1}",cant,item);
                cant++;
            }
            Console.Write("Seleccion: ");
            int seleccion= Convert.ToInt32(Console.ReadLine());
            if (seleccion<0||seleccion>23)
            {
                int intentos=0;
                Console.WriteLine("Ingrese un numero del 0 al 23 correspondiente a unas de la provincia de la lista");
                while (intentos!=3 && (seleccion<0||seleccion>23))
                {
                    Console.Write("Seleccion: ");
                    seleccion= Convert.ToInt32(Console.ReadLine()); 
                    intentos++;  
                }
                Console.WriteLine("Intentos Fallidos, salga e intente iniciar de nuevo.");
                Continuar();
                return new List<Personaje>();
            }
            string provSelec=nomprov[seleccion];
            Console.WriteLine("Provincia: {0}",provSelec);
            nomprov.Remove(provSelec);
            Personaje jugador1 = new Personaje();
            jugador1=ConstructorPersonaje.AltaPersonaje(nom,provSelec,nomClav);
            //Personaje jugador1 = new AltaPersonaje(nom,provSelec,nomClav);
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
                //seleccion de nombre Clave al azar
                int lng3=nomClave.Count;
                int sel3=numRan.Next(0,lng3);
                string nomC=nomClave[sel3];
                nomClave.Remove(nomC);
                //pasamos los datos para generar datos personaje
                jugadores.Add(ConstructorPersonaje.AltaPersonaje(nomm[0],noomprov, nomC));
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
            Console.WriteLine();
            Console.WriteLine("PARTIDA CREADA...");
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
            List<string> NomProv= new List<string>();
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
                            RootP? ListProvincias = JsonSerializer.Deserialize<RootP>(responseBody);
                            foreach (Provincia Prov in ListProvincias!.provincias!)
                            {
                                NomProv.Add(Prov.nombre!);
                            }
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API "+ex);
            }
            return NomProv;
        }
        //*****************************************************************api nombre clave
        //************************************************************
        private static List<string> GetCivilizacion()
        {
            var url = @"https://age-of-empires-2-api.herokuapp.com/api/v1/civilizations";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            List<string> nuevo = new List<string>();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return nuevo;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            //const string NombreArchivo = "civiliz.json";
                            //var miHelperdeArchivos = new HelperDeJson();
                            string responseBody = objReader.ReadToEnd();
                            RootC? ListCivRec = JsonSerializer.Deserialize<RootC>(responseBody);
                            //Civilization ListCivilizacion = JsonSerializer.Deserialize<Civilization>(responseBody);
                            foreach (Civilization item in ListCivRec!.civilizations!)
                            {
                                nuevo.Add(item.name!);
                            }
                            // //Guardo el archivo
                            // Console.WriteLine();
                            // Console.WriteLine("--Serializando--");
                            // string CJson = JsonSerializer.Serialize(ListCivRec);
                            // Console.WriteLine("Archivo Serializado : " + ListCivRec);
                            // Console.WriteLine("--Guardando--");
                            // miHelperdeArchivos.GuardarArchivoTexto(NombreArchivo, CJson);
                            

                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Problemas de acceso a la API "+ex);
            }
            return nuevo;
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
        //****************************************************************************
        private static List<Personaje>? CargarJugadores()
        {
            List<Personaje>? jugadores0=new List<Personaje>();
            string dir ="";
            const string NombreArchivo = "jugadores.json";
            string ruta=dir+NombreArchivo;
            if (File.Exists(ruta))
            {
                var miHelperdeArchivos = new HelperDeJson();
                string jsonDocument = miHelperdeArchivos.AbrirArchivoTexto(ruta);
                //Root jugadoresRecuperado = JsonSerializer.Deserialize<List<Root>>(jsonDocument);
                List<Personaje>? jugadores = JsonSerializer.Deserialize<List<Personaje>>(jsonDocument);
                return jugadores;
            }else{
                Console.WriteLine("No se genero ninguna Partida");
                Console.WriteLine("Salga e inicie el Juego");
                Continuar();
                return jugadores0;
            }
            
        }
        //*******************************************************
        private static void MostrarPersonaje(Personaje jugador)
        { 
            Console.WriteLine("Informacion del Personaje");
            Console.WriteLine();
            Jugador.MuestraDatos(jugador);
            Console.WriteLine();
            Jugador.MuestraCaracter(jugador);
            Console.WriteLine();
        }
        //****************************************************
        private static void Jugar(List<Personaje> jugadores)
        {
            Personaje jugador1=jugadores[0];
            jugadores.Remove(jugador1);
            Random numRan= new Random();
            int a;
            int CantEnfrent=1;
            Console.WriteLine("INICIO DEL ENFRENTAMIENTOS");
            while (jugador1.Salud>0 || jugadores.Count==1)
            {
                Console.WriteLine("ENFRENTAMIENTOS {0}",CantEnfrent);
                CantEnfrent++;
                a=numRan.Next(0,jugadores.Count);
                Personaje jugador2=jugadores[a];
                jugadores.Remove(jugador2);
                Personaje ganador=Enfrentamiento(jugador1,jugador2);
                //muestra final de la salud de los jugadores
                if (ganador==jugador1)
                {
                    Console.WriteLine("\n________________GANADOR_______________");
                    Console.WriteLine("|                                    |");
                    Console.WriteLine($"              {jugador1.Nombre}                 ");
                    Console.WriteLine("|____________________________________|");
                    Jugador.CargarPremio(jugador1);
                }else{
                    Console.WriteLine("___________________________________");
                    Console.WriteLine("|                                  |");
                    Console.WriteLine("|             GAME OVER            |");
                    Console.WriteLine("|         Intentelo de NUEVO       |");
                    Console.WriteLine("|__________________________________|");
                    Continuar();
                }
                GuardarGanador(ganador);
            }
        }
        //****************************************************************************************************
        private static Personaje Enfrentamiento(Personaje jugador1, Personaje jugador2)
        {
            int cantEnf=1;
            Console.WriteLine("Cantidad de ataques por Turno: 3");
            Console.WriteLine("Jugador 1--> Nombre: {0}; Territorio: {1}",jugador1.Nombre,jugador1.Territorio);
            Console.WriteLine("Jugador 2--> Nombre: {0}; Territorio: {1}",jugador2.Nombre,jugador2.Territorio);
            Console.Write("Ronda");
            while(Jugador.ActSalud(jugador1)!=0 && Jugador.ActSalud(jugador2)!=0)
            {
                Console.Write(" -> {0}",cantEnf);
                for (int i = 0; i < 3; i++){
                    Jugador.Atacar(jugador1,jugador2);
                }
                if (Jugador.ActSalud(jugador2)!=0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Jugador.Atacar(jugador2,jugador1);
                    }
                }
                cantEnf++;
            }
            Console.Write(" ->FIN");
            Console.WriteLine();
            if (Jugador.ActSalud(jugador1)==0)
            {
                Console.WriteLine("Ganador: {0}",jugador2.Nombre);
                return jugador2;
            }else{
                Console.WriteLine("Ganador: {0}",jugador1.Nombre);
                return jugador1;
            }
        }
        private static void GuardarGanador(Personaje ganador)
        {//******************* guardar dato en csv
            string archivo = "Ganadores.csv";
            List<string[]> nuevo=HelperCSV.LeerCsv(archivo,',');
            DateTime actual = DateTime.Now;
            string[] linea = {actual.ToString(),ganador.Nombre!,ganador.Territorio!};
            nuevo.Add(linea);
            HelperCSV.GuardarCSV(archivo,nuevo);
        }
    }
}