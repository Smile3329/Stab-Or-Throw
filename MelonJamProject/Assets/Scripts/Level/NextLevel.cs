using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private Animator transitor;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private float radius;

    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transitor = Camera.main.transform.Find("leveltransitor").GetComponent<Animator>();
    }

    private void Update() {
        if (inputAction.action.IsPressed() && (player.position - transform.position).magnitude < radius) {
            transitor.SetTrigger("NewLevel");
            StartCoroutine(Wait(2f));
        }
    }

    private IEnumerator Wait(float time) {
        float timer = 0;
        musicSource.volume = 1;
        while (timer < 3) {
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, time*Time.deltaTime);
            
            yield return null;

            timer += Time.deltaTime;
        }

        SceneManager.LoadScene(1);
    }
}
