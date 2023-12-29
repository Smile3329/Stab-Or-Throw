using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject minimapIcon;

    public Vector2 sizes;
    public bool playerInRoom {get; private set;} = false;

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

    private List<Vector2> GetClosedSides() {
        List<Vector2> openSides = new List<Vector2>();

        // up
        Vector3 position = Vector2.up*sizes;
        bool hit = Physics2D.Raycast(transform.position + position, Vector2.up, 1);
        
        if (hit) {
            openSides.Add(Vector2.up);
        }
        
        // right
        position = Vector2.right*sizes;
        hit = Physics2D.Raycast(transform.position + position, Vector2.right, 1);
        if (hit) {
            openSides.Add(Vector2.right);
        }

        // down
        position = Vector2.down*sizes;
        hit = Physics2D.Raycast(transform.position + position, Vector2.down, 1);
        if (hit) {
            openSides.Add(Vector2.down);
        }

        // left
        position = Vector2.left*sizes;
        hit = Physics2D.Raycast(transform.position + position, Vector2.left, 1);
        if (hit) {
            openSides.Add(Vector2.left);
        }

        return openSides;
    }

    public void GenerateDoor() {
        List<Vector2> sides = GetClosedSides();

        foreach (Vector2 side in sides) {
            Vector3 position = side*(sizes/2);
            Vector3 halfPostion = doorPrefab.transform.TransformVector(Vector2.up)/2;
            position = transform.position + position - halfPostion;

            GameObject obj = Instantiate(doorPrefab, position, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.right = side;
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
}
