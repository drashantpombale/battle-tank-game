using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

   
    private void LateUpdate()
    {
        if (cam == null) {
            cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        transform.LookAt(transform.position + cam.forward);
    }
}
