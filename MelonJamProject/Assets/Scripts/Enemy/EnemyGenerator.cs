using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 4)]
    [SerializeField] private float enemySpawnRate = 1;

    [Header("References")]
    //TODO: several enemy prefabs
    [SerializeField] private GameObject enemyPrefab;
    
    public static EnemyGenerator instance {get; private set;}

    private void Awake() {
        instance = this;
    }

    public void GenerateEnemies(List<Room> generatedRooms) {
        foreach (Room room in generatedRooms) {
            for (int i = 0; i < Random.Range(-1, 2*enemySpawnRate); i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-room.sizes.x+1, room.sizes.x-1),Random.Range(-room.sizes.y+1, room.sizes.y-1), 0);
                spawnPosition = spawnPosition/2 + room.transform.position;
                GameObject obj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                obj.transform.parent = transform;
            }
        }
    }
}
