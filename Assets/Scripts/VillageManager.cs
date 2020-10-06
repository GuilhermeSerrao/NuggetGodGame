using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VillageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject villageCenter;

    [SerializeField]
    private Build[] buildings;

    private List<Village> villages = new List<Village>();

    private List<Building> buildingsToPass = new List<Building>();  


    public Village CreateVillageCenter(Vector3 spawnPoint, Nugget nugget, string villageName)
    {              
        var newVillage = new Village();
        newVillage.Name = villageName + " Village " + villages.Count;
        newVillage.NuggetNumber = 1;
        newVillage.Id = villages.Count;
        newVillage.Members = new List<Nugget>();
        newVillage.Members.Add(nugget);
        newVillage.Faction = nugget.race;

        foreach (var item in buildings)
        {
            if (item.Faction == newVillage.Faction)
            {
                var newBuilding = new Building();

                newBuilding.Name = item.name;
                newBuilding.Prop = item.gameObject;
                newBuilding.WoodCost = item.WoodCost;
                newBuilding.StoneCost = item.StoneCost;
                newBuilding.time = item.TimeToBuild;

                if (newVillage.Buildings == null)
                {
                    newVillage.Buildings = new List<Building>();
                }      
                
                newVillage.Buildings.Add(newBuilding);

            }         
        }
        
        nugget.villageName = newVillage.Name;

        var newCenter = Instantiate(villageCenter, spawnPoint, Quaternion.identity);

        var newcenterComponent = newCenter.GetComponent<VillageCenter>();
        newcenterComponent.thisVillage = newVillage;
        newcenterComponent.name = newVillage.Name;
        newcenterComponent.buildings = newVillage.Buildings;
        newcenterComponent.population += 1;
        newcenterComponent.nuggets.Add(nugget);

        nugget.villageCenter = newCenter.GetComponent<VillageCenter>();

        villages.Add(newVillage);

        return newVillage;
    }
}