using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColShrink : MonoBehaviour {

    // Use this for initialization
    [Range(0.4f,2f)]
    public float scaleVerts = 0.94f;
    public bool thisMeshOnly;
	void Start () {
        if (thisMeshOnly)
        {
            MeshCollider mc = GetComponent<MeshCollider>();
            shrinkMeshCollider(mc);

        }
        else
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject thisChild = transform.GetChild(i).gameObject;
                MeshCollider mc = thisChild.GetComponent<MeshCollider>();
                if (mc == null)
                {
                    mc = thisChild.AddComponent<MeshCollider>();
                    mc.convex = true;
                }
                shrinkMeshCollider(mc);
            }
       // mc.sharedMesh.vertices = newVerts;
    }


    void shrinkMeshCollider(MeshCollider mc)
    {
        Mesh m = mc.sharedMesh;
        if (m == null) Debug.Log(mc.gameObject.name + " has empty mesh !");
        Vector3[] newVerts = m.vertices;
        Mesh newMesh = new Mesh();
        if (m.vertexCount < 5) Debug.Log("warning, " + mc.gameObject.name + " has too few vertexes : " +m.vertexCount);
        for (int i = 0; i < m.vertexCount; i++)
        {
            newVerts[i] = m.vertices[i] * scaleVerts;

        }
        newMesh.vertices = newVerts;
        newMesh.uv = m.uv;
        newMesh.triangles = m.triangles;
        mc.sharedMesh = newMesh;

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
