using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour {
    [SerializeField, Range(1.0f, 20.0f)]
    float gridSize = 10.0f;

    void Update () {
        Vector3 snapPos = (transform.position / gridSize) ;
        snapPos.x = Mathf.RoundToInt(snapPos.x);
        snapPos.y = Mathf.RoundToInt(snapPos.y);
        snapPos.z = Mathf.RoundToInt(snapPos.z);
        snapPos *= gridSize;
        transform.position = snapPos;
    }
}
