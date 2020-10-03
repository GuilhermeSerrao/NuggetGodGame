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
    private bool lookingForResources;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SearchNearbyNuggets();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookingForResources)
        {
            var sources = GameObject.FindGameObjectsWithTag("WoodSource");
            var closestSource = Vector3.zero;
            float maxDistance = 0;

            foreach (var item in sources)
            {
                if (maxDistance == 0)
                {
                    maxDistance = Vector3.Distance(transform.position, item.transform.position);
                    closestSource = item.transform.position;
                }
                else if (Vector3.Distance(transform.position, item.transform.position) < maxDistance)
                {
                    closestSource = item.transform.position;
                    maxDistance = Vector3.Distance(transform.position, item.transform.position);
                }
            }

            if (maxDistance != 0)
            {
                agent.SetDestination(closestSource);
                //lookingForResources = false;
            }
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

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, findOtherNuggetsRange);
    }
}
