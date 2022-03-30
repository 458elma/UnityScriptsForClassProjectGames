using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Code from Mr. Pergerson's platformer tutorial
 * https://mr-pergerson.itch.io/platformer-starter-project
 * 
 */

public class CamMove : MonoBehaviour
{

    public GameObject followObj;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 trackPos = new Vector3(followObj.transform.position.x, 
            cam.transform.position.y, cam.transform.position.z);
        cam.transform.position = trackPos;
    }
}
