using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //TODO: Enemy types
    [SerializeField] private float damage;
    public float health {get; private set;}

    void Update()
    {
        if (health <= 0) {
            GetComponent<EnemyAI>().enabled = false;
            Destroy(gameObject, 2);
            //TODO: Animation
        }
    }

    public void GetDamage(float damage) {
        health -= damage;
    }

    public void Attack() {
        //TODO: Attack
    }
}
