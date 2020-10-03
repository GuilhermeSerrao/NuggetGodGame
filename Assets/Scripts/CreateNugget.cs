using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateNugget : MonoBehaviour
{


    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Nugget nuggetToSpawn;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, groundLayer))
            {
                Instantiate(nuggetToSpawn, new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z), Quaternion.identity);                
            }
        }
            
    }
}
