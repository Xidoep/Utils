using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GPUOptimizer : MonoBehaviour
{
    [System.Serializable] public class OptimizedMesh
    {
        public OptimizedMesh(Mesh mesh, Material material) { this.mesh = mesh; this.material = material; }
        public Mesh mesh;
        public Material material;
    }
    [System.Serializable] public class Grafics
    {
        public OptimizedMesh optimizedMesh;
        [SerializeField] List<GPUElement> elements;
        [SerializeField] List<Matrix4x4> matrixes;
        public CombineInstance[] CombineInstance;
        public bool full = false;

        Matrix4x4[] matrix4X4s;
        public Matrix4x4[] Matrix4X4s
        {
            get
            {
                if(matrix4X4s == null) matrix4X4s = new Matrix4x4[elements.Count];
                for (int i = 0; i < matrix4X4s.Length; i++)
                {
                    matrix4X4s[i] = Matrix4x4.TRS(elements[i].transform.position, elements[i].transform.rotation, elements[i].transform.localScale);
                }
                return matrix4X4s;
            }
            set => matrix4X4s = value;
        }

        public void AddTransform(GPUElement element)
        {
            if (elements == null) elements = new List<GPUElement>();
            elements.Add(element);
            if (elements.Count >= 1000) full = true;

            if (matrixes == null) matrixes = new List<Matrix4x4>();
            matrixes.Add(Matrix4x4.TRS(element.transform.position, element.transform.rotation, element.transform.localScale));
        }

        public List<GPUElement> Elements => elements;
        //public Matrix4x4[] Matrix4X4s => matrixes.ToArray();

    }

    public Grafics[] grafics;



    private void Start() => Iniciar();

    //[ContextMenu("Iniciar")]
    public void Iniciar()
    {
        BuscarMeshes();
        StartCoroutine(BorrarReals());
        StartCoroutine(CombineMeshes());
    }

    IEnumerator BorrarReals()
    {
        yield return new WaitForSeconds(1);
        for (int g = 0; g < grafics.Length; g++)
        {
            for (int e = 0; e < grafics[g].Elements.Count; e++)
            {
                Destroy(grafics[g].Elements[e].MeshFilter);
                Destroy(grafics[g].Elements[e].MeshRenderer);
            }
        }


    }

    IEnumerator CombineMeshes()
    {
        yield return new WaitForSeconds(2);
        foreach (var item in grafics)
        {
            CombineInstance[] allMeshes = new CombineInstance[item.Elements.Count];
        }
    }

    private void Update() => Actualitzar();

    public void Actualitzar()
    {
        for (int i = 0; i < grafics.Length; i++)
        {
            Graphics.DrawMeshInstanced(grafics[i].optimizedMesh.mesh, 0, grafics[i].optimizedMesh.material, grafics[i].Matrix4X4s);
        }
    }

    /*[ContextMenu("BorrarReals")]
    void BorrarReals()
    {
        for (int g = 0; g < grafics.Length; g++)
        {
            for (int e = 0; e < grafics[g].Elements.Count; e++)
            {
                Destroy(grafics[g].Elements[e].MeshFilter);
                Destroy(grafics[g].Elements[e].MeshRenderer);
            }
        }
    }*/

    bool coincidit;
    int m;
    public MeshFilter[] BuscarMeshes()
    {
        grafics = new Grafics[0];
        List<MeshFilter> trobats = new List<MeshFilter>();

        GPUElement[] gpuElements = GetComponentsInChildren<GPUElement>();

        for (int mf = 0; mf < gpuElements.Length; mf++)
        {
            coincidit = false;

            if(grafics == null || grafics.Length == 0)
            {
                NouGrafics(gpuElements[mf]);
            }
            else
            {
                m = 0;
                coincidit = false;
                while (m < grafics.Length && coincidit == false)
                {
                    if (grafics[m].optimizedMesh.mesh.Equals(gpuElements[mf].Mesh) &&
                        grafics[m].optimizedMesh.material.Equals(gpuElements[mf].Material) &&
                        !grafics[m].full) 
                    {
                        AddGrafics(ref grafics[m], gpuElements[mf]);
                        coincidit = true;
                    } 
                    m++;
                }
                if (coincidit == false)
                {
                    NouGrafics(gpuElements[mf]);
                }
            }

        }

        return trobats.ToArray();
    }

    void NouGrafics(GPUElement gpuElement)
    {
        Grafics grafic = new Grafics()
        {
            optimizedMesh = new OptimizedMesh(gpuElement.Mesh, gpuElement.Material)
        };
        grafic.AddTransform(gpuElement);

        if (this.grafics == null) this.grafics = new Grafics[0];
        List<Grafics> grafics = new List<Grafics>(this.grafics);
        grafics.Add(grafic);
        this.grafics = grafics.ToArray();
    }
    void AddGrafics(ref Grafics grafic, GPUElement gpuElement)
    {
        grafic.AddTransform(gpuElement);
    }


    [ContextMenu("Clean all GPUelements")]
    void CleanAllCPUelements()
    {
        GPUElement[] elements = FindObjectsOfType<GPUElement>();
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].Clean();
        }
    }

}
