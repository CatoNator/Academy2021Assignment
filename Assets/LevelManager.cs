using System;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //The player entity (in the scene)
    public GameObject PlayerEntity;

    //The player entity (in the scene)
    public Transform ObjectSpawnerTransform;

    //camera entity (in the scene)
    public Transform CameraEntity;

    //This is for the UI element
    public GameObject TextRendererEntity;
    
    //ColourSwitcher entity prefab, for generation
    public GameObject[] CollectiblePrefabs;

    //array of obstacle entity prefabs
    public GameObject[] ObstaclePrefabs;

    //distance between two spawned objects
    public float SpawnDistance = 0;

    float LastSpawnPos = 0;

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
        if (CameraEntity != null)
        {
            //
            if (CameraEntity.position.y >= LastSpawnPos + SpawnDistance)
            {
                SpawnObject();
                LastSpawnPos = CameraEntity.position.y;
            }
        }
        else
            Debug.LogWarning("LevelManager: Camera entity is null!");

        //stars and colour switchers have the same spawn behaviour

        //Updating the UI mesh
        if (TextMesh != null)
            TextMesh.text = StarCount.ToString();
    }

    private void SpawnObject()
    {
        //Random int to pick something to spawn
        int ChooseObjectType = RandomGenerator.Next(0, 4);

        //0 spawns a collectible - star or a colour switcher gizmo
        if (ChooseObjectType == 0)
        {
            Debug.Log("Spawning collectible");

            //spawn random collectible
            ChooseObjectType = RandomGenerator.Next(0, CollectiblePrefabs.Length);

            UnityEngine.Object.Instantiate(CollectiblePrefabs[ChooseObjectType], ObjectSpawnerTransform.position, Quaternion.identity);

        }
        else //otherwise we spawn an obstacle
        {
            Debug.Log("Spawning obstacle");

            //spawn random obstacle
            ChooseObjectType = RandomGenerator.Next(0, ObstaclePrefabs.Length);
            GameObject ObstacleType = ObstaclePrefabs[ChooseObjectType];

            //the reason for the addition is that some of the prefabs (like the blade) have offset, not including it makes the game basically impossible
            UnityEngine.Object.Instantiate(ObstacleType, ObjectSpawnerTransform.position + ObstacleType.transform.position, Quaternion.identity);
        }
    }

    public void AddStar()
    {
        StarCount++;
    }
}
