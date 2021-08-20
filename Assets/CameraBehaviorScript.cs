using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviorScript : MonoBehaviour
{
    public GameObject CameraTarget = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Updating camera Y position to match the player's, if theirs is higher
        if (CameraTarget.transform.position.y > transform.position.y)
            //Z depth is retained to keep the camera from clipping into the player object
            transform.position = new Vector3(0, CameraTarget.transform.position.y, transform.position.z);
    }
}
