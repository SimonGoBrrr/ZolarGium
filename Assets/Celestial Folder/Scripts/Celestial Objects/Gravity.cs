using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravity : MonoBehaviour
{
    public static List<Gravity> gravityObjects;
    public float mass = 100f;
    public Rigidbody rb;
    public Vector3 initalVelocity = new Vector3(0,0,0);
    public float RotationalX, RotationalY = 0;

    void Start(){
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(initalVelocity);
        rb.AddTorque(transform.up * RotationalY);
        rb.AddTorque(transform.right * RotationalX);
        rb.mass = mass;
    }
    public void FixedUpdate(){
        foreach(Gravity attractor in gravityObjects){
            if(attractor != this){
                Attract(attractor);
            }
        }
    }
    public void Attract(Gravity objectToAttact){
        Rigidbody rbtoAttract = objectToAttact.rb;
        Vector3 dir = rb.position - rbtoAttract.position;
        float distance = dir.sqrMagnitude;
        float forceMagnitude = (rb.mass *rbtoAttract.mass)  / distance;
        Vector3 force = dir.normalized* forceMagnitude;
        rbtoAttract.AddForce(force);
    }
    
    void OnEnable(){
        if(gravityObjects == null) {
            gravityObjects = new List<Gravity>();
        }
        gravityObjects.Add(this);
    }
    void OnDisable(){
        gravityObjects.Remove(this);
    }
}

