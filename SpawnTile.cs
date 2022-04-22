using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    Transform player;
    List<GameObject> tiles = new List<GameObject>();
    public GameObject [] tileToSpawn;
    public GameObject referenceObject;
    public float timeOffset = 0.4f;
    public float distanceBetweenTiles = 5.0F;
    public float randomValue = 0.8f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        previousTilePosition = referenceObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > timeOffset)
        {
            if (Random.value < randomValue)
                direction = mainDirection;
            else
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            }
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            startTime = Time.time;
            GameObject tile = Instantiate(tileToSpawn[Random.Range(0,tileToSpawn.Length)], spawnPos, Quaternion.Euler(0, 0, 0));
            tiles.Add(tile);
           
            if((tiles[0].transform.position - player.transform.position).magnitude > 10f)
            {
                Destroy(tiles[0]);
                tiles.RemoveAt(0);
            }
        
            
            
            previousTilePosition = spawnPos;
        }
    }
}
