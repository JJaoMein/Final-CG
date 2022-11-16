using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{

    public float activeTime = 2f;

    [Header("Mesh Refresh")]
    public float meshRefreshRate = 0.1f;

    public float meshDestroyDelay = 0.1f;
    public Transform positionToSpawn;

    [Header(("Shader Realted"))] 
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float ShaderVarRefreshRate = 0.05f;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] _skinnedMeshRenderers;

    public void Trail()
    {
        StartCoroutine(ActivateTrail(activeTime));
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (_skinnedMeshRenderers == null) 
                _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                _skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AnimateMaterialFloat(mr.material,0, shaderVarRate,ShaderVarRefreshRate));
                
                Destroy(gObj, meshDestroyDelay);
            }
            


            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material mat, float goal,float rate,  float refreshRate)
    {
        float valutoAnimate = mat.GetFloat(shaderVarRef);

        while (valutoAnimate > goal)
        {
            valutoAnimate -= rate;
            mat.SetFloat(shaderVarRef,valutoAnimate);

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
