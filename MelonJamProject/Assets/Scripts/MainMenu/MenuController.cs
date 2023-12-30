using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Vector3 optionPos;
    private Animator anim;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    public void Options() {
        anim.SetTrigger("Option");
        StartCoroutine(Wait(optionPos));
    }

    private IEnumerator Wait(Vector3 pos) {
        yield return new WaitForSeconds(1.5f);
        transform.position = pos;
    }

    public void Menu() {
        anim.SetTrigger("Menu");
        StartCoroutine(Wait(startPos));
    }

    public void Quit() {
        Application.Quit();
    }

    public void StartGame() {
        // SceneManager.LoadScene(1);
    }
}
