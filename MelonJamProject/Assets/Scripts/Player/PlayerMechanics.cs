using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMechanics : MonoBehaviour
{


    private void Start() {
        GetComponent<HealthController>().InitScript(gameObject);
    }

    public void Die() {
        // animation
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Sword")) {
            GetComponentInChildren<SwordController>().ActivateSword();
            Destroy(other.gameObject);
        }
    }
}
