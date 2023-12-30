using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DecorationGenerator : MonoBehaviour
{
    public enum DecorationType {
        Wall,
        Floor,
        Corner
    }

    [System.Serializable]
    public struct Decoration {
        public Sprite sprite;
        public DecorationType type;
    }

    [SerializeField] private List<Decoration> decorationsClass;

    public static DecorationGenerator instance {get; private set;}
    private Dictionary<Sprite, DecorationType> decorations = new Dictionary<Sprite, DecorationType>();

    private List<Vector2> wallDecorationPositions = new List<Vector2>();
    
    private void Awake() {
        instance = this;

        foreach (Decoration decoration in decorationsClass) {
            decorations.Add(decoration.sprite, decoration.type);
        }
    }

    public void GenerateDecorations(List<Room> generatedRooms) {
        foreach (Room room in generatedRooms) {
            wallDecorationPositions = new List<Vector2>();

            foreach (Sprite sprite in decorations.Keys) {
                switch (decorations[sprite]) {
                    case DecorationType.Wall: 
                        if (Random.Range(-2, 1) == 0) {
                            GenerateWallDecoration(room, sprite);
                        }
                        break;
                    case DecorationType.Floor:
                        for (int i = 1; i <= Random.Range(1, 3); i++) {
                            GenerateFloorDecoration(room, sprite);
                        }
                        break;
                    case DecorationType.Corner:
                        if (Random.Range(-1, 2) == 0) {
                            GenerateCornerDecoration(room, sprite);
                        }
                        break;
                }
            }
        }

        // ClearDecorations();
    }

    private void GenerateWallDecoration(Room room, Sprite sprite) {
        Transform obj = CreateGameObject(sprite).transform;
        obj.parent = room.transform;
        
        Vector2 position = new Vector2(Random.Range((-room.sizes.x+2)/2, (room.sizes.x-2)/2), room.sizes.y/2) + Vector2.one/2;

        if (position.x == 0) {
            position.x += 1;
        }

        while (wallDecorationPositions.Contains(position)) {
            position = new Vector2(Random.Range((-room.sizes.x+2)/2, (room.sizes.x-2)/2), room.sizes.y/2) + Vector2.one/2;

            if (position.x == 0.5f) {
                position.x += 1;
            }
        }

        obj.localPosition = new Vector3(position.x, position.y, 0);
        wallDecorationPositions.Add(position);
    }

    private void GenerateFloorDecoration(Room room, Sprite sprite) {
        Transform obj = CreateGameObject(sprite).transform;
        obj.parent = room.transform;

        Vector2 position = new Vector2(Random.Range(-room.sizes.x/2+1, room.sizes.x/2-1), Random.Range(-room.sizes.y/2+1, room.sizes.y/2-1)) + Vector2.one/2;

        obj.localPosition = new Vector3(position.x, position.y, 0);
        obj.localScale = new Vector3(Mathf.Sign(Random.Range(-2, 1)), 1, 1);
    }

    private void GenerateCornerDecoration(Room room, Sprite sprite) {
        Transform obj = CreateGameObject(sprite).transform;
        obj.parent = room.transform;

        Vector2 position = new Vector2(4.5f, 3.75f);

        if (sprite.name.Contains('4')) {
            position.x = -3.5f;
            position.y = 4;
        }

        obj.localPosition = new Vector3(position.x, position.y, 0);
    }

    private GameObject CreateGameObject(Sprite sprite) {
        GameObject obj = Instantiate(new GameObject("Decoration (" + sprite.name + ")"));
        obj.transform.tag = "Decoration";
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.renderingLayerMask = 2;

        return obj;
    }
}
