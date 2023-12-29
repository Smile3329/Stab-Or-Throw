using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    
    private void Awake() {
        instance = this;

        foreach (Decoration decoration in decorationsClass) {
            decorations.Add(decoration.sprite, decoration.type);
        }
    }

    public void GenerateDecorations(List<Room> generatedRooms) {
        foreach (Room room in generatedRooms) {
            foreach (DecorationType type in decorations.Values) {
                switch (type) {
                    case DecorationType.Wall: 
                        GenerateWallDecoration(room, decorations.FirstOrDefault(x => x.Value == type).Key);
                        break;
                    case DecorationType.Floor:
                        for (int i = 1; i <= Random.Range(1, 3); i++) {
                            GenerateFloorDecoration(room, decorations.FirstOrDefault(x => x.Value == type).Key);
                        }
                        break;
                    case DecorationType.Corner:
                        GenerateCornerDecoration(room, decorations.FirstOrDefault(x => x.Value == type).Key);
                        break;
                }
            }
        }
    }

    private void GenerateWallDecoration(Room room, Sprite sprite) {
        Transform obj = CreateGameObject(sprite).transform;
        obj.parent = room.transform;
        
        Vector2Int position = new Vector2Int(Random.Range(-room.sizes.x/2, room.sizes.x/2), room.sizes.y/2);

        obj.localPosition = new Vector3(position.x, position.y, 0);
    }

    private void GenerateFloorDecoration(Room room, Sprite sprite) {
        Transform obj = CreateGameObject(sprite).transform;
        obj.parent = room.transform;

        Vector2Int position = new Vector2Int(Random.Range(-room.sizes.x/2+1, room.sizes.x/2-1), Random.Range(-room.sizes.y/2+1, room.sizes.y/2-1));

        obj.localPosition = new Vector3(position.x, position.y, 0);
    }

    private void GenerateCornerDecoration(Room room, Sprite sprite) {
        Transform obj = CreateGameObject(sprite).transform;
        obj.parent = room.transform;

        Vector2Int position = new Vector2Int(room.sizes.x/2*Mathf.RoundToInt(Mathf.Sign(Random.Range(-2, 1))), room.sizes.y/2);

        obj.localPosition = new Vector3(position.x, position.y, 0);
    }

    private GameObject CreateGameObject(Sprite sprite) {
        GameObject obj = Instantiate(new GameObject("Decoration (" + sprite.name + ")"));
        SpriteRenderer renderer = obj.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.renderingLayerMask = 2;
        return obj;
    }
}
