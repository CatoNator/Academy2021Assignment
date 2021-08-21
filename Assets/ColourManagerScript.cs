using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManagerScript : MonoBehaviour
{
    //current colour index
    public int CurrentColourIndex = 0;

    //colour list (adjustable in editor), I added these as defaults to save time
    public Color[] PlausibleColours =
    {
        new Color(0xFF, 0x00, 0x00, 0xFF),
        new Color(0x00, 0xFF, 0x00, 0xFF),
        new Color(0x00, 0x00, 0xFF, 0xFF),
        new Color(0xFF, 0xFF, 0x00, 0xFF)
    };

    //the entity with the sprite which colour is to be adjusted
    public GameObject SpriteEntity = null;

    //the sprite renderer to be adjusted
    private SpriteRenderer EntitySpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        //fetching the sprite renderer to be updated
        EntitySpriteRenderer = SpriteEntity.GetComponent<SpriteRenderer>();
        
        if (EntitySpriteRenderer == null)
            Debug.LogWarning("ColourManagerScript: EntitySpriteRenderer is null!");
    }

    void Update()
    {
        //I'd make this a separate subroutine, but I don't remember how Unity handles entity creation and I don't feel like debugging race conditions all weekend
        
        //error handling
        if (EntitySpriteRenderer == null)
        {
            Debug.LogWarning("ColourManagerScript: EntitySpriteRenderer is null!");
            return;
        }
        
        //Setting the colour of the sprite to what we want it to be
        if (CurrentColourIndex >= 0 && CurrentColourIndex < PlausibleColours.Length)
        {
            EntitySpriteRenderer.color = PlausibleColours[CurrentColourIndex];
        }
    }
}
