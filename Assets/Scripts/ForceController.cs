using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour
{
    public Camera cam;
    public bool pullForce;
    public float pullStrength, pushStrength;
    public float pullRange = 25, pullRaduis = 4, pushRadius = 3;


    public Collider[] targetObjects;
    public string ignoredTag = "IgnoreRaycast";
    


    public Transform holdPosition;
    public Transform pushPosition;

    public Animator animator;

    public void Update()
    {

        if(Input.GetButtonDown("Fire2"))
        {
            pullForce = true;
            GetPullObjects();
        }
        if(Input.GetButton("Fire2"))
        {
            if(pullForce)
            PullForce();
        }
        if(Input.GetButtonUp("Fire2"))
        {
            pullForce = false;
        }
        if(Input.GetButtonDown("Fire1"))
        {
            if(pullForce)
            {
                ThrowForce();
                pullForce = false;
            }
            else
            {
                PushForce();
            }

        }

    }
    public void PushForce()
    {
        GetPushObjects();
        ThrowForce();
    }


    public void GetPullObjects()
    {
        targetObjects = null;
        RaycastHit hit;
        

            if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, pullRange))
                        {
            targetObjects = Physics.OverlapSphere(hit.point, pullRaduis);
                       }
            }
            
   

    

    public void GetPushObjects()
    {
        targetObjects = null;
        targetObjects = Physics.OverlapSphere(pushPosition.position, pushRadius);
    }

    public void PullForce()
    {
        if(targetObjects != null && targetObjects.Length > 0)
        {
            foreach(Collider col in targetObjects)
            {
                if(col.GetComponent<Rigidbody>())
                col.GetComponent<Rigidbody>().velocity = (holdPosition.position - col.transform.position) * pullStrength *Time.deltaTime;
            }
        }
    }

     public void ThrowForce()
    {
        if(targetObjects != null && targetObjects.Length > 0)
        {
            foreach(Collider col in targetObjects)
            {
                if(col.GetComponent<Rigidbody>())
                col.GetComponent<Rigidbody>().AddForce(col.transform.TransformDirection(Vector3.forward) * pushStrength, ForceMode.Impulse);
            }
        }
    }

   
}
