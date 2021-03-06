using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : Gravity
{
    public float checkSphereRadius = 10f;

    // Update is called once per frame

    override public void FixedUpdate()
    {
        rb.mass = mass;
        foreach(Gravity attractor in gravityObjects){
            if(attractor != this){
                Attract(attractor);
            }
        }
        Collider[] checkSphere = Physics.OverlapSphere(rb.position, checkSphereRadius);
        if(checkSphere != null){
            foreach(Collider i in checkSphere){
                float distance = (rb.position - i.transform.position).magnitude;
                i.transform.localScale *= distance/checkSphereRadius;
                //If i transform localScale is lower than transform blackhole * 0,5.
            }
        }
    }
}

