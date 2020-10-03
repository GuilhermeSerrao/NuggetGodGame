using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VillageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject villageCenter;

    private List<Village> villages = new List<Village>();

    public Village CreateVillageCenter(Vector3 spawnPoint, Nugget nugget, string villageName)
    {       

        var newVillage = new Village();
        newVillage.Name = villageName + " village " + villages.Count;
        newVillage.NuggetNumber = 1;
        newVillage.Id = villages.Count;
        newVillage.Members = new List<Nugget>();
        newVillage.Members.Add(nugget);
        newVillage.Faction = nugget.race;

        var newCenter = Instantiate(villageCenter, spawnPoint, Quaternion.identity);
        newCenter.GetComponent<VillageCenter>().thisVillage = newVillage;
        newCenter.GetComponent<VillageCenter>().name = newVillage.Name;



        villages.Add(newVillage);

        return newVillage;
    }
}
