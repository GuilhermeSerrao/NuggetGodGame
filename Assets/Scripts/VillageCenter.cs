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

    [SerializeField]
    private int wood, stone;

    public List<Building> buildings = new List<Building>();

    public Village thisVillage;

    public string name;

    private float startCooldownBuild;

    private void Start()
    {
        startCooldownBuild = cooldownBuild;
        print(buildings.Count);
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
            foreach (var item in buildings)
            {
                if (item.Name == "House" && wood >= item.WoodCost)
                {
                    wood -= item.WoodCost;
                    Instantiate(item.Prop, transform.position + new Vector3(Random.Range(-areaSize.x / 2, areaSize.x / 2), 0, Random.Range(-areaSize.z / 2, areaSize.z / 2)), Quaternion.identity);
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
            }
        }
    }

    private void SendBuilder()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }

    
}


