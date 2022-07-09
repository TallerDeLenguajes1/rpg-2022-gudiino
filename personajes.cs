using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
public class Root
        {
            public int Tipo { get; set; }
            enum Tipos{SoldadoModerno,GerreroMediaval,SoldadoInterestelar,Alien};
            public string Nombre { get; set; }
            public int Apodo { get; set; }
            enum Apodos{Destructor,Malote,Viejo,Fantasma,Nerd};
            public DateTime FecNac { get; set; }
            public int Edad { get; set; }//0 a 300
            public double Salud { get; set; }//100
            //CARACTERISTICAS
            public double Velocidad { get; set; }//1 a 10
            public double Destreza { get; set; }//1 a 5
            public double Fuerza { get; set; }//1 a 10
            public double Nivel { get; set; }//1 a 10
            public double Armadura { get; set; }//1 a 10
            public string Territorio { get; set; }
        }
namespace rpg
{
    public class Personaje
    {
        //DATOS
        public int Tipo { get; set; }
        enum Tipos{SoldadoModerno,GerreroMediaval,SoldadoInterestelar,Alien};
        public string Nombre { get; set; }
        public int Apodo { get; set; }
        enum Apodos{Destructor,Malote,Viejo,Fantasma,Nerd};
        public DateTime FecNac { get; set; }
        public int Edad { get; set; }//0 a 300
        public double Salud { get; set; }//100
        //CARACTERISTICAS
        public double Velocidad { get; set; }//1 a 10
        public double Destreza { get; set; }//1 a 5
        public double Fuerza { get; set; }//1 a 10
        public double Nivel { get; set; }//1 a 10
        public double Armadura { get; set; }//1 a 10
        public string Territorio { get; set; }
        Random numRan= new Random();
        DateTime actual = DateTime.Now;
        public Personaje(string nomm, string t){
            Tipo=numRan.Next(4);
            Nombre=nomm;
            Apodo=numRan.Next(5);
            DateTime inicio = actual.AddYears(-300);
            FecNac = inicio.AddDays(numRan.Next(0,(actual-inicio).Days));
            Salud=100;
            Velocidad=numRan.Next(1,10);
            Destreza=numRan.Next(1,5);
            Fuerza=numRan.Next(1,10);
            Nivel=numRan.Next(1,10);
            Armadura=numRan.Next(1,10);
            Edad=Edad2();
            Territorio=t;
        }
        
        public int Edad2()
        {
            var edad = actual - FecNac;
            int anios = (int)(edad.TotalDays / 365.25);
            return anios;
        }
        public static void MuestraDatos(Root personaje){
            Console.WriteLine("Datos del Personaje");
            Console.WriteLine("-------------------");
            Console.WriteLine("Tipo: {0}", Enum.GetName(typeof(Tipos), personaje.Tipo));
            Console.WriteLine("Nombre: "+personaje.Nombre);
            Console.WriteLine("Apodo: {0}", Enum.GetName(typeof(Apodos), personaje.Apodo));
            Console.WriteLine("Fecha de nacimiento: {0:d}", personaje.FecNac);
            Console.WriteLine("Edad: "+personaje.Edad+" años");
            Console.WriteLine("Provincia a cargo: {0}",personaje.Territorio);
            Console.WriteLine("Salud: "+personaje.Salud);
            Console.WriteLine();
        }
        public static void MuestraCaracter(Root personaje){
            Console.WriteLine("Caracteristicas del Personaje");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Velocidad: {0}", personaje.Velocidad);
            Console.WriteLine("Destreza: {0}", personaje.Destreza);
            Console.WriteLine("Fuerza: {0}", personaje.Fuerza);
            Console.WriteLine("Nivel: {0}", personaje.Nivel);
            Console.WriteLine("Armadura: {0}", personaje.Armadura);
        }
        //MECANICA DEL COMBATE
        //valores de ataques
        public static double PD(Root personaje){//PoderDeDisparo
            return ((double)(personaje.Destreza*personaje.Fuerza*personaje.Nivel));
        }
        public static double ED(){//EfectividadDeDisparo
            Random numRan= new Random();
            return (numRan.Next(1,100));
        }
        public static double VA(Root personaje){//ValorDeAtaque
            return PD(personaje)*ED()/100;//si ed es 100, va = pd
        }
        //valores de defensa
        public static double PDEF(Root personaje){//PoderDeDefensa
            return personaje.Armadura*personaje.Velocidad;
        }
        //resultado del enfrentamiento
        public static double MDP(){//MaximoDanioProvocable
            return 50000;
        }
        public static void DP(Root p1,Root p2){//DanioProvocado(atacante, atacado)
            double dp=Math.Abs((((VA(p1)*ED())-PDEF(p2))/MDP())*100);//el daño se va acumulado por separado o solo se resta en salud?
            p2.Salud=p2.Salud-dp;//actualizando salud
        }
        public static double ActSalud(Root p1){
            return Math.Max(0,Math.Round(p1.Salud,2));
        }
        public static void Atacar(Root p1,Root p2){
            DP(p1,p2);
        }
        public static void PremioSalud(Root p1){
            p1.Salud=p1.Salud+10;//10 puntos mas de salud
        }
        public static void PremioFuerza(Root p1){
            Random numRan= new Random();
            double az=numRan.Next(5,10);
            p1.Fuerza=p1.Fuerza*(1+az/100);// de 5 a 10 % de aumento en fuerza
        }
        public static void CargarPremio(Root p1){
            Console.WriteLine();
            Console.WriteLine("ELIJA SU PREMIO");
            Console.WriteLine("Opcion 1 --> +10 puntos de Salud");
            Console.WriteLine("Opcion 2 --> 5% a 10% de Fuerza");
            Console.Write("Ingrese Numero de Opcion: ");
            int a=Convert.ToInt32(Console.ReadLine());
            if(a==1)
            {
                Console.WriteLine("Salud: {0}", Math.Round(p1.Salud,2));
                PremioSalud(p1);
                Console.WriteLine("Salud Actual: {0}", Math.Round(p1.Salud,2));
            }else{
                Console.WriteLine("Fuerza: {0}", Math.Round(p1.Fuerza,2));
                PremioFuerza(p1);
                Console.WriteLine("Fuerza Actual: {0}", Math.Round(p1.Fuerza,2));
            }
            Console.WriteLine();
        }
        
    }
}