using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursoren : MonoBehaviour
{
    public float speed = 5f;
    public Material cursorMaterial; // Material supporting emission effect
    [Range(0.0f, 5.0f)]
    public float glowStrength = 1.0f;
    // Start is called before the first frame update
    public float smoothness = 5f;
    private Vector3 targetPosition;
    public Camera mainCamera;
    public float cameraZ = 10f;

    private void Start()
    {
        Cursor.visible = false;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CursorLogic();
        if (cursorMaterial != null)
        {
            cursorMaterial.SetFloat("_Strength", glowStrength);
        }
        Cursor.visible = false;
    }

    private void CursorLogic()
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        Vector3 newPosition = new Vector3(x, y, cameraZ);

        targetPosition = Vector3.Lerp(targetPosition, newPosition, smoothness);
        newPosition = mainCamera.ScreenToWorldPoint(targetPosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    }
}