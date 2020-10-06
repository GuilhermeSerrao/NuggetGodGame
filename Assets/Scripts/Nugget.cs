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

    private Building toBuild;

    private NavMeshAgent navAgent;

    public string villageName;

    public Race race;

    public Village village = new Village();

    public VillageCenter villageCenter;

    public bool isSearching = true, isInVillage = true, isBusy = false, isMoving = false, isBuilding = false, isHarvesting = false;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        gameObject.name = race + " Nugget";
        village.Name = "";
        StartCoroutine("SpawnVillageDelay");
    }

    private void FixedUpdate()
    {
        if (isBuilding)
        {
            if (navAgent.remainingDistance > 0.1f)
            {
                print("Moving");
            }
            else
            {
                print(toBuild.time);

                isBuilding = false;

                StartCoroutine(BuildTime(toBuild.time));              
               
            }
        }
        
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

    public void GoBuild(Building building, Vector3 targetPosition)
    {
        isBusy = true;
        isBuilding = true;
        navAgent.SetDestination(targetPosition);
        toBuild = building;       
        
    }

    private IEnumerator BuildTime(float time)
    {
        
        isBusy = false;
        isBuilding = false;
        yield return new WaitForSeconds(time);

        villageCenter.wood -= toBuild.WoodCost;
        Instantiate(toBuild.Prop, new Vector3(transform.position.x + 1, 0, transform.position.z + 1), Quaternion.identity);        
        FindObjectOfType<NavMeshBaker>().BakeNavMesh();
        villageCenter.builtHouses++;        
        navAgent.SetDestination(villageCenter.RandomLocationInTerritory());

        print("Built");

    }

}