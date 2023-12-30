using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject upDoorPrefab;
    [SerializeField] private GameObject rightDoorPrefab;
    [SerializeField] private GameObject minimapIcon;
    [SerializeField] private GameObject enemyMinimapIconPrefab;

    public Vector2Int sizes;
    public bool playerInRoom {get; private set;} = false;
    private GameObject enemyIcon;
    private int enemyCount;

    public List<Vector2> GetAvailableSides() {
        List<Vector2> openSides = new List<Vector2>();

        // up
        Vector3 position = Vector2.up*sizes;
        bool hit = Physics2D.Raycast(transform.position + position, Vector2.up, 1);
        
        if (!hit) {
            openSides.Add(Vector2.up);
        }
        
        // right
        position = Vector2.right*sizes;
        hit = Physics2D.Raycast(transform.position + position, Vector2.right, 1);
        if (!hit) {
            openSides.Add(Vector2.right);
        }

        // down
        position = Vector2.down*sizes;
        hit = Physics2D.Raycast(transform.position + position, Vector2.down, 1);
        if (!hit) {
            openSides.Add(Vector2.down);
        }

        // left
        position = Vector2.left*sizes;
        hit = Physics2D.Raycast(transform.position + position, Vector2.left, 1);
        if (!hit) {
            openSides.Add(Vector2.left);
        }

        return openSides;
    }

    public void GenerateDoor() {
        List<Vector2> sides = GetAvailableSides();

        foreach (Vector2 side in sides) {
            if (side.x == 0 && side.y != 0) {
                Vector3 position = side*((sizes-Vector2.one)/2)+Vector2.one/2;
                // Vector3 halfPostion = upDoorPrefab.transform.TransformVector(Vector2.up)/2;

                GameObject obj = Instantiate(upDoorPrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = position;
            } else if (side.x != 0 && side.y == 0) {
                Vector3 position = side*((sizes-Vector2.one)/2)+Vector2.one/2;
                // Vector3 halfPostion = rightDoorPrefab.transform.TransformVector(Vector2.up)/2;

                GameObject obj = Instantiate(rightDoorPrefab);
                obj.transform.parent = transform;
                obj.transform.localPosition = position;
                obj.transform.localScale = new Vector3(Mathf.Sign(side.x), 1, 1);
            }
        }
    }

    private void Update() {
        if (enemyCount <= 0) {
            Destroy(enemyIcon);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRoom = true;
            minimapIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRoom = false;
        }
    }

    public void CreateEnemyIcon(int enemyCount) {
        enemyIcon = Instantiate(enemyMinimapIconPrefab, transform);
        enemyIcon.transform.localPosition = new Vector3(4f, 3.5f);
        enemyIcon.transform.parent = minimapIcon.transform;

        this.enemyCount = enemyCount;
    }

    public void EnemyKilled() {
        enemyCount--;
    }
}
