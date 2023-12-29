using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float health = 10;
    public Slider slider;
    private GameObject script;

    public void InitScript(GameObject script) {
        this.script = script;
        if (slider != null) {
            slider.maxValue = health;
        }
    }

    void Update()
    {
        if (health <= 0) {
            script.SendMessage("Die");
        }
        if (slider != null) {
            slider.value = Mathf.Lerp(slider.value, health, Time.deltaTime*4);
        }
    }

    public void GetDamage(float damage) {
        health -= damage;
    }
}
