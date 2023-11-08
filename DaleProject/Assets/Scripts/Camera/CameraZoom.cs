using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Camera cam;

    public float maxZoom;
    public float minZoom;

    public float zoomSpeed = 0.5f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && cam.orthographicSize > maxZoom)
        {
            ZoomOut();
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0 && cam.orthographicSize < minZoom)
        {
            ZoomIn();
        }

        if (Input.GetMouseButtonDown(2))
        {
            ResetZoom();
        }
    }

    public void ZoomIn()
    {
        cam.orthographicSize += zoomSpeed;
    }

    public void ZoomOut()
    {
        cam.orthographicSize -= zoomSpeed;
    }

    public void ResetZoom()
    {
        cam.orthographicSize = 7.5f;
    }
}
