using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    // public int levelsPassed = 0;
    [Header("Values")]

    [Tooltip("More value - more narrow level is\nLess value - less narrow level is")] 
    [Range(1, 10)]
    [SerializeField] private int narrowityLevel = 4;

    [Header("References")]
    [SerializeField] private Room roomPrefab;
    [SerializeField] private Room firstRoom;
    
    public static RoomGenerator instance {get; private set;}
    private List<Room> createdRooms = new List<Room>();
    private List<Room> unclosedRooms = new List<Room>();
    private Room currentRoom;

    private int roomCount;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        InitFirstRoom();
        InitRoomCount();
    }

    private void InitFirstRoom() {
        createdRooms.Add(firstRoom);
        unclosedRooms.Add(firstRoom);
    }

    private void InitRoomCount() {
        // roomCount = Random.Range(5 + levelsPassed, 10 + 2 * levelsPassed);
        roomCount = 12;
    }

    public List<Room> GenerateRooms() {
        while (createdRooms.Count <= roomCount) {
            currentRoom = unclosedRooms[Random.Range(0, unclosedRooms.Count-1)];
            unclosedRooms.Remove(currentRoom);

            List<Vector2> availableSides = currentRoom.GetAvailableSides();

            int createdRoomsBefore = createdRooms.Count;

            foreach (Vector2 side in availableSides) {
                if (Random.Range(0, narrowityLevel) == 0) {     
                    CreateRoom(side);
                }
            }

            if (createdRoomsBefore == createdRooms.Count) {
                CreateRoom(availableSides[Random.Range(0, availableSides.Count-1)]);
            }
        }

        return createdRooms;
    }

    public void GenerateDoors() {
        foreach (Room room in createdRooms) {
            room.GenerateDoor();
        }
    }

    private void CreateRoom(Vector2 side) {
        Vector3 position = side*currentRoom.sizes;

        Room room = Instantiate(roomPrefab.gameObject, position + currentRoom.transform.position, Quaternion.identity).GetComponent<Room>();
        createdRooms.Add(room);
        unclosedRooms.Add(room);
        room.transform.parent = transform;
    }
}
