using System;
using System.Collections.Generic;using System.Text.Json.Serialization;

namespace rpg
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Civilization
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? expansion { get; set; }
        public string? army_type { get; set; }
        public List<string>? unique_unit { get; set; }
        public List<string>? unique_tech { get; set; }
        public string? team_bonus { get; set; }
        public List<string>? civilization_bonus { get; set; }
        public List<Civilization>? civilizations { get; set; }

        public void MostrarC(Civilization item)
        {
            Console.WriteLine("ID: "+id);
            Console.WriteLine("Nombre: "+name);
            Console.WriteLine("Expansion: "+expansion);
            Console.WriteLine("Tipo de Arma: "+army_type);
            Console.WriteLine("Unique Unit: ");
            foreach (var item2 in unique_unit!)
            {
                Console.WriteLine(item2);
            }
            Console.WriteLine("Unique Tech: ");
            foreach (var item3 in unique_tech!)
            {
                Console.WriteLine(item3);
            }
            Console.WriteLine("Bonus Equipo: "+team_bonus);
            Console.WriteLine("Civilizacion Bonus");
            foreach (var item3 in unique_tech)
            {
                Console.WriteLine(item3);
            }

        }

    }

    public class RootC
    {
        public List<Civilization>? civilizations { get; set; }
    }
}