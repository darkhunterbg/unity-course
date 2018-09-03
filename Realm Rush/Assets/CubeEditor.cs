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
   
        Vector3 gridPos = (transform.position / gridSize) ;
        gridPos.x = Mathf.RoundToInt(gridPos.x);
        gridPos.y = 0;
        gridPos.z = Mathf.RoundToInt(gridPos.z);
        transform.position = gridPos * gridSize;

        string labelText = $"{gridPos.x },{gridPos.z}";

        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = labelText;
        gameObject.name = labelText;
    }
}
