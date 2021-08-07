using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Trajectoryprediction : MonoBehaviour
{
    public int maxiterations = 100;
    public float lineStartWidth = 0.5f;
    public float lineEndWidth = 0.3f;
    public bool draw = false;
    public float timeStep = 0.01f;
    void Update()
    {
        if(draw == true) {
            if(!Application.isPlaying){
                Gravity[] gravityObjects = Object.FindObjectsOfType<Gravity>();
                foreach(Gravity gravityobject in gravityObjects){
                    LineRenderer objectLineRenderer = gravityobject.gameObject.GetComponent<LineRenderer>();
                    objectLineRenderer.positionCount = maxiterations;
                    objectLineRenderer.enabled = true;
                    objectLineRenderer.startWidth = lineStartWidth;
                    objectLineRenderer.endWidth = lineEndWidth;
                }
                List<SimulatedBody> bodies = new List<SimulatedBody>();
                foreach(Gravity gravityobject in gravityObjects){
                    Rigidbody gravityobjectrb = gravityobject.gameObject.GetComponent<Rigidbody>();
                    LineRenderer gravityobjectlr = gravityobject.gameObject.GetComponent<LineRenderer>();
                    //Unsure what to put in with initial velocity... Problem is not gravityobject mass for certain.
                    SimulatedBody s = new SimulatedBody(gravityobjectrb, gravityobject.initalVelocity * timeStep, gravityobject.mass, gravityobjectlr);
                    bodies.Add(s);
                }
                for(int i = 0; i < maxiterations; i++){
                    foreach(SimulatedBody simulatedObject in bodies){
                        for(int x = 0; x < bodies.Count; x++){
                            if(bodies[x] == simulatedObject){
                                continue;
                            }
                            simulatedObject.velocity+=ReverseAttraction(simulatedObject, bodies[x]);
                        }
                        //Why extra timestep underneath. i don´t understand why?
                        simulatedObject.position+=simulatedObject.velocity * timeStep;
                        simulatedObject.lr.SetPosition(i, simulatedObject.position);
                    }
                }
            }
        }
        else{
                Gravity[] gravityObjects = FindObjectsOfType<Gravity>();
                foreach(Gravity gravityobject in gravityObjects){
                    LineRenderer lr = gravityobject.gameObject.GetComponent<LineRenderer>();
                    lr.enabled = false;
                    lr.positionCount = 0;
                }}
    }
    Vector3 ReverseAttraction(SimulatedBody currentobject, SimulatedBody toattract)
    {
        Vector3 direction = toattract.position - currentobject.position;
        float distance = direction.sqrMagnitude;
        float forceMultiplied = (toattract.mass * currentobject.mass) / distance;
        Vector3 force = direction.normalized * forceMultiplied;
        return force;
    }
    class SimulatedBody{
    public Vector3 velocity;
    public Vector3 position;
    public float mass;
    public Rigidbody rigidbody;
    public LineRenderer lr;
    public SimulatedBody(Rigidbody rb, Vector3 startVel, float weight, LineRenderer linerenderer) {
        velocity = startVel;
        position = rb.position;
        mass = weight;
        rigidbody = rb;
        lr = linerenderer;
    }
}
}
