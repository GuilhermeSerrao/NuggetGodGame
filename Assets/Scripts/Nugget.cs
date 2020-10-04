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

    private NavMeshAgent navAgent;

    public string villageName;

    public Race race;

    public Village village = new Village();

    public bool isSearching = true;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        gameObject.name = race + " Nugget";
        village.Name = "";
        StartCoroutine("SpawnVillageDelay");
    }

    private void SpawnNewVillage()
    {
        isSearching = false;
        
        village = FindObjectOfType<VillageManager>().CreateVillageCenter(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), GetComponent<Nugget>(), race.ToString() + " ");
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

    public void MoveNugget(Vector3 targetPosition)
    {
        navAgent.SetDestination(targetPosition);
    }
    
}
