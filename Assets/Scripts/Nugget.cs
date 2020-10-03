using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nugget : MonoBehaviour
{
    [SerializeField]
    private LayerMask nuggetsLayer;

    [SerializeField]
    private float findOtherNuggetsRange;

    [SerializeField]
    private float cooldownSearchVillage;

    public string villageName;

    public Race race;

    public Village village = new Village();

    public bool isSearching = true;

    private void Start()
    {
        village.Name = "";
        StartCoroutine("SpawnVillageDelay");
    }

    private void SpawnNewVillage()
    {
        isSearching = false;
        village = FindObjectOfType<VillageManager>().CreateVillageCenter(transform.position, GetComponent<Nugget>(), race.ToString() + " ");
    }

    private void SearchNearbyNuggets()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, findOtherNuggetsRange, transform.forward, Mathf.Infinity, nuggetsLayer, QueryTriggerInteraction.UseGlobal);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findOtherNuggetsRange);
    }

    private IEnumerator SpawnVillageDelay()
    {
        yield return new WaitForSeconds(cooldownSearchVillage);
        if (isSearching)
        {
            SpawnNewVillage();
        }
    }
    
}
