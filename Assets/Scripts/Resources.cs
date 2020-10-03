using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NuggetGroup
{
    public NuggetGroup(int nuggetNumber, List<Nugget> nuggets, int id) : this()
    {
        NuggetNumber = nuggetNumber;
        Nuggets = nuggets;
        Id = id;
    }

    public int NuggetNumber { get; set; }
    public List<Nugget> Nuggets { get; set; }

    public int Id { get; set; }

}

public struct Village
{
    public string Name;
    public int NuggetNumber;
    public int Id;
    public List<Nugget> Members;
    public Race Faction;
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
