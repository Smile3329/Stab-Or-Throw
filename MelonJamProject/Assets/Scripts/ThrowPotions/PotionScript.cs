using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    [SerializeField] private Collider2D trigger;

    public bool _throwed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_throwed)
        {
            if (collision.transform.tag == "Wall")
            {
                trigger.enabled = true;
            }

            if (collision.transform.tag == "Enemy")
            {
                trigger.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_throwed)
        {
            if (collision.transform.tag == "Wall")
            {
                trigger.enabled = true;
            }

            if (collision.transform.tag == "Enemy")
            {
                trigger.enabled = true;
            }
        }
    }
}
