using System;
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
                string? opcaux=Console.ReadLine();
                //opc= Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                if(int.TryParse(opcaux,out opc)&& opc>=0 && opc<=6)
                {
                    switch (opc)
                    {
                        case 0:
                            Console.WriteLine("============================");
                            Console.WriteLine("  ADIOS, GRACIAS POR JUGAR ");
                            Console.WriteLine("             A             ");
                            Console.WriteLine("     BATALLA PROVINCIAL    ");
                            Console.WriteLine("============================");
                            opcaux="0";
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 1:
                            jugadores=IniciarPartida();
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 2:
                            MostrarPersonaje(jugadores);
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 3:
                            menuJugar(jugadores);
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 4:
                            MenuMostrar(jugadores);
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 5:
                            MostrarGanadores();
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 6:
                            Console.WriteLine("====================================================================");
                            Console.WriteLine("INICIE UNA NUEVA PARTIDA Y SELECCIONE REEMPLAZAR LA PARTIDA GUARDADA");
                            Console.WriteLine("====================================================================");
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                    }
                }else{
                    Console.WriteLine("===========================");
                    Console.WriteLine(" Ingrese una opcion Valida");
                    Console.WriteLine("===========================");
                    opc=1;
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("\nFIN PROGRAMA.");
        }
        //funciones o metodos
        //****************************************************************
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
            Console.WriteLine("ENTER para IR al MENU");
            Console.ReadLine();
            Console.WriteLine();
        }
        //*****************************************************************
        private static void Continuar()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER para VOLVER al MENU");
            Console.ReadLine();
        }
        //***************************************************************
        private static void menu()
        {
            Console.WriteLine("     MENU PRINCIPAL");
            Console.WriteLine("************************");
            Console.WriteLine();
            Console.WriteLine("1 -> CARGAR NUEVA PARTIDA"); 
            Console.WriteLine("2 -> MOSTRAR PERSONAJE PRINCIPAL");
            Console.WriteLine("3 -> JUGAR UNA PARTIDA");
            Console.WriteLine("4 -> MOSTRAR JUGADORES");
            Console.WriteLine("5 -> MOSTRAR GANADORES");
            Console.WriteLine("6 -> ELIMINAR PARTIDA GUARDADA");
            Console.WriteLine("0 -> SALIR");
            Console.WriteLine();
        }
        //******************************************************************
        private static void menuJugar(List<Personaje> jugadores)
        {
            int opc=1;
            while(opc!=0)
            {
                string? opcaux="";
                if(jugadores.Count!=0||File.Exists("jugadores.json"))
                {
                    Console.WriteLine("    MENU JUGAR");
                    Console.WriteLine("*******************");
                    Console.WriteLine();
                    if (jugadores.Count!=0)
                    {
                        Console.WriteLine("1 -> NUEVA PARTIDA");
                        if(File.Exists("jugadores.json"))
                        {
                            Console.WriteLine("2 -> PARTIDA GUARDADA");
                        }
                    }else{
                        if(File.Exists("jugadores.json"))
                        {
                            Console.WriteLine("2 -> PARTIDA GUARDADA");
                        }else{
                            Console.WriteLine("NO HAY PARTIDA NUEVA INICIADA");
                            Console.WriteLine("NO HAY PARTIDA GUARDADA");
                            Console.WriteLine("SALGA, INICIE Y GUARDE UNA NUEVA PARTIDA");
                            opc=0;
                        }
                    }
                }
                if(jugadores.Count!=0||File.Exists("jugadores.json"))
                {
                    Console.WriteLine("0 -> SALIR");
                    Console.Write("Seleccion: ");
                    opcaux=Console.ReadLine();
                }                
                Console.WriteLine();
                if(int.TryParse(opcaux,out opc)&& opc>=0 && opc<=2)
                {
                //int opc= Convert.ToInt32(Console.ReadLine());
                    switch (opc)
                    {
                        case 0:
                            break; 
                        case 1:
                            if (jugadores.Count!=0)
                            {
                                List<Personaje>? jugadoresAux=new List<Personaje>(jugadores);
                                Jugar2(jugadoresAux);
                            }else{
                                Console.WriteLine("La lista de jugadores nuevo esta vacia. Inicie una nueva partida");
                                Continuar();
                                opc=0;
                            }
                            break;
                        case 2:
                            if (File.Exists("jugadores.json"))
                            {
                                List<Personaje>? jugadoresPrevio=new List<Personaje>();
                                jugadoresPrevio=CargarJugadores();
                                Jugar2(jugadoresPrevio!);   
                            }else{
                                Console.WriteLine("No existe una partida anterior. Inicie una nueva partida y guardela");
                                Continuar();
                                opc=0;
                            }
                            break;
                    }
                    Console.WriteLine();
                }else{
                    Console.WriteLine("===========================");
                    Console.WriteLine(" Ingrese una opcion Valida");
                    Console.WriteLine("===========================");
                    opc=1;
                    Console.WriteLine();
                }
            }
        }
        //***************************************************************
        private static List<Personaje> IniciarPartida()
        {
            Random numRan= new Random();
            List<Personaje> jugadores=new List<Personaje>();
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
            if(nomprov.Count!=0&&nomClave.Count!=0)
            {
                //muestra provincias en lista
                int cant=0;
                foreach (var item in nomprov)
                {
                    Console.WriteLine("Provincia {0}: {1}",cant,item);
                    cant++;
                }
                int seleccion=numRan.Next(24);
                Console.WriteLine("Seleccion: {0}",seleccion);
                // int seleccion= Convert.ToInt32(Console.ReadLine());
                // if (seleccion<0||seleccion>23)
                // {
                //     int intentos=0;
                //     Console.WriteLine("Ingrese un numero del 0 al 23 correspondiente a unas de la provincia de la lista");
                //     while (intentos!=3 && (seleccion<0||seleccion>23))
                //     {
                //         Console.Write("Seleccion: ");
                //         seleccion= Convert.ToInt32(Console.ReadLine()); 
                //         intentos++;  
                //     }
                //     Console.WriteLine("Intentos Fallidos, salga e intente iniciar de nuevo.");
                //     Continuar();
                //     return new List<Personaje>();
                // }
                string provSelec=nomprov[seleccion];
                Console.WriteLine("Provincia: {0}",provSelec);
                Console.WriteLine();
                nomprov.Remove(provSelec);
                Personaje jugador1 = new Personaje();
                jugador1=ConstructorPersonaje.AltaPersonaje(nom,provSelec,nomClav);
                Console.WriteLine("DATOS Y CARACTERISTICAS JUGADOR PRINCIPAL");
                Console.WriteLine();
                Jugador.MuestraDatos(jugador1);
                Console.WriteLine();
                Jugador.MuestraCaracter(jugador1);
                Console.WriteLine();
                //Personaje jugador1 = new AltaPersonaje(nom,provSelec,nomClav);
                jugadores.Add(jugador1);
                //carga al azar nombres de pesonajes de una lista de nombres
                Console.WriteLine("CARGANDO DATOS DE LOS OPONENTES...");
                cant=0;
                while(cant!=23)
                {    //seleccion de nombres al azar
                    int lng=LecturaListaNom.Count;
                    int sel=numRan.Next(lng);
                    string[] nomm=LecturaListaNom[sel];
                    LecturaListaNom.Remove(nomm);
                    //seleccion de provincias al azar
                    int lng2=nomprov.Count;
                    int sel2=numRan.Next(lng2);
                    string noomprov=nomprov[sel2];
                    nomprov.Remove(noomprov);
                    //seleccion de nombre Clave al azar
                    int lng3=nomClave.Count;
                    int sel3=numRan.Next(lng3);
                    string nomC=nomClave[sel3];
                    nomClave.Remove(nomC);
                    //pasamos los datos para generar datos personaje
                    jugadores.Add(ConstructorPersonaje.AltaPersonaje(nomm[0],noomprov, nomC));
                    cant++;
                }
                Console.WriteLine("DATOS DE LOS OPONENTES CARGADOS.");
                Console.WriteLine();
                if (File.Exists("jugadores.json"))
                {
                    Console.WriteLine("Ud. tiene una partida guardada");
                    Console.WriteLine("¿QUIERE REMPLAZARLA CON LA NUEVA PARTIDA?");
                }else{
                    Console.WriteLine("¿QUIERE GUARDAR LA NUEVA PARTIDA CREADA?");
                }
                Console.WriteLine("1--> SI");
                Console.WriteLine("2--> NO");
                Console.Write("Seleccion: ");
                //******
                string? opcaux=Console.ReadLine();
                int seleccion2;
                Console.WriteLine();
                if(!(int.TryParse(opcaux,out seleccion2)&& seleccion2>=1 && seleccion2<=2))
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Ingreso una opcion invalida");
                    Console.WriteLine("LA PARTIDA SE INICIARA PERO NO SE GUARDARA");
                    Console.WriteLine("==========================================");
                    seleccion2=2;
                }
                //**
                //int seleccion2= Convert.ToInt32(Console.ReadLine());
                if (seleccion2==1)
                {
                    Console.WriteLine();
                    GuardarPartida(jugadores);
                    Console.WriteLine("PARTIDA CREADA Y GUARDADA...");
                    Console.WriteLine("AHORA PUEDE SALIR Y JUGAR A SU NUEVA PARTIDA");
                }else
                {
                    Console.WriteLine();
                    Console.WriteLine("PARTIDA CREADA...");
                    Console.WriteLine("AHORA PUEDE SALIR Y JUGAR  A SU NUEVA PARTIDA");
                }
                Console.WriteLine();
                Continuar();
            }else{
                Console.WriteLine("==============================");
                Console.WriteLine("ERROR AL INICIAR LA PARTIDA");
                Console.WriteLine("==============================");
                Console.WriteLine(" INTENTELO DE NUEVO");
                Console.WriteLine("==============================");
            }
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
                Console.WriteLine("==============================");
                Console.WriteLine(" Problemas de acceso a la API \n"+ex.Message);
                Console.WriteLine("==============================");
            }
            return NomProv;
        }
        //********************************************prueba api con HttpClient
        private static readonly HttpClient client = new HttpClient();
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
        private static void MostrarPersonaje(List<Personaje> jugadores)
        { 
            Console.WriteLine("Informacion del Personaje principal");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("PARTIDA NUEVA");
            Console.WriteLine("*************");
            if(jugadores.Count!=0)
            {
                Console.WriteLine();
                Jugador.MuestraDatos(jugadores[0]);
                Console.WriteLine();
                Jugador.MuestraCaracter(jugadores[0]);
                Console.WriteLine();
            }else{
                Console.WriteLine("NO HAY PARTIDA NUEVA");
                Console.WriteLine("Salga e inicie una nueva partida");
            }
            Console.WriteLine();
            Console.WriteLine("PARTIDA GUARDADA");
            Console.WriteLine("*****************");
            string dir ="";
            const string NombreArchivo = "jugadores.json";
            string ruta=dir+NombreArchivo;
            if(File.Exists(ruta))
            {
                var miHelperdeArchivos = new HelperDeJson();
                string jsonDocument = miHelperdeArchivos.AbrirArchivoTexto(ruta);
                List<Personaje>? jugadores2 = JsonSerializer.Deserialize<List<Personaje>>(jsonDocument);
                Console.WriteLine();
                Jugador.MuestraDatos(jugadores2![0]);
                Console.WriteLine();
                Jugador.MuestraCaracter(jugadores2[0]);
                Console.WriteLine();
            }else{
                Console.WriteLine("NO HAY PARTIDA GUARDADA");
                Console.WriteLine("Salga e inicie una nueva partida y guarde los datos");
            }
            Continuar();
            Console.WriteLine();
        }
        //****************************************************
        // private static void Jugar(List<Personaje> jugadores)
        // {
        //     Personaje jugador1=jugadores[0];
        //     jugadores.Remove(jugador1);
        //     Random numRan= new Random();
        //     int a;
        //     int CantEnfrent=1;
        //     Console.WriteLine("INICIO DEL ENFRENTAMIENTOS");
        //     while (jugador1.Salud>0 || jugadores.Count==1)
        //     {
        //         Console.WriteLine("ENFRENTAMIENTOS {0}",CantEnfrent);
        //         CantEnfrent++;
        //         a=numRan.Next(0,jugadores.Count);
        //         Personaje jugador2=jugadores[a];
        //         jugadores.Remove(jugador2);
        //         Personaje ganador=Enfrentamiento(jugador1,jugador2);
        //         //muestra final de la salud de los jugadores
        //         if (ganador==jugador1)
        //         {
        //             Console.WriteLine("\n________________GANADOR_______________");
        //             Console.WriteLine("|                                    |");
        //             Console.WriteLine($"              {jugador1.Nombre}                 ");
        //             Console.WriteLine("|____________________________________|");
        //             Jugador.CargarPremio(jugador1);
        //         }else{
        //             Console.WriteLine("___________________________________");
        //             Console.WriteLine("|                                  |");
        //             Console.WriteLine("|             GAME OVER            |");
        //             Console.WriteLine("|         Intentelo de NUEVO       |");
        //             Console.WriteLine("|__________________________________|");
        //             Continuar();
        //         }
        //         GuardarGanador(ganador);
        //     }
        // }
                //****************************************************
        private static void Jugar2(List<Personaje> jugadores)
        {
            if (jugadores.Count>0)
            {
                Personaje jugador0=jugadores[0];
                Random numRan= new Random();
                int CantEnfrent=1;
                Console.WriteLine("INICIO DEL ENFRENTAMIENTOS");
                while (jugadores.Count>1)
                {
                    Personaje jugador1=jugadores[numRan.Next(jugadores.Count)];
                    jugadores.Remove(jugador1);
                    Personaje jugador2=jugadores[numRan.Next(jugadores.Count)];
                    jugadores.Remove(jugador2);
                    if (jugador0==jugador1||jugador0==jugador2)
                    {
                        Console.WriteLine("ENFRENTAMIENTOS {0}",CantEnfrent);
                        CantEnfrent++;
                        Console.WriteLine("Cantidad de ataques por Turno: 3");
                        Console.WriteLine("Jugador 1 --> Nombre: {0}; Territorio: {1}",jugador1.Nombre,jugador1.Territorio);
                        Console.WriteLine("Jugador 2 --> Nombre: {0}; Territorio: {1}",jugador2.Nombre,jugador2.Territorio);
                    }
                    Personaje ganador=Enfrentamiento2(jugador1,jugador2);
                    if (jugador0==jugador1||jugador0==jugador2)
                    {
                        Console.WriteLine("Ganador: {0}",ganador.Nombre);
                        Console.WriteLine();
                    }
                    //muestra final del ganador
                    // if (jugadores.Count>1)
                    // {
                    //     Jugador.CargarPremio(ganador);
                    // }
                    if (ganador==jugador1)
                    {
                        jugadores.Add(jugador1);
                    }else{
                        jugadores.Add(jugador2);
                    }
                    if (jugadores.Count==1)
                    {
                        GuardarGanador(ganador);
                        if (ganador==jugador0)
                        {
                            Console.WriteLine("\n         GANADOR FINAL");
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine($"         {ganador.Nombre}            ");
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
                            Jugador.CargarPremio(ganador);
                        }else{
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine("             GAME OVER            ");
                            Console.WriteLine("         Intentelo de NUEVO       ");
                            Console.WriteLine("      Inicie una NUEVA PARTIDA    ");
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            Console.WriteLine($"Ganador Final: {ganador.Nombre}");
                            Jugador.CargarPremio(ganador);
                            Console.WriteLine();
                            Console.WriteLine("PARA VOLVER A JUGAR, VUELVA AL MENU ANTERIOR");
                            Continuar();
                        }
                    }
                }
            }else{
                Console.WriteLine("Inicie una Nueva Partida");
                Continuar();
            }
        }
        //****************************************************************************************************
        // private static Personaje Enfrentamiento(Personaje jugador1, Personaje jugador2)
        // {
        //     int cantEnf=1;
        //     Console.WriteLine("Cantidad de ataques por Turno: 3");
        //     Console.WriteLine("Jugador 1 --> Nombre: {0}; Territorio: {1}",jugador1.Nombre,jugador1.Territorio);
        //     Console.WriteLine("Jugador 2 --> Nombre: {0}; Territorio: {1}",jugador2.Nombre,jugador2.Territorio);
        //     Console.Write("Ronda");
        //     while(Jugador.ActSalud(jugador1)!=0 && Jugador.ActSalud(jugador2)!=0)
        //     {
        //         Console.Write(" -> {0}",cantEnf);
        //         for (int i = 0; i < 3; i++){
        //             Jugador.Atacar(jugador1,jugador2);
        //         }
        //         if (Jugador.ActSalud(jugador2)!=0)
        //         {
        //             for (int i = 0; i < 3; i++)
        //             {
        //                 Jugador.Atacar(jugador2,jugador1);
        //             }
        //         }
        //         cantEnf++;
        //     }
        //     Console.Write(" ->FIN");
        //     Console.WriteLine();
        //     if (Jugador.ActSalud(jugador1)==0)
        //     {
        //         Console.WriteLine("Ganador: {0}",jugador2.Nombre);
        //         return jugador2;
        //     }else{
        //         Console.WriteLine("Ganador: {0}",jugador1.Nombre);
        //         return jugador1;
        //     }
        // }
          //****************************************************************************************************
        private static Personaje Enfrentamiento2(Personaje jugador1, Personaje jugador2)
        {
            while(Jugador.ActSalud(jugador1)>0 && Jugador.ActSalud(jugador2)>0)
            {
                for (int i = 0; i < 3; i++){
                    Jugador.Atacar(jugador1,jugador2);
                }
                if (Jugador.ActSalud(jugador2)>0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Jugador.Atacar(jugador2,jugador1);
                    }
                }
            }
            if (Jugador.ActSalud(jugador1)==0)
            {
                return jugador2;
            }else{
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
        //******************************************************
        private static void MenuMostrar(List<Personaje> jugadores)
        {
            int opc=1;
            while(opc!=0)
            {
                if(jugadores.Count!=0||File.Exists("jugadores.json"))
                {
                    Console.WriteLine("MENU MUESTRA JUGADORES");
                    Console.WriteLine("**********************");
                    Console.WriteLine();
                    if (jugadores.Count!=0)
                    {
                        Console.WriteLine("1 -> NUEVA PARTIDA");
                        if(File.Exists("jugadores.json"))
                        {
                            Console.WriteLine("2 -> PARTIDA GUARDADA");
                        }
                    }else{
                        if(File.Exists("jugadores.json"))
                        {
                            Console.WriteLine("2 -> PARTIDA GUARDADA");
                        }else{
                            Console.WriteLine("NO HAY PARTIDA NUEVA INICIADA");
                            Console.WriteLine("NO HAY PARTIDA GUARDADA");
                            Console.WriteLine("SALGA, INICIE Y GUARDE UNA NUEVA PARTIDA");
                            Continuar();
                        }
                    }
                }
                if(jugadores.Count!=0||File.Exists("jugadores.json"))
                {
                    Console.WriteLine("0 -> SALIR");
                    Console.Write("Seleccion: ");
                    //opc= Convert.ToInt32(Console.ReadLine());
                    //*******
                    Console.Write("Seleccion: ");
                    string? opcaux=Console.ReadLine();
                    //opc= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if(!(int.TryParse(opcaux,out opc)&& opc>=0 && opc<=2))
                    {
                        Console.WriteLine("===========================");
                        Console.WriteLine(" Ingrese una opcion Valida");
                        Console.WriteLine("===========================");
                        opc=10;
                        Console.WriteLine();
                    }
                }else{
                    opc=0;
                }
                
                Console.WriteLine();
                switch (opc)
                {
                    case 0:
                        break; 
                    case 1:
                        if (jugadores.Count!=0)
                        {
                            MostrarJugadores(jugadores);
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
                            MostrarJugadores(jugadoresPrevio!);   
                        }else{
                            Console.WriteLine("No existe una partida anterior. Inicie una nueva partida");
                            Continuar();
                        }
                        break;
                }
                Console.WriteLine();
            }
        }
        private static void MostrarJugadores(List<Personaje> jugadores)
        {
            int cont=1;
            Console.WriteLine("JUGADORES");
            Console.WriteLine("**********");
            Console.WriteLine();
            foreach (var item in jugadores)
            {
                Console.WriteLine("{0} --> Nombre: {1}; Territorio a Cargo: {2}",cont,item.Nombre,item.Territorio);
                cont++;
            }
            Continuar();
        }
        private static void MostrarGanadores()
        {
            string archivo = "Ganadores.csv";
            if (File.Exists(archivo))//validacion de su existencia
            {
                List<string[]> nuevo=HelperCSV.LeerCsv(archivo,',');
                Console.WriteLine("GANADORES");
                Console.WriteLine("**********");
                Console.WriteLine();
                foreach (var item in nuevo)
                {
                    Console.WriteLine("Fecha {0} --> Nombre: {1}; Territorio a Cargo: {2}",item[0],item[1],item[2]);
                }
            }else{
                Console.WriteLine("Aun NO hay lista de ganadores. Empiece una nueva partida y conviertase en el primer ganador");
            }
            Continuar();
        }
    }
}