using System;
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
// jugador2 = new Personaje(nom2);
// jugador2 = new Personaje();
Console.WriteLine("************************");
Console.WriteLine("Datos y Carateristicas de los personajes");
Console.WriteLine();
Console.WriteLine("************************");
Console.WriteLine("Jugador Numero 1");
jugador1.MuestraDatos();
Console.WriteLine();
jugador1.MuestraCaracter();
Console.WriteLine();
Console.WriteLine("************************");
Console.WriteLine("Jugador Numero 2");
jugador2.MuestraDatos();
Console.WriteLine();
jugador2.MuestraCaracter();
Console.WriteLine();
Console.WriteLine("INICIO DEL ENFRENTAMIENTO");
int cantEnf=1;
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
}else{
    Console.WriteLine($"|              {jugador2.Nombre}                |");
}
Console.WriteLine("|                                    |");
Console.WriteLine("**************************************");
Console.WriteLine("\nFin.");