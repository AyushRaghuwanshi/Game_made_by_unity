using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    Collider collider;
    float timee;
    void Start()
    {
        timee = Time.time;
        collider = GetComponent<MeshCollider>();
    }

    void Update()
    {
        if((Time.time - timee) > 4)
        {
            timee = Time.time;
            if(transform.rotation.z == 0)
            {
                transform.Rotate(0, 0, 90);
                collider.enabled = false;
            }
            else
            {
                transform.Rotate(0, 0, -90);
                collider.enabled = true;
            }
        }  
    }
}
