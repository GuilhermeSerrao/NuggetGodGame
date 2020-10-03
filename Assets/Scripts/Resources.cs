using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Village
{
    public string Name { get; set; }
    public List<Nugget> Habitants { get; set; }
    public int Power { get; set; }
    public Race RaceFaction { get; set; }

    public Village(string Name, List<Nugget> Habitants, int Power, Race RaceFaction) : this()
    {
        this.Name = Name;
        this.Habitants = Habitants;
        this.Power = Power;
        this.RaceFaction = RaceFaction;

    }
}
public enum Race
{
    Human,
    Elf,
    Orc,
    Dwarf
}

public class Resources : MonoBehaviour
{

}
