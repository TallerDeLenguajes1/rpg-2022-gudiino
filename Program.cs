using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
Console.WriteLine("Inicializacion de la carga de Datos del Personaje");
Console.WriteLine("Ingrese Nombre Jugador 1");
String nom="JORGE";//PROVISORIO
Console.WriteLine(nom);
//nom = Console.ReadLine();
Personaje jugador1 = new Personaje(nom);
// jugador1 = new Personaje(nom);
// jugador1 = new Personaje();
Console.WriteLine("Ingrese Nombre Jugador 2");
String nom2="GUDIÑO";//PROVISORIO
Console.WriteLine(nom2);
// //nom2 = Console.ReadLine();
Personaje jugador2 = new Personaje(nom2);
var jugadores = new List<Personaje> {jugador1, jugador2};
// jugador2 = new Personaje(nom2);
// jugador2 = new Personaje();
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
Console.WriteLine("INICIO DEL ENFRENTAMIENTO");
// for (int i = 0; i < 3; i++)
// {
//     Console.WriteLine("\n..............................");
//     Console.WriteLine("Ronda "+(i+1));
//     Console.WriteLine("Ataque jugador 1 a jugador 2");
//     jugador1.Atacar(jugador2);
//     Console.WriteLine("Salud jugador 2: "+jugador2.ActSalud());
//     Console.WriteLine("Ataque jugador 2 a jugador 1");
//     jugador2.Atacar(jugador1);
//     Console.WriteLine("Salud jugador 1: "+jugador1.ActSalud());
// }
cantEnf=1;
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
Console.WriteLine("\nSalud jugador {0}: {1}",jugador2.Nombre,jugador2.ActSalud());
Console.WriteLine("Salud jugador {0}: {1}",jugador1.Nombre,jugador1.ActSalud());
// for (int i = 0; i < 3; i++)
// {
//     Console.WriteLine("\n..............................");
//     Console.WriteLine("Ronda "+(i+1));
//     Console.WriteLine("Ataque jugador 1 a jugador 2");
//     jugador1.Atacar(jugador2);
//     Console.WriteLine("Salud jugador 2: "+jugador2.ActSalud());
//     Console.WriteLine("Ataque jugador 2 a jugador 1");
//     jugador2.Atacar(jugador1);
//     Console.WriteLine("Salud jugador 1: "+jugador1.ActSalud());
// }
Console.WriteLine("\n________________GANADOR_______________");
Console.WriteLine("|                                    |");
if (jugador1.ActSalud()>jugador2.ActSalud())
{
    Console.WriteLine($"|              {jugador1.Nombre}                 |");
    Console.WriteLine("|        Premio: +10 en Salud        |");
    jugador1.Ganador();
    Console.WriteLine("|           Salud: {0}             |",jugador1.ActSalud());
    jugadores.Remove(jugador2);
}else{
    Console.WriteLine($"|              {jugador2.Nombre}                |");
    Console.WriteLine("|        Premio: +10 en Salud        |");
    jugador2.Ganador();
    Console.WriteLine("|           Salud: {0}             |",jugador2.ActSalud());
    jugadores.Remove(jugador1);
}
Console.WriteLine("|                                    |");
Console.WriteLine("|____________________________________|");
string ruta = @"C:\Users\USUARIO\Desktop\DirectorioTP8\Ganadores.cvs";
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
        StreamW.WriteLine(actual.ToString()+'.'+juga.Nombre);//asignamo la extencion del arreglo en base al tamaño menos uno
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
    string[] Fila = linea.Split('.');
    //escribir aqui los datos a mostrar en pantalla
}
Console.WriteLine("**************************************");
Console.WriteLine("\nFin.");