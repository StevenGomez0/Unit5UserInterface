using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseSwipe : MonoBehaviour
{
    private GameManager manager;
    private Camera cam;
    private TrailRenderer trail;
    private Vector3 mousePos;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        manager = GetComponent<GameManager>();
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
        if (Input.GetMouseButton(0))
        {
            trail.enabled = true;
        }
        else
        {
            trail.enabled = false;
        }
    }
}
