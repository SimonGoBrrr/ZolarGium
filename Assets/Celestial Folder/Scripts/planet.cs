using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : generation
{
    [SerializeField]
    private int size = 10;
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


    void Start(){
        var objectMesh = GetComponent<MeshFilter>();
        var meshCollider = GetComponent<MeshCollider>();
        Mesh marchMesh = createMesh(size, surfaceLevel, noiseScale, noiseHeightMultiplier, seed, numLayers);
        objectMesh.mesh = marchMesh;
        meshCollider.sharedMesh = marchMesh;
    }
}