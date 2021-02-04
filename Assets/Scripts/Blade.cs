using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float CameraOffsetZ;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        // Accounts for distance between the camera and the objects plane
        CameraOffsetZ = -1 * Camera.main.transform.position.z;
    }

    void Update()
    {
        SetBladeToMouse();
    }

    private void SetBladeToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = CameraOffsetZ;

        rb2D.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
