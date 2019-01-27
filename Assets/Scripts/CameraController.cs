using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public float panSpeed;
    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currentZoom = cam.orthographicSize;
        if (Input.mousePosition.x > Screen.width - 30)
            cam.transform.Translate(Vector3.right * 0.1f * panSpeed);
        else if (Input.mousePosition.x < 30)
            cam.transform.Translate(Vector3.left * 0.1f * panSpeed);
        var zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom != 0)
        {
            currentZoom -= zoom;
        }
        if (currentZoom > 3f && currentZoom < 15f) cam.orthographicSize = currentZoom;
    }
}
