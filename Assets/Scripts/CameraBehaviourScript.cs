using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{
    public GameObject CameraTarget = null;

    public float CamShakeDampenAmount = 0.01f;

    private float CamShakeAmount = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //null check, in case player object is gone
        if (CameraTarget != null)
        {
            //Updating camera Y position to match the player's, if theirs is higher
            if (CameraTarget.transform.position.y > transform.position.y)
                //Z depth is retained to keep the camera from clipping into the player object
                transform.position = new Vector3(0, CameraTarget.transform.position.y, transform.position.z);
        }
        
        //Camera shake! (vlambeer would be proud)
        //Known bug: the camera vertical offset stays, so if the camera shake is triggered while the player is still alive, it could kill the player.
        //Luckily the camera shake right now only appears when the player dies, so the issue doesn't manifest.
        transform.position = new Vector3
        (
            Random.Range(-CamShakeAmount, CamShakeAmount),
            transform.position.y + Random.Range(-CamShakeAmount, CamShakeAmount),
            transform.position.z
        );

        if (CamShakeAmount > 0.0f)
            CamShakeAmount -= CamShakeDampenAmount;
        else //catch to prevent camera from shaking too much
            CamShakeAmount = 0;
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

    public void SetCamShakeAmount(float Amount)
    {
        CamShakeAmount = Amount;
    }
}
