using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    [SerializeField] private Transform playerMinimapIcon;
    private Room room;

    private void Start() {
        GetComponent<HealthController>().InitScript(gameObject);
    }

    private void Update() {
        
    }

    public void Die() {
        LevelController.instance.PlayerDied();
        // animation
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Sword")) {
            GetComponentInChildren<SwordController>().ActivateSword();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Room")) {
            playerMinimapIcon.position = other.transform.position;
            room = other.GetComponent<Room>();
        }
    }

    public Room GetCurrentRoom() {
        return room;
    }
}
