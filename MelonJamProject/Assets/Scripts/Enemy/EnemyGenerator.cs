using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Values")]

    [Header("References")]
    //TODO: several enemy prefabs
    [SerializeField] private GameObject enemyPrefab;
    
    public static EnemyGenerator instance {get; private set;}

    private void Awake() {
        instance = this;
    }

    public int GenerateEnemies(List<Room> generatedRooms, float enemySpawnRate) {
        int enemyCountInSum = 0;
        generatedRooms.RemoveAt(0);
        foreach (Room room in generatedRooms) {
            int enemyCount = 0;
            for (int i = 0; i < Random.Range(1*enemySpawnRate, 2*enemySpawnRate); i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-room.sizes.x/2+1, room.sizes.x/2-1),Random.Range(-room.sizes.y/2+1, room.sizes.y/2-1), 0);
                spawnPosition = spawnPosition + room.transform.position;
                GameObject obj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                obj.transform.parent = transform;
                obj.GetComponent<EnemyAI>().SetRoom(room);

                enemyCount++;
            }
            enemyCountInSum += enemyCount;
            room.CreateEnemyIcon(enemyCount);
        }
        return enemyCountInSum;
    }
}
