using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //The player entity (in the scene)
    public GameObject Player;

    //camera entity (in the scene)
    public GameObject Camera;
    
    //ColourSwitcher entity prefab, for generation
    public GameObject ColourSwitcherPrefab;

    //Star entity prefab
    public GameObject StarPrefab;

    //array of obstacle entity prefabs
    public GameObject[] ObstaclePrefabs;

    //RNG, using the C# System library's implementation for simpler integer randomization
    System.Random RandomGenerator = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check camera position in level, spawn new obstacles, stars or colour switchers if necessary

        //stars and colour switchers have the same spawn behaviour
    }
}
