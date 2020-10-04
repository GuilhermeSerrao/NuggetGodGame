using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateNugget : MonoBehaviour
{


    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    Nugget nuggetToSpawn;

    [SerializeField]
    private Nugget[] nuggetsToSelect;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nuggetToSpawn = nuggetsToSelect[0];
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nuggetToSpawn = nuggetsToSelect[1];
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nuggetToSpawn = nuggetsToSelect[2];
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nuggetToSpawn = nuggetsToSelect[3];
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer, QueryTriggerInteraction.Ignore ))
            {
                Debug.Log(hit.collider.gameObject.layer);
                Instantiate(nuggetToSpawn, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);                
            }
        }
            
    }
}
