using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageSpawner : MonoBehaviour
{  

    [SerializeField]
    private GameObject villageCenter;

    public void CreateVillage(List<Nugget> nuggets,Race faction, Vector3 spawnPosition)
    {
        var newVillage = new Village("Vila", nuggets, nuggets.Count, faction);
        
        Instantiate(villageCenter, spawnPosition, Quaternion.identity);
        
    }

}


