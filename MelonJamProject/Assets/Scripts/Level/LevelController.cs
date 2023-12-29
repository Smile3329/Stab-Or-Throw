using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance {get; private set;}

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        List<Room> generatedRooms = RoomGenerator.instance.GenerateRooms();
        // RoomGenerator.instance.GenerateDoors();
        
        ObstacleGenerator.instance.GenerateObstacles(generatedRooms);
        EnemyGenerator.instance.GenerateEnemies(generatedRooms);
    }
}
