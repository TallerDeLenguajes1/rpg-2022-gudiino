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
    public float Salud;//100
    //CARACTERISTICAS
    public int Velocidad;//1 a 10
    public int Destreza;//1 a 5
    public int Fuerza;//1 a 10
    public int Nivel;//1 a 10
    public int Armadura;//1 a 10
    public Personaje(int tipoo, string nomm, int apodoo, DateTime nace){
        Tipo=tipoo;
        Nombre=nomm;
        Apodo=apodoo;
        FecNac=nace;
    }
    public int Edad2()
    {
        DateTime actual = DateTime.Now;
        var edad = actual - FecNac;
        int anios = (int)(edad.TotalDays / 365.25);
        return anios;
    }
    public Personajes(){
        Random numRan= new Random();
        Velocidad=numRan.Next(1,10);
        Destreza=numRan.Next(1,5);
        Fuerza=numRan.Next(1,10);
        Nivel=numRan.Next(1,10);
        Armadura=numRan.Next(1,10);
    }
}