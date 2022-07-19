using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGizmos : MonoBehaviour
{
    public List<GameObject> proceduralObjs = new List<GameObject>();

    private void OnDrawGizmos()
    {
        foreach (GameObject obj in proceduralObjs)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(obj.transform.position, 0.5f);
        }
    }
}