using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : generation
{
    public int chunkSize = 10;
    public int numChunks = 3;

    [SerializeField]
    private float noiseScale = 0.975f;

    [SerializeField]
    private float noiseHeightMultiplier = 20f;

    [SerializeField]
    private float surfaceLevel = 0.5f;

    [SerializeField]
    private int seed = 100;

    [SerializeField]
    private int numLayers = 3;
    
    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private Material mat;
    



    void Start(){
        float [,,] noiseMap = GenerateNoiseMap(chunkSize*numChunks, numLayers, noiseScale, noiseHeightMultiplier, seed);
        //Possibility to manipulate noiseMap here, to create craters, etcetera for moons.
        generateChunksWithMesh(chunkSize, numChunks, this.gameObject.transform, surfaceLevel, noiseScale, noiseHeightMultiplier, seed, numLayers, noiseMap, gradient, mat);
        generateVertexColoursForPlanet(this.gameObject.transform, new Vector3(0,0,0), chunkSize*numChunks/2.1f, gradient);
    }
}