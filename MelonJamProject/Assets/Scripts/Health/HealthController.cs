using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float health = 10;
    private GameObject script;

    public void InitScript(GameObject script) {
        this.script = script;
    }

    void Update()
    {
        if (health <= 0) {
            script.SendMessage("Die");
        }
    }

    public void GetDamage(float damage) {
        health -= damage;
    }
}
