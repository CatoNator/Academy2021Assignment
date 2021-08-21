using System;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //The player entity (in the scene)
    public GameObject Player;

    //camera entity (in the scene)
    public GameObject Camera;

    //This is for the UI element
    public GameObject TextRendererEntity;
    
    //ColourSwitcher entity prefab, for generation
    public GameObject[] CollectiblePrefabs;

    //array of obstacle entity prefabs
    public GameObject[] ObstaclePrefabs;

    //RNG, using the C# System library's implementation for simpler integer randomization
    System.Random RandomGenerator = new System.Random();

    //TextMesh component
    TextMeshProUGUI TextMesh = null;

    //Star count
    int StarCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //getting the TMP component for score display
        if (TextRendererEntity != null)
        {
            TextMesh = TextRendererEntity.GetComponent<TextMeshProUGUI>();

            if (TextMesh == null)
                Debug.LogWarning("LevelManager: Failed to get TextMeshPro component for score tally!");
            else
                TextMesh.text = StarCount.ToString(); //setting the socre to zero (temp)
        }
        else
            Debug.LogWarning("LevelManager: TextRendererEntity is null!");
    }

    // Update is called once per frame
    void Update()
    {
        //Check camera position in level, spawn new obstacles, stars or colour switchers if necessary

        //stars and colour switchers have the same spawn behaviour

        //Updating the UI mesh
        if (TextMesh != null)
            TextMesh.text = StarCount.ToString();
    }

    public void AddStar()
    {
        StarCount++;
    }
}
