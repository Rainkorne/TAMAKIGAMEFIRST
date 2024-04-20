using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivoten : MonoBehaviour
{
    // Start is called before the first frame update
public Vector3 newPivotPosition;

    void Start()
    {
        ChangePivot(newPivotPosition);
    }

    void ChangePivot(Vector3 newPivot)
    {
        Vector3 offset = transform.position - newPivot;
        transform.position = newPivot;
        foreach (Transform child in transform)
        {
            child.position += offset;
        }
    }
}