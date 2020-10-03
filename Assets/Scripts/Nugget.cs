using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nugget : MonoBehaviour
{


    [SerializeField]
    private LayerMask nuggetsLayer;

    [SerializeField]
    private float findOtherNuggetsRange, searchCooldown;

    [SerializeField]
    private bool lookingForResources;

    [SerializeField]
    private Race race;

    private bool isLookingForOthers = true;

    private float startSearchCooldown;

    private List<Nugget> friendlyNearbyNuggets = new List<Nugget>();

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        startSearchCooldown = searchCooldown;
        agent = GetComponent<NavMeshAgent>();
        SearchNearbyNuggets();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (friendlyNearbyNuggets.Count >= 3 )
        {

        }

        if (searchCooldown > 0 && isLookingForOthers)
        {
            searchCooldown -= Time.deltaTime;
        }
        else if (searchCooldown <= 0)
        {
            searchCooldown = startSearchCooldown;
            SearchNearbyNuggets();
        }

        if (friendlyNearbyNuggets.Count >= 3)
        {
            CreateVillage();
            
        }
    }

    private void SearchNearbyNuggets()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, findOtherNuggetsRange, transform.forward, Mathf.Infinity, nuggetsLayer, QueryTriggerInteraction.UseGlobal);

        List<GameObject> nearbyNuggets = new List<GameObject>();

        foreach (var hit in hits)
        {                
            if (hit.collider.transform != transform)
            {
                nearbyNuggets.Add(hit.collider.gameObject);
            }
        }
 
        Debug.Log(nearbyNuggets.Count + " Nuggets nearby");

        foreach (var item in nearbyNuggets)
        {
            var nugget = item.GetComponent<Nugget>();

            if (nugget.race == race)
            {
                if (!friendlyNearbyNuggets.Contains(nugget))
                {
                    friendlyNearbyNuggets.Add(item.GetComponent<Nugget>());
                }
                
            }
        }

    }

    private void CreateVillage()
    {
        isLookingForOthers = false;
        FindObjectOfType<VillageSpawner>().CreateVillage(friendlyNearbyNuggets, race, transform.position);
        friendlyNearbyNuggets.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findOtherNuggetsRange);
    }
}
