using System;
public class Personaje
{
    //DATOS
    public int Tipo { get; set; }
    public enum Tipos{SoldadoModerno,GerreroMediaval,SoldadoInterestelar,Alien};
    public string Nombre { get; set; }
    public int Apodo { get; set; }
    public enum Apodos{Destructor,Malote,Viejo,Fantasma,Nerd};
    public DateTime FecNac { get; set; }
    public int Edad { get; set; }//0 a 300
    public double Salud { get; set; }//100
    //CARACTERISTICAS
    public double Velocidad { get; set; }//1 a 10
    public double Destreza { get; set; }//1 a 5
    public double Fuerza { get; set; }//1 a 10
    public double Nivel { get; set; }//1 a 10
    public double Armadura { get; set; }//1 a 10
    public string Territorio;
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
   
    public void MuestraDatos(){
        Console.WriteLine("Datos del Personaje");
        Console.WriteLine("-------------------");
        Console.WriteLine("Tipo: {0}", Enum.GetName(typeof(Tipos), Tipo));
        Console.WriteLine("Nombre: "+Nombre);
        Console.WriteLine("Apodo: {0}", Enum.GetName(typeof(Apodos), Apodo));
        Console.WriteLine("Fecha de nacimiento: {0:d}", FecNac);
        Console.WriteLine("Edad: "+Edad2()+" años");
        Console.WriteLine("Provincia a cargo: {0}",Territorio);
        Console.WriteLine("Salud: "+Salud);
        Console.WriteLine();
    }
    public void MuestraCaracter(){
        Console.WriteLine("Caracteristicas del Personaje");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Velocidad: {0}", Velocidad);
        Console.WriteLine("Destreza: {0}", Destreza);
        Console.WriteLine("Fuerza: {0}", Fuerza);
        Console.WriteLine("Nivel: {0}", Nivel);
        Console.WriteLine("Armadura: {0}", Armadura);
    }
    //MECANICA DEL COMBATE
    //valores de ataques
    public double PD(){//PoderDeDisparo
        return ((double)(Destreza*Fuerza*Nivel));
    }
    public double ED(){//EfectividadDeDisparo
        Random numRan= new Random();
        return (numRan.Next(1,100));
    }
    public double VA(){//ValorDeAtaque
        return PD()*ED()/100;//si ed es 100, va = pd
    }
    //valores de defensa
    public double PDEF(){//PoderDeDefensa
        return Armadura*Velocidad;
    }
    //resultado del enfrentamiento
    public double MDP(){//MaximoDanioProvocable
        return 50000;
    }
    public void DP(double va, double ed){//DanioProvocado
        double dp=Math.Abs((((va*ed)-PDEF())/MDP())*100);//el daño se va acumulado por separado o solo se resta en salud?
        Salud=Salud-dp;//actualizando salud
    }
    public double ActSalud(){
        return Math.Max(0,Math.Round(Salud,2));
    }
    public void Atacar(Personaje enemigoX){
        enemigoX.DP(VA(),ED());
    }
    public void PremioSalud(){
        Salud=Salud+10;//10 puntos mas de salud
    }
    public void PremioFuerza(){
        double az=numRan.Next(5,10);
        Fuerza=Fuerza*(1+az/100);// de 5 a 10 % de aumento en fuerza
    }
    public void CargarPremio(){
        Console.WriteLine();
        Console.WriteLine("ELIJA SU PREMIO");
        Console.WriteLine("Opcion 1 --> +10 puntos de Salud");
        Console.WriteLine("Opcion 2 --> 5% a 10% de Fuerza");
        Console.Write("Ingrese Numero de Opcion: ");
        int a=Convert.ToInt32(Console.ReadLine());
        if(a==1)
        {
            Console.WriteLine("Salud: {0}", Math.Round(Salud,2));
            PremioSalud();
            Console.WriteLine("Salud Actual: {0}", Math.Round(Salud,2));
        }else{
            Console.WriteLine("Fuerza: {0}", Math.Round(Fuerza,2));
            PremioFuerza();
            Console.WriteLine("Fuerza Actual: {0}", Math.Round(Fuerza,2));
        }
        Console.WriteLine();
    }
}