using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private float radius;

    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if (inputAction.action.IsPressed() && (player.position - transform.position).magnitude < radius) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
