using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGenerator : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;

    public static ChestGenerator instance {get; private set;}

    private void Awake() {
        instance = this;
    }

    public void Generate(List<Room> generatedRooms, float multiplier) {
        foreach (Room room in generatedRooms) {
            for (int i = 0; i < Random.Range(1 * multiplier, 3 * (multiplier-0.5f)); i++) {
                GameObject obj = Instantiate(chestPrefab, room.transform);
                obj.transform.localPosition = new Vector2(Random.Range((-room.sizes.x+2)/2, (room.sizes.x-2)/2), Random.Range((-room.sizes.y+2)/2, (room.sizes.y-2)/2));
            }
        }
    }
}
