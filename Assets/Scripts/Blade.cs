using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Collider2D c2D;
    private Vector2 lastPos;
    private float CameraOffsetZ;

    [SerializeField]
    private float thresholdVel;
    

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        c2D = GetComponent<Collider2D>();

        lastPos = rb2D.position;

        // Accounts for distance between the camera and the objects plane
        CameraOffsetZ = -1 * Camera.main.transform.position.z;
    }

    void Update()
    {
        SetBladeToMouse();
        c2D.enabled = IsMouseMoving();
    }

    private void SetBladeToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = CameraOffsetZ;

        rb2D.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseMoving()
    {
        float vel = (rb2D.position - lastPos).magnitude / Time.deltaTime;
        lastPos = rb2D.position;

        return vel >= thresholdVel;
    }
}
