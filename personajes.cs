using System;
public class Personaje
{
    //DATOS
    public int Tipo;
    public enum Tipos{SoldadoModerno,GerreroMediaval,SoldadoInterestelar,Alien};
    public int Nombre;
    public enum Nombres{Jorge, Juan, Mario, Luciana, Marcos};
    public int Apodo;
    public enum Apodos{Destructor,Malote,Viejo,Fantasma,Nerd};
    public DateTime FecNac;
    public int Edad;//0 a 300
    public float Salud;//100
    //CARACTERISTICAS
    public int Velocidad;//1 a 10
    public int Destreza;//1 a 5
    public int Fuerza;//1 a 10
    public int Nivel;//1 a 10
    public int Armadura;//1 a 10
    public Personaje(int tipoo, int nomm, int apodoo, DateTime nace){
        Tipo=tipoo;
        Nombre=nomm;
        Apodo=apodoo;
        FecNac=nace;
        Salud=100;
    }
    public int Edad2()
    {
        DateTime actual = DateTime.Now;
        var edad = actual - FecNac;
        int anios = (int)(edad.TotalDays / 365.25);
        return anios;
    }
    public Personaje(){
        Random numRan= new Random();
        Velocidad=numRan.Next(1,10);
        Destreza=numRan.Next(1,5);
        Fuerza=numRan.Next(1,10);
        Nivel=numRan.Next(1,10);
        Armadura=numRan.Next(1,10);
    }
    public void MuestraDatos(){
        Console.WriteLine("Datos del Personaje");
        Console.WriteLine("Tipo: {0}", Enum.GetName(typeof(Tipos), Tipo));
        Console.WriteLine("Nombre: {0}", Enum.GetName(typeof(Nombres), Nombre));
        Console.WriteLine("Apodo: {0}", Enum.GetName(typeof(Apodos), Apodo));
        Console.WriteLine("Fecha de nacimiento: {0:d}", FecNac);
        Console.WriteLine("Edad: "+Edad2()+" años");
        Console.WriteLine("Salud: "+Salud);
    }
    public void MuestraCaracter(){
        Console.WriteLine("Caracteristicas del Personaje");
        Console.WriteLine("Velocidad: {0}", Velocidad.ToString());
        Console.WriteLine("Destreza: {0}", Destreza);
        Console.WriteLine("Fuerza: {0}", Fuerza);
        Console.WriteLine("Nivel: {0}", Nivel);
        Console.WriteLine("Armadura: {0}", Armadura);
    }
}