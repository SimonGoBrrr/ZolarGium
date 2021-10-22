using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
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
                //Enable linerenderer.
                Gravity[] gravityObjects = Object.FindObjectsOfType<Gravity>();
                foreach(Gravity gravityobject in gravityObjects){
                    LineRenderer objectLineRenderer = gravityobject.gameObject.GetComponent<LineRenderer>();
                    objectLineRenderer.positionCount = maxiterations;
                    objectLineRenderer.enabled = true;
                    objectLineRenderer.startWidth = lineStartWidth;
                    objectLineRenderer.endWidth = lineEndWidth;
                }
                //Generate simulatedbodiers.
                List<SimulatedBody> bodies = new List<SimulatedBody>();
                foreach(Gravity gravityobject in gravityObjects){
                    Rigidbody gravityobjectrb = gravityobject.gameObject.GetComponent<Rigidbody>();
                    LineRenderer gravityobjectlr = gravityobject.gameObject.GetComponent<LineRenderer>();
                    Vector3 pos = new Vector3(gravityobject.gameObject.GetComponent<planet>().size/2, gravityobject.gameObject.GetComponent<planet>().size/2, gravityobject.gameObject.GetComponent<planet>().size/2)+ gravityobject.gameObject.transform.position;
                    SimulatedBody s = new SimulatedBody(gravityobjectrb, gravityobject.initalVelocity , gravityobject.mass, gravityobjectlr, pos);
                    bodies.Add(s);
                }
                //Go through each iterations
                for(int i = 0; i < maxiterations; i++){
                    foreach(SimulatedBody simulatedObject in bodies){
                        for(int x = 0; x < bodies.Count; x++){
                            if(bodies[x] == simulatedObject){
                                continue;
                            }
                            //Update velocity
                            simulatedObject.velocity+=ReverseAttraction(simulatedObject, bodies[x]) * timeStep;
                        }
                        //Update positions
                        simulatedObject.position+=simulatedObject.velocity * timeStep;
                        simulatedObject.lr.SetPosition(i, simulatedObject.position);
                    }
                }
                //Colour change of linerenderer.
                foreach(SimulatedBody simulatedObject in bodies){
                    Color colour1 = Color.blue;
                    Color colour2 = new Color(1,1,1,0.5f);
                    simulatedObject.lr.material = new Material(Shader.Find("Sprites/Default"));
                    simulatedObject.lr.startColor = colour1;
                    simulatedObject.lr.endColor = colour2;
                }
            }
            //If is not in editor mode, disable all linerenderer. Ineffective, does every frame when only need once in start.
            else{
                Gravity[] gravityObjects = FindObjectsOfType<Gravity>();
                foreach(Gravity gravityobject in gravityObjects){
                    LineRenderer lr = gravityobject.gameObject.GetComponent<LineRenderer>();
                    lr.enabled = false;
                    lr.positionCount = 0;
                }}
        }
        //If draw is off then disable all linerenderers.
        else{
                Gravity[] gravityObjects = FindObjectsOfType<Gravity>();
                foreach(Gravity gravityobject in gravityObjects){
                    LineRenderer lr = gravityobject.gameObject.GetComponent<LineRenderer>();
                    lr.enabled = false;
                    lr.positionCount = 0;
                }}
    }
    //Reversesattraction from newtons gravity equation.
	Vector3 ReverseAttraction(SimulatedBody currentobject, SimulatedBody toattract)
    {
        Vector3 direction = toattract.position - currentobject.position;
        float distance = direction.sqrMagnitude;
        float forceMultiplied = (toattract.mass * currentobject.mass) / distance;
        Vector3 force = direction.normalized * forceMultiplied;
        return force;
    }
    //Simulated object of celestial object, do not want to update actual positions of planets.
    class SimulatedBody{
        public Vector3 velocity;
        public Vector3 position;
        public float mass;
        public Rigidbody rigidbody;
        public LineRenderer lr;
        public SimulatedBody(Rigidbody rb, Vector3 startVel, float weight, LineRenderer linerenderer, Vector3 pos) {
            velocity = startVel;
            position = pos;
            mass = weight;
            rigidbody = rb;
            lr = linerenderer;
        }
    }
}
