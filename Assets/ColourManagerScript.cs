using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManagerScript : MonoBehaviour
{
    //current colour index
    public int CurrentColourIndex = 0;

    //colour list (adjustable in editor)
    public Color[] PlausibleColours;

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

    // Update is called once per frame
    void Update()
    {
        //Setting the colour of the sprite to what we want it to be
        //This could be a lot more efficient, but it works
        if (EntitySpriteRenderer != null && CurrentColourIndex >= 0 && CurrentColourIndex < PlausibleColours.Length)
        {
            EntitySpriteRenderer.color = PlausibleColours[CurrentColourIndex];
        }
    }
}
