using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace rpg
{
    public class Personaje
    {
        //DATOS
        public enum Tipos{Flematico,Colerico,Sanguineo,Apatico, Apasionado, Sentimental, Nervioso, Amorfo};
        public int Tipo { get; set; }
        public string? Nombre { get; set; }
        public string? Apodo { get; set; }
        public DateTime FecNac { get; set; }
        public int Edad { get; set; }//0 a 300
        public double Salud { get; set; }//100
        //CARACTERISTICAS
        public double Velocidad { get; set; }//1 a 10
        public double Destreza { get; set; }//1 a 5
        public double Fuerza { get; set; }//1 a 10
        public double Nivel { get; set; }//1 a 10
        public double Armadura { get; set; }//1 a 10
        public string? Territorio { get; set; }
    }
    public class ConstructorPersonaje
    {
        public static Personaje AltaPersonaje(string nomm, string t, string nomC)
        {
            Personaje nuevo= new Personaje();
            Random numRan= new Random();
            DateTime actual = DateTime.Now;
            nuevo.Tipo=numRan.Next(8);
            nuevo.Nombre=nomm;
            nuevo.Apodo=nomC;
            DateTime inicio = actual.AddYears(-300);//ver
            nuevo.FecNac = inicio.AddDays(numRan.Next(0,(actual-inicio).Days));
            nuevo.Edad=Edad2(nuevo.FecNac);
            nuevo.Salud=100;
            nuevo.Velocidad=numRan.Next(1,10);
            nuevo.Destreza=numRan.Next(1,5);
            nuevo.Fuerza=numRan.Next(1,10);
            nuevo.Nivel=numRan.Next(1,10);
            nuevo.Armadura=numRan.Next(1,10);
            nuevo.Territorio=t;
            return nuevo;
        }
        public static int Edad2(DateTime FecNac)
        {
            DateTime actual = DateTime.Now;
            var edad = actual - FecNac;
            int anios = (int)(edad.TotalDays / 365.25);
            return anios;
        }
    }
    public class Jugador
    {
        public static void MuestraDatos(Personaje personaje){
            Console.WriteLine("Datos del Personaje");
            Console.WriteLine("-------------------");
            Console.WriteLine("Tipo de Caracter:............. {0}", Enum.GetName(typeof(Personaje.Tipos), personaje.Tipo));
            Console.WriteLine("Nombre:....................... "+personaje.Nombre);
            Console.WriteLine("Nombre Clave:................. {0}", personaje.Apodo);
            Console.WriteLine("Fecha de nacimiento:.......... {0:d}", personaje.FecNac);
            Console.WriteLine("Edad:......................... "+personaje.Edad+" años");
            Console.WriteLine("Provincia a cargo:............ {0}",personaje.Territorio);
            Console.WriteLine("Estado Territorio:............ {0}%",personaje.Salud);
            Console.WriteLine();
        }
        public static void MuestraCaracter(Personaje personaje){
            Console.WriteLine("Caracteristicas del Ejercito");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Velocidad de Repuesta:........ {0}/10", personaje.Velocidad);
            Console.WriteLine("Destreza Del Ejercito:........ {0}/5", personaje.Destreza);
            Console.WriteLine("Fuerza Ofensiva:.............. {0}/10", personaje.Fuerza);
            Console.WriteLine("Nivel de ataque:.............. {0}/10", personaje.Nivel);
            Console.WriteLine("Fuerza Defensiva:............. {0}/10", personaje.Armadura);
        }
        //MECANICA DEL COMBATE
        //valores de ataques
        public static double PD(Personaje personaje){//PoderDeDisparo
            return ((double)(personaje.Destreza*personaje.Fuerza*personaje.Nivel));
        }
        public static double ED(){//EfectividadDeDisparo
            Random numRan= new Random();
            return (numRan.Next(1,100));
        }
        public static double VA(Personaje personaje){//ValorDeAtaque
            return PD(personaje)*ED()/100;//si ed es 100, va = pd
        }
        //valores de defensa
        public static double PDEF(Personaje personaje){//PoderDeDefensa
            return personaje.Armadura*personaje.Velocidad;
        }
        //resultado del enfrentamiento
        public static double MDP(){//MaximoDanioProvocable
            return 50000;
        }
        public static void DP(Personaje p1,Personaje p2){//DanioProvocado(atacante, atacado)
            double dp=Math.Abs((((VA(p1)*ED())-PDEF(p2))/MDP())*100);//el daño se va acumulado por separado o solo se resta en salud?
            p2.Salud=p2.Salud-dp;//actualizando salud
        }
        public static double ActSalud(Personaje p1){
            return Math.Max(0,Math.Round(p1.Salud,2));
        }
        public static void Atacar(Personaje p1,Personaje p2){
            DP(p1,p2);
        }
        public static void PremioSalud(Personaje p1){
            p1.Salud=p1.Salud+10;//10 puntos mas de salud
        }
        public static double PremioFuerza(double p1){
            Random numRan= new Random();
            double az=numRan.Next(5,10);
            p1=p1*(1+az/100);// de 5 a 10 % de aumento en fuerza
            return p1;
        }
        public static void CargarPremio(Personaje p1){
            Console.WriteLine();
            Console.WriteLine("ELIJA SU PREMIO");
            Console.WriteLine();
            Console.WriteLine("Aumento en capacidad Ofensiva");
            Console.WriteLine("Opcion 1 --> 5% a 10% mas de Fuerza ofensiva");
            Console.WriteLine("Opcion 2 --> 5% a 10% mas de Deztreza Ejercito");
            Console.WriteLine("Opcion 3 --> 5% a 10% mas de Nivel de ataque");
            Console.WriteLine();
            Console.WriteLine("Aumento en capacidad Defensiva");
            Console.WriteLine("Opcion 4 --> +10 puntos restauracion Territorio");
            Console.WriteLine("Opcion 5 --> 5% a 10% mas de Velocidad de Repuesta");
            Console.WriteLine("Opcion 6 --> 5% a 10% mas de Fuerza Defensiva");
            Console.WriteLine();
            Console.Write("Ingrese Numero de Opcion: ");
            //int a=Convert.ToInt32(Console.ReadLine());
            Random numRan= new Random();
            int a;
            a=numRan.Next(1,6);
            Console.Write("{0}",a);
            Console.WriteLine();
            switch (a)
            {
                case 1:
                    Console.WriteLine("Fuerza Ofensiva: {0}", Math.Round(p1.Fuerza,2));
                    p1.Fuerza=PremioFuerza(p1.Fuerza);
                    Console.WriteLine("Fuerza Ofensiva Actual: {0}", Math.Round(p1.Fuerza,2));
                    break;
                case 2:
                    Console.WriteLine("Destreza Ejercito: {0}", Math.Round(p1.Destreza,2));
                    p1.Destreza=PremioFuerza(p1.Destreza);
                    Console.WriteLine("Destreza Ejercito Actual: {0}", Math.Round(p1.Destreza,2));
                    break;
                case 3:
                    Console.WriteLine("Nivel de Ataque: {0}", Math.Round(p1.Nivel,2));
                    p1.Nivel=PremioFuerza(p1.Nivel);
                    Console.WriteLine("Nivel de Ataque Actual: {0}", Math.Round(p1.Nivel,2));
                    break;
                case 4:
                    Console.WriteLine("Salud: {0}", Math.Round(p1.Salud,2));
                    PremioSalud(p1);
                    Console.WriteLine("Salud Actual: {0}", Math.Round(p1.Salud,2));
                    break; 
                case 5:
                    Console.WriteLine("Velocidad de repuesta: {0}", Math.Round(p1.Velocidad,2));
                    p1.Velocidad=PremioFuerza(p1.Velocidad);
                    Console.WriteLine("Velocidad de repuesta Actual: {0}", Math.Round(p1.Velocidad,2));
                    break;
                case 6:
                    Console.WriteLine("Fuerza Defensiva: {0}", Math.Round(p1.Armadura,2));
                    p1.Armadura=PremioFuerza(p1.Armadura);
                    Console.WriteLine("Fuerza Defensiva Actual: {0}", Math.Round(p1.Armadura,2));
                    break; 
            }
            Console.WriteLine();
        }
        
    }
}