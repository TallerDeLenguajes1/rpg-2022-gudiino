using System;
public class Personaje
{
    //DATOS
    public int Tipo;
    public enum Tipos{SoldadoModerno,GerreroMediaval,SoldadoInterestelar,Alien};
    public string Nombre;
    public int Apodo;
    public enum Apodos{Destructor,Malote,Viejo,Fantasma,Nerd};
    public DateTime FecNac;
    public int Edad;//0 a 300
    public double Salud;//100
    //CARACTERISTICAS
    public double Velocidad;//1 a 10
    public double Destreza;//1 a 5
    public double Fuerza;//1 a 10
    public double Nivel;//1 a 10
    public double Armadura;//1 a 10
    Random numRan= new Random();
    DateTime actual = DateTime.Now;
    public Personaje(string nomm){
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
    }
    //  public Personaje(){
    //     Velocidad=numRan.Next(1,10);
    //     Destreza=numRan.Next(1,5);
    //     Fuerza=numRan.Next(1,10);
    //     Nivel=numRan.Next(1,10);
    //     Armadura=numRan.Next(1,10);
    // }
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
        Console.WriteLine("Salud: "+Salud);
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
        return PD()*ED()/100;
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
        Salud=Salud-(((va*ed)-PDEF())/MDP())*100;
    }
    public double ActSalud(){
        return Math.Max(0,Math.Round(Salud,2));
    }
    public void Atacar(Personaje enemigoX){
        enemigoX.DP(VA(),ED());
    }
}