using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
    private Mesh meshFilter;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }


}
