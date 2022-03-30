using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Code from Mr. Pergerson's platformer tutorial
 * https://mr-pergerson.itch.io/platformer-starter-project
 * 
 */

public class CamFollowPlyer : MonoBehaviour
{
    public GameObject thePlayer;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 followPos = new Vector3(thePlayer.transform.position.x,
            thePlayer.transform.position.y, -10);
        camera.transform.position = followPos;
    }
}
