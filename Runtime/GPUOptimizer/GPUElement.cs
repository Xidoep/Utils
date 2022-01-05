using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUElement : MonoBehaviour
{
    MeshFilter meshFilter;
    Mesh mesh;
    MeshRenderer meshRenderer;
    Material material;

    public MeshFilter MeshFilter
    {
        get
        {
            if (meshFilter == null) meshFilter = GetComponent<MeshFilter>();
            return meshFilter;
        }
    }
    public Mesh Mesh
    {
        get
        {
            if (mesh == null) mesh = MeshFilter.sharedMesh;
            return mesh;
        }
    }
    public MeshRenderer MeshRenderer
    {
        get
        {
            if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
            return meshRenderer;
        }
    }
    public Material Material
    {
        get
        {
            if (material == null) material = MeshRenderer.sharedMaterial;
            return material;
        }
    }

    public void Clean()
    {
        mesh = null;
        material = null;
        meshFilter = null;
    }
}
