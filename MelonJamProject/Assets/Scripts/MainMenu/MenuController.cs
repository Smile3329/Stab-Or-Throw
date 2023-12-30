using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Vector3 optionPos;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider general;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider other;
    [SerializeField] private Animator transitor;
    private Animator anim;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        anim = GetComponent<Animator>();
    }

    private void Update() {
        mixer.SetFloat("Master", general.value - 80);
        mixer.SetFloat("Music", music.value - 80);
        mixer.SetFloat("SFX", sfx.value - 80);
        mixer.SetFloat("Other", other.value - 80);
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
        transitor.SetTrigger("NewLevel");
        StartCoroutine(Wait(2f));
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
