using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //public (editor) fields:

    //The player entity (in the scene)
    public GameObject PlayerEntityPrefab;

    //The player entity (in the scene)
    public Transform ObjectSpawnerTransform;

    //camera entity (in the scene)
    public GameObject CameraEntity;

    //This is for the UI element
    public GameObject TextRendererEntity;
    
    //ColourSwitcher entity prefab, for generation
    public GameObject[] CollectiblePrefabs;

    //array of obstacle entity prefabs
    public GameObject[] ObstaclePrefabs;

    //distance between two spawned objects
    public float SpawnDistance = 0;

    //how far below the camera the object is allowed to be before it's cleaned
    public float DespawnDistance = 0;

    //private fields:

    //The player entity
    GameObject PlayerEntity;

    //The player entity
    Vector3 CameraStartPos;

    //This is a list of all the objects created by the spawner system, in order to despawn stuff outside of the view
    List<GameObject> SpawnedObjects;

    //The next position to spawn an object in
    float NextSpawnPos = 0;

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

        //initializing the object list
        SpawnedObjects = new List<GameObject>();

        //saving the starting position of the camera for resetting
        CameraStartPos = CameraEntity.transform.position;

        //level reset also doubles as level instantiation!
        ResetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //Check camera position in level, spawn new obstacles, stars or colour switchers if necessary
        if (CameraEntity != null)
        {
            //
            if (CameraEntity.transform.position.y >= NextSpawnPos)
            {
                NextSpawnPos = CameraEntity.transform.position.y + SpawnDistance;
                SpawnObject();
            }
        }
        else
            Debug.LogWarning("LevelManager: CameraEntity is null!");

        if (PlayerEntity == null)
        {
            Debug.Log("Player is gone! Allowing reset");
            
            if (Input.GetMouseButton(0))
            {
                Debug.Log("Level reset");
                ResetLevel();
            }
        }

        //Updating the UI mesh
        if (TextMesh != null)
            TextMesh.text = StarCount.ToString();

        //cleaning collectibles and obstacles that have left the game view
        for (int i = 0; i < SpawnedObjects.Count; i++)
        {
            GameObject ent = SpawnedObjects[i];

            //if the entity is already null, we remove the reference
            if (ent == null)
            {
                //removing the reference from the list
                SpawnedObjects.RemoveAt(i);
                //preventing the loop from jumping indexes
                i--;
            }
            else if (CameraEntity.transform.position.y - ent.transform.position.y > DespawnDistance)
            {
                Destroy(ent);
                //removing the reference from the list
                SpawnedObjects.RemoveAt(i);
                //preventing the loop from jumping indexes
                i--;
            }
        }
    }

    private void SpawnObject()
    {
        //Random int to pick something to spawn
        int ChooseObjectType = RandomGenerator.Next(0, 4);

        //0 spawns a collectible - star or a colour switcher gizmo
        if (ChooseObjectType == 0)
        {
            //spawn random collectible
            ChooseObjectType = RandomGenerator.Next(0, CollectiblePrefabs.Length);
            
            //spawn position
            Vector3 SpawnPos = new Vector3(0, ObjectSpawnerTransform.position.y, 0);

            //adding the newly spawned collectible into the list of spawned stuff
            GameObject NewCollectible = (GameObject)Instantiate(CollectiblePrefabs[ChooseObjectType], SpawnPos, Quaternion.identity);
            SpawnedObjects.Add(NewCollectible);

        }
        else //otherwise we spawn an obstacle
        {
            //spawn random obstacle
            ChooseObjectType = RandomGenerator.Next(0, ObstaclePrefabs.Length);
            GameObject ObstacleType = ObstaclePrefabs[ChooseObjectType];

            //the reason for this strange spawn position is that some of the prefabs (like the blade) have offset, not including it makes the game impossible
            Vector3 SpawnPos = new Vector3(ObstacleType.transform.position.x, ObjectSpawnerTransform.position.y, 0);

            //adding the newly spawned obstacle into the list of spawned stuff
            GameObject NewObstacle = (GameObject)Instantiate(ObstacleType, SpawnPos, Quaternion.identity);
            SpawnedObjects.Add(NewObstacle);
        }
    }

    public void AddStar()
    {
        StarCount++;
    }

    private void ResetLevel()
    {
        //resetting camera
        CameraEntity.transform.position = CameraStartPos;

        //spawning the player
        PlayerEntity = (GameObject)UnityEngine.Object.Instantiate(PlayerEntityPrefab, Vector3.zero, Quaternion.identity);

        //Setting the camera target
        if (CameraEntity != null)
        {
            CameraBehaviourScript CamScript = CameraEntity.GetComponent<CameraBehaviourScript>();

            if (CamScript != null)
            {
                CamScript.CameraTarget = PlayerEntity;
            }
            else
                Debug.LogWarning("LevelManager: Failed to fecth CameraBehaviourScript from camera entity!");
        }
        else
                Debug.LogWarning("LevelManager: Camera is null!");

        //setting the player's level manager target to self, and camera target to the camera entity
        PlayerBehaviourScript PScript = PlayerEntity.GetComponent<PlayerBehaviourScript>();

        if (PScript != null)
        {
            PScript.LevelManagerEntity = gameObject;
            PScript.GameCamera = CameraEntity;
        }
        else
            Debug.LogWarning("LevelManager: Failed to fecth PlayerBehaviourScript from player entity!");

        //clear the collectibles and obstacles
        foreach (GameObject ent in SpawnedObjects)
        {
            //Destroy the entity
            Destroy(ent);
        }

        SpawnedObjects.Clear();

        NextSpawnPos = 0;
    }
}
