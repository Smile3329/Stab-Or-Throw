using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static ObstacleGenerator instance {get; private set;}

    [SerializeField] private float obstacleSpawnRate;
    //TODO: More obstacles
    [SerializeField] private GameObject obstaclePrefab;

    private void Awake() {
        instance = this;
    }

    public void GenerateObstacles(List<Room> generatedRooms) {
        //TODO: Spawn with 100% in rooms with long range enemies
        foreach (Room room in generatedRooms) {
            for (int i = 0; i < Random.Range(-1, 2*obstacleSpawnRate); i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-room.sizes.x+3, room.sizes.x-3),Random.Range(-room.sizes.y+3, room.sizes.y-3), 0);
                spawnPosition = spawnPosition/2 + room.transform.position;
                spawnPosition = new Vector3(Mathf.RoundToInt(spawnPosition.x), Mathf.RoundToInt(spawnPosition.y), 0);
                
                GameObject obj = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
                obj.transform.parent = room.transform;
            }
        }
    }
}
