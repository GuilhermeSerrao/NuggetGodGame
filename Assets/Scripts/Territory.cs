using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{

    public void SetSize(Vector3 newSize)
    {
        GetComponent<BoxCollider>().size = newSize;
    }

}
