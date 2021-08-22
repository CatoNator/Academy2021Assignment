using System;
using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour
{
    //Player gravity strength; this is a constant downward acceleration
    public float PlayerGravity = -0.005f;

    //Maximum negative vertical velocity. Might be unnecessary
    public float TerminalVelocity = -2.0f;

    //The velocity the player's velocity is set to upon jumping, makes for better game feel that adding upward acceleration
    public float PlayerJumpVelocity = 0.5f;

    //Player velocity
    float VerticalVelocity = 0.0f;

    //this is the height at which the player objects starts in when the game loads in
    public static int StartingHeight = 0;

    //Required gameobjects

    //Level manager, keeps track of score
    public GameObject LevelManagerEntity;

    //Game camera, for camera shake
    public GameObject GameCamera;

    //Death particle generator prefab
    public GameObject DeathParticlePrefab;

    //Star collection particle generator prefab
    public GameObject StarParticlePrefab;

    //Required sound asset references

    //jump noise
    public AudioClip JumpSound;

    //colour switch jingle
    public AudioClip ColourSwitchSound;

    //private script references

    //Player audio emitter
    AudioSource AudioEmitter = null;

    //Reference to the colour manager component, to change colours
    ColourManagerScript ColourScriptComponent = null;

    //Workaround to fixedupdate not getting the button state consistently
    bool MouseButtonHeld = true;

    //Random generator; Using Microsoft's C# Random implementation for a simple integer randomizer, instead of Unity's floating point -based one
    System.Random RandomGenerator = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        //Set starting position, x and z don't matter since neither axis is used in the game
        transform.position = new Vector3(0, StartingHeight, 0);

        //Getting colour manager script, setting random starting colour
        ColourScriptComponent = GetComponent<ColourManagerScript>();

        if (ColourScriptComponent == null)
            Debug.LogWarning("Player: Failed to fetch ColourManagerScript component!");
        else
            ChangeColour();

        //Getting audio source for audio playback
        AudioEmitter = GetComponent<AudioSource>();

        if (AudioEmitter == null)
            Debug.LogWarning("Player: Failed to fetch AudioSource component!");
    }

    //using FixedUpdate for player physics to keep the game speed consistent
    void FixedUpdate()
    {
        //Jump button check
        //The doc said that it just needs to be the left (or primary) mouse button, so here we are
        //Should be only on down state, so you can't hold the button
        if (Input.GetMouseButton(0) && !MouseButtonHeld)
        {
            VerticalVelocity = PlayerJumpVelocity;

            //play jump sound
            AudioEmitter.PlayOneShot(JumpSound, 0.5f);

            MouseButtonHeld = true;
        }
        else if (!Input.GetMouseButton(0) && MouseButtonHeld)
            MouseButtonHeld = false;
        
        if (transform.position.y <= StartingHeight && VerticalVelocity < 0)
        {
            //The position is less than or equal to the ground height; therefore we are on the ground

            //reset velocity and player position
            VerticalVelocity = 0.0f;
            transform.position = new Vector3(0, StartingHeight, 0); //ensure we are always directly on ground level to avoid nasty clips through the 'floor'
        }
        else
        {
            //Velocity is affected by gravity, constantly (temp)
            VerticalVelocity += PlayerGravity;
        }

        //Clamping velocity to terminal velocity
        if (VerticalVelocity < TerminalVelocity)
            VerticalVelocity = TerminalVelocity;

        transform.position = new Vector3(0, transform.position.y + VerticalVelocity, 0);
    }

    //todo: comment
    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.tag.Equals("Obstacle"))
        {
            CheckObstacleCollision(Collider.gameObject);
        }
        else if (Collider.tag.Equals("ColourSwitcher"))
        {
            //change colour of player
            ChangeColour();
            
            //destroy the colour switcher entity
            Destroy(Collider.gameObject, 0);

            //play collection sound
            AudioEmitter.PlayOneShot(ColourSwitchSound, 0.9f);
        }
        else if (Collider.tag.Equals("Star"))
        {
            CollectStar(Collider.gameObject);
        }
    }

    private void CheckObstacleCollision(GameObject Obstacle)
    {
        if (Obstacle == null)
        {
            Debug.LogWarning("Player: Obstacle GameObject passed to CheckObstacleCollision was null!");
            return;
        }
        
        //Getting the colourmanagerscript from the object we collided with
        ColourManagerScript ObstacleCManager = Obstacle.GetComponent<ColourManagerScript>();

        //Null check
        if (ObstacleCManager != null)
        {
            //if the colour indexes don't match, kill player
            if (ObstacleCManager.CurrentColourIndex != ColourScriptComponent.CurrentColourIndex)
                Death();
        }
    }

    private void ChangeColour()
    {
        int NewColour = ColourScriptComponent.CurrentColourIndex;

        //this could in theory cause an infinite loop, but I think bugs caused by an act of god are outside of the scop of this project
        //...so let's hope it won't happen
        while (NewColour == ColourScriptComponent.CurrentColourIndex)
            NewColour = RandomGenerator.Next(0, 4);
        
        //pick random new colour
        ColourScriptComponent.CurrentColourIndex = NewColour;
    }

    private void CollectStar(GameObject Star)
    {
        //creating the particle shower
        Instantiate(StarParticlePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

        //destroy the star entity
        Destroy(Star, 0);

        //Increment the UI counter
        if (LevelManagerEntity != null)
        {
            LevelManager LManScript = LevelManagerEntity.GetComponent<LevelManager>();

            if (LManScript != null)
                LManScript.AddStar();
        }
    }

    public void Death()
    {
        //Debug.Log("F");
        //creating the particle shower
        Instantiate(DeathParticlePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);

        //Adding some camera shake to spice the death up
        if (GameCamera != null)
        {
            CameraBehaviourScript CamScript = GameCamera.GetComponent<CameraBehaviourScript>();

            if (CamScript != null)
            {
                CamScript.SetCamShakeAmount(0.35f);
            }
        }

        //Destroying self (temp)
        Destroy(gameObject, 0.01f);
    }
}
