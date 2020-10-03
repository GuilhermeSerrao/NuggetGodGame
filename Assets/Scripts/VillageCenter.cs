using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageCenter : MonoBehaviour
{
   

    public Village thisVillage;

    public string name;

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
            }
        }
    }

}


