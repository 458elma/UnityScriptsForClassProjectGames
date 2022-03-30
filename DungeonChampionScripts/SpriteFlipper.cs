using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{

    public SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Input.GetAxisRaw("Horizontal")) {
            case 1:
                renderer.flipX = false;
                break;
            case -1:
                renderer.flipX = true;
                break;
        }
    }
}
