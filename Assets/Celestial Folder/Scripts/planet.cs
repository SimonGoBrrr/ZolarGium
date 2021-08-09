using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : generation
{
    [SerializeField]
    private Vector3 size = new Vector3(10,10,10);
    [SerializeField]
    private float noiseScale = 0.1f;
    [SerializeField]
    private float surfaceLevel = 0.5f;
    [SerializeField]
    private int seed = 100;


    void Start(){
        var objectMesh = GetComponent<MeshFilter>();
        var meshCollider = GetComponent<MeshCollider>();
        Mesh marchMesh = createMesh(size, surfaceLevel, noiseScale, seed);
        objectMesh.mesh = marchMesh;
        meshCollider.sharedMesh = marchMesh;
    }
}