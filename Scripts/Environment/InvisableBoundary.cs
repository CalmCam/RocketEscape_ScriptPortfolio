using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisableBoundary : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
