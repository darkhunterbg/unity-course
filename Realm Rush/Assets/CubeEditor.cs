using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour {
    [SerializeField, Range(1.0f, 20.0f)]
    float gridSize = 10.0f;

    TextMesh textMesh;

    private void Start()
    {

        
    }

    void Update () {
   
        Vector3 snapPos = (transform.position / gridSize) ;
        snapPos.x = Mathf.RoundToInt(snapPos.x);
        snapPos.y = 0;
        snapPos.z = Mathf.RoundToInt(snapPos.z);
        transform.position = snapPos * gridSize;

        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = $"{snapPos.x },{snapPos.z}";

    }
}
