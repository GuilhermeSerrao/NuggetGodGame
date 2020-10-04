﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCenter : MonoBehaviour
{
    [SerializeField]
    private List<Village> allyVillages = new List<Village>(), enemyVillages = new List<Village>();

    [SerializeField]
    private Vector3 areaSize;

    [SerializeField]
    private float cooldownBuild;

    [SerializeField]
    private int wood, stone;

    private Vector3 randomLocationInTerritory;

    public List<Nugget> nuggets = new List<Nugget>();

    private Territory territory;

    public int population;

    public List<Building> buildings = new List<Building>();

    public Village thisVillage;

    public string name;

    private float startCooldownBuild;

    private int builtHouses = 0;

    private void Start()
    {
        startCooldownBuild = cooldownBuild;
        territory = transform.GetChild(0).GetComponent<Territory>();
        territory.SetSize(areaSize);
        randomLocationInTerritory = transform.position + new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.z / 2, areaSize.z / 2));
    }

    private void Update()
    {
        if (cooldownBuild > 0)
        {
            cooldownBuild -= Time.deltaTime;
        }
        else if (cooldownBuild <= 0)
        {
            cooldownBuild = startCooldownBuild;

            if (builtHouses == 0)
            {
                foreach (var item in buildings)
                {
                    if (item.Name == "House" && wood >= item.WoodCost)
                    {
                        foreach (var nugget in nuggets)
                        {
                            if (nugget.isInVillage)
                            {
                                nugget.MoveNugget(randomLocationInTerritory);                                
                            }
                        }

                        wood -= item.WoodCost;
                        Instantiate(item.Prop, randomLocationInTerritory, Quaternion.identity);

                        builtHouses++;

                        break;
                    }
                }
            }
            else if (population / builtHouses > 3)
            {
                foreach (var item in buildings)
                {
                    if (item.Name == "House" && wood >= item.WoodCost)
                    {
                        wood -= item.WoodCost;
                        Instantiate(item.Prop, transform.position + new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.z / 2, areaSize.z / 2)), Quaternion.identity);

                        builtHouses++;

                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.GetComponent<Nugget>())
        {            
            var newNugget = other.GetComponent<Nugget>();

            if (newNugget.village.Name == "" && newNugget.race == thisVillage.Faction)
            {
                newNugget.isSearching = false;
                thisVillage.Members.Add(newNugget);
                newNugget.village = thisVillage;
                newNugget.villageName = thisVillage.Name;

                newNugget.MoveNugget(transform.position + new Vector3(Random.Range( -areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.z / 2, areaSize.z / 2)));

                population++;
                nuggets.Add(newNugget);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }

    
}


