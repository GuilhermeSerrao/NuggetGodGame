using System.Collections;
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

    public int wood, stone;

    private Vector3 randomLocationInTerritory;

    public List<Nugget> nuggets = new List<Nugget>();

    private Territory territory;

    public int population;

    public List<Building> buildings = new List<Building>();

    public Village thisVillage;

    public string name;

    private float startCooldownBuild;

    public int builtHouses = 0;

    public bool alreadyBuilding = false;

    private void Start()
    {
        startCooldownBuild = cooldownBuild;
        territory = transform.GetChild(0).GetComponent<Territory>();
        territory.SetSize(areaSize);

        foreach (var build in buildings)
        {
            if (build.Name == "House")
            {
                foreach (var nugget in nuggets)
                {
                    if (nugget.isInVillage && !nugget.isBusy)
                    {
                        
                        nugget.GoBuild(build, RandomLocationInTerritory());
                        

                        break;
                    }
                }
            }            
        }
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
            if (builtHouses > 0)
            {
                if (population / builtHouses > 3)
                {
                    foreach (var build in buildings)
                    {
                        if (build.Name == "House")
                        {
                            foreach (var nugget in nuggets)
                            {
                                if (nugget.isBuilding && nugget.toBuild.Name == "House")
                                {
                                    alreadyBuilding = true;
                                    break;
                                }
                                
                            }

                            if (!alreadyBuilding)
                            {
                                foreach (var item in nuggets)
                                {
                                    if (item.isInVillage && !item.isBusy)
                                    {
                                        item.GoBuild(build, RandomLocationInTerritory());
                                        break;
                                    }
                                }
                            }
                            break;
                        }
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
                newNugget.villageCenter = GetComponent<VillageCenter>();

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

    public Vector3 RandomLocationInTerritory()
    {
        return transform.position + new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.z / 2, areaSize.z / 2)); ;
    }    
}