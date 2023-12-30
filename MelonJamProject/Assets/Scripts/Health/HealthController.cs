using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private bool _itsPlayer;

    public float health = 10;
    public Slider slider;
    public HealthBar healthBar;
    private GameObject script;

    public void InitScript(GameObject script) {
        this.script = script;
        if (slider != null) {
            slider.maxValue = health;
        }
        if (healthBar != null) {
            healthBar.SetMaxValue(health);
        }
    }

    void Update()
    {
        if (slider != null) {
            slider.value = Mathf.Lerp(slider.value, health, Time.deltaTime*4);
        }
        if (healthBar != null) {
            healthBar.ChangeValue(Mathf.Lerp(healthBar.value, health, Time.deltaTime*4));
        }
        if (health <= 0) {
            if (_itsPlayer)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().PlayerDieSound();
            }
            else
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().EnemyDieSound();
            }

            script.SendMessage("Die");

            if (healthBar != null) {
                ScoreCounter.instance.AddScore(10);
                healthBar.ChangeValue(health);
            }

            this.enabled = false;
        }
    }

    public void GetDamage(float damage) {
        health -= damage;
    }
}
