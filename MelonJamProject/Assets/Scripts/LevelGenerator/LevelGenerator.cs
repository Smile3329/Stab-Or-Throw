using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // public int levelsPassed = 0;

    [Header("References")]
    [SerializeField] private Room roomPrefab;
    [SerializeField] private Room firstRoom;
    // [SerializeField] private GameObject doorPrefab;
    // [SerializeField] private GameObject wallPrefab;
    
    private List<Room> createdRooms = new List<Room>();
    private List<Room> unclosedRooms = new List<Room>();
    private Room currentRoom;

    private int roomCount;

    private void Start() {
        InitFirstRoom();
        InitRoomCount();
        
        GenerateRooms();
    }

    private void InitFirstRoom() {
        createdRooms.Add(firstRoom);
        unclosedRooms.Add(firstRoom);
    }

    private void InitRoomCount() {
        // roomCount = Random.Range(5 + levelsPassed, 10 + 2 * levelsPassed);
        roomCount = 6;
    }

    private void GenerateRooms() {
        while (createdRooms.Count <= roomCount) {
            currentRoom = unclosedRooms[Random.Range(0, unclosedRooms.Count-1)];
            unclosedRooms.Remove(currentRoom);

            List<Vector2> availableSides = currentRoom.GetAvailableSides();

            int createdRoomsBefore = createdRooms.Count;

            foreach (Vector2 side in availableSides) {
                if (Random.Range(0, 4) == 0) {     
                    CreateRoom(side);
                }
            }

            if (createdRoomsBefore == createdRooms.Count) {
                CreateRoom(availableSides[Random.Range(0, availableSides.Count-1)]);
            }
        }

        GenerateDoors();
    }

    private void GenerateDoors() {
        foreach (Room room in createdRooms) {
            room.GenerateDoor();
        }
    }

    private void CreateRoom(Vector2 side) {
        Vector3 position = side*currentRoom.sizes;

        Room room = Instantiate(roomPrefab.gameObject, position + currentRoom.transform.position, Quaternion.identity).GetComponent<Room>();
        createdRooms.Add(room);
        unclosedRooms.Add(room);
    }
}
