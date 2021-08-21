using System.Collections;
using System.Collections.Generic;
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

    //Reference to the colour manager component, to change colours
    ColourManagerScript ColourScriptComponent = null;

    //Workaround to fixedupdate not getting the button state consistently
    bool MouseButtonHeld = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set starting position, x and z don't matter since neither axis is used in the game
        transform.position = new Vector3(0, StartingHeight, 0);

        ColourScriptComponent = GetComponent<ColourManagerScript>();

        if (ColourScriptComponent == null)
            Debug.LogWarning("Player: Failed to fetch ColourManagerScript component!");
        else
            ColourScriptComponent.CurrentColourIndex = 2;
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

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.tag.Equals("Obstacle"))
        {
            CheckObstacleCollision();
        }
    }

    private void CheckObstacleCollision()
    {
        Debug.Log("Ouch!");
    }

    private void Death()
    {

    }
}
