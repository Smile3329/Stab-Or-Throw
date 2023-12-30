using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionExplodeScript : MonoBehaviour
{
    [SerializeField] private float _icePotionFreezeTimeEnemy = 3f;

    [SerializeField] float _timeToDestroy;

    public Item _item;

    public bool _throwed;

    private void Update()
    {
        if (_item._itemType == Item.ItemType.HealthPotion)
        {
            if (_throwed)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().HealUsed();
                GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>().health = 10;
                DestroyPotion(0f);
            }
        }
    }

    public void ExplodePotionWall()
    {
        switch (_item._itemType)
        {
            default:
            case Item.ItemType.DamagePotion:
                DestroyPotion(1);
                break;

            case Item.ItemType.IcePotion:
                DestroyPotion(0);
                break;

            case Item.ItemType.HealthPotion:
                DestroyPotion(1);
                break;
        }
    }

    public void ExplodePotionEnemy(HealthController healthController, Collider2D enemy)
    {
        switch (_item._itemType)
        {
            default:
            case Item.ItemType.DamagePotion:
                healthController.health -= 5;
                DestroyPotion(1);
                break;

            case Item.ItemType.IcePotion:
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().IcePotionCrashSound();
                enemy.transform.GetComponent<EnemyAI>().StartFreezeEnemy(_icePotionFreezeTimeEnemy);
                DestroyPotion(0);
                break;

            case Item.ItemType.HealthPotion:
                DestroyPotion(1f);
                break;
        }
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            ExplodePotionWall();
        }

        if (collision.tag == "Enemy")
        {
            ExplodePotionEnemy(collision.GetComponent<HealthController>(), collision);     
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            ExplodePotionWall();
        }

        if (collision.tag == "Enemy")
        {
            ExplodePotionEnemy(collision.GetComponent<HealthController>(), collision);
        }
    }

    public void DestroyPotion(float time)
    {
        Destroy(transform.parent.gameObject, time);
    }

    public void DefaultCrashPotion()
    {
        DestroyPotion(_timeToDestroy);
    }
    
    public void SetItem(Item item)
    {
        _item = item;
    }
}
