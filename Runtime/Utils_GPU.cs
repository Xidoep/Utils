using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_GPU
    {
        const int MAX_GRAPHICS_PER_BUFFER = 1000;

        [System.Serializable]
        public class Grafic
        {
            public Mesh mesh;
            public Material[] materials;
            public List<MeshRenderer> elements;
            public Matrix4x4[] matrix4X4s;
        }

        public static List<Grafic> grafics;

        //INTERN
        static int index;
        static MeshRenderer meshRenderer;
        static bool matched;

        public static void AddGrafics(this GameObject gameObject)
        {
            return;

            if (grafics == null) grafics = new List<Grafic>();
            MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

            for (int mf = 0; mf < meshFilters.Length; mf++)
            {
                index = -1;
                meshRenderer = meshFilters[mf].GetComponent<MeshRenderer>();
                for (int g = 0; g < grafics.Count; g++)
                {
                    if (grafics[g].elements.Count <= MAX_GRAPHICS_PER_BUFFER &&
                        grafics[g].mesh == meshFilters[mf].sharedMesh)
                    {
                        if (grafics[g].materials.Length != meshRenderer.sharedMaterials.Length)
                            continue;

                        matched = true;
                        for (int m = 0; m < meshRenderer.sharedMaterials.Length; m++)
                        {
                            if (grafics[g].materials[m] != meshRenderer.sharedMaterials[m])
                            {
                                matched = false;
                                break;
                            }
                        }

                        if (matched)
                        {
                            index = g;
                            break;
                        }
                    }
                }

                if (index == -1)
                {
                    grafics.Add(new Grafic()
                    {
                        mesh = meshFilters[mf].sharedMesh,
                        //material = meshRenderer.sharedMaterial,
                        materials = meshRenderer.sharedMaterials,
                        elements = new List<MeshRenderer>() { meshRenderer },
                        matrix4X4s = new Matrix4x4[] { Matrix4x4.TRS(meshFilters[mf].transform.position, meshFilters[mf].transform.rotation, meshFilters[mf].transform.localScale) }
                        //elements = new List<GameObject>() { new Grafic.Element() {element = gameObject, matrix4X4 = Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.localScale) } }
                        //matrix4X4s = new List<Matrix4x4>() { Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.localScale) }
                    });
                }
                else
                {
                    grafics[index].elements.Add(meshRenderer);
                    List<Matrix4x4> tmp = new List<Matrix4x4>(grafics[index].matrix4X4s);
                    tmp.Add(Matrix4x4.TRS(meshFilters[mf].transform.position, meshFilters[mf].transform.rotation, meshFilters[mf].transform.localScale));
                    grafics[index].matrix4X4s = tmp.ToArray();

                    //grafics[matched].elements.Add(new Grafic.Element() { element = gameObject, matrix4X4 = Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.localScale) });
                    //grafics[graficMatched].matrix4X4s.Add(Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.localScale));
                }

                meshRenderer.enabled = false;
            }
        }

        public static void RemoveGrafic(this GameObject gameObject) => RemoveGrafic(gameObject.GetComponentsInChildren<MeshRenderer>());

        public static void RemoveGrafic(MeshRenderer[] meshRenderers)
        {
            return;

            for (int g = 0; g < grafics.Count; g++)
            {
                for (int mr = 0; mr < meshRenderers.Length; mr++)
                {
                    if (grafics[g].elements.Contains(meshRenderers[mr]))
                    {
                        List<Matrix4x4> tmp = new List<Matrix4x4>(grafics[g].matrix4X4s);
                        tmp.RemoveAt(grafics[g].elements.IndexOf(meshRenderers[mr]));
                        grafics[g].matrix4X4s = tmp.ToArray();

                        grafics[g].elements.Remove(meshRenderers[mr]);

                        meshRenderers[mr].enabled = true;
                    }
                }
            }

        }








        public static void Render()
        {
            if (grafics == null)
                return;

            for (int g = 0; g < grafics.Count; g++)
            {
                for (int m = 0; m < grafics[g].materials.Length; m++)
                {
                    Graphics.DrawMeshInstanced(grafics[g].mesh, m, grafics[g].materials[m], grafics[g].matrix4X4s);
                }
            }
        }
    }

}
