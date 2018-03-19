using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class Cortex : MonoBehaviour
{
    Controller controller;
    private bool isMouseDown = false;
    private GameObject system;
    // Use this for initialization
    void Start()
    {
        controller = new Controller ();
        system = GameObject.Find("System");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        if (isMouseDown) {
            Vector3 mousePos2D = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1);
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos2D);
            system.GetComponent<Particles>().ComputeDistance(mousePosWorld);
        }

        if (controller.IsConnected) { //controller is a Controller object
            Frame frame = controller.Frame (); //The latest frame
            Frame previous = controller.Frame (1); //The previous frame
            Debug.Log(frame.ToString());
        }
    }
    
    private double DegToRad(double angle)
    {
        return Math.PI * angle / 180.0;
    }
    
}
