using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{
    public GameObject CameraTarget = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //null check, in case player object is gone
        if (CameraTarget == null)
            return;
        
        //Updating camera Y position to match the player's, if theirs is higher
        if (CameraTarget.transform.position.y > transform.position.y)
            //Z depth is retained to keep the camera from clipping into the player object
            transform.position = new Vector3(0, CameraTarget.transform.position.y, transform.position.z);
    }

    //I will use this for the player exiting the camera view - it's probably not the most flexible solution, but it works for such a short project
    void OnTriggerExit2D(Collider2D Collider)
    {
        //check tag
        if (Collider.tag.Equals("Player"))
        {
            //Get player behavious script
            PlayerBehaviourScript PlayerScript = Collider.gameObject.GetComponent<PlayerBehaviourScript>();

            if (PlayerScript == null)
            {
                Debug.Log("CameraBehaviourScript: Failed to fetch Player's behaviour script!");
                return;
            }

            //Kill
            PlayerScript.Death();
        }
    }
}
