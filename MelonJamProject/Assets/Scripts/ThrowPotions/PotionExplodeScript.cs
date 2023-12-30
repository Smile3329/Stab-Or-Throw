using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionExplodeScript : MonoBehaviour
{
    [SerializeField] private float _icePotionFreezeTimeEnemy = 3f;

    [SerializeField] float _timeToDestroy;

    [SerializeField] private GameObject _potionExplodeDamage;

    [SerializeField] private GameObject _potionExplodeIce;

    [SerializeField] private GameObject _potionExplodeHealing;

    public Item _item;

    public bool _throwed;

    private bool _exploded;

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
                DestroyPotion(0);
                break;

            case Item.ItemType.IcePotion:
                DestroyPotion(0);
                break;

            case Item.ItemType.HealthPotion:
                DestroyPotion(0);
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
                DestroyPotion(0);
                break;

            case Item.ItemType.IcePotion:
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().IcePotionCrashSound();
                enemy.transform.GetComponent<EnemyAI>().StartFreezeEnemy(_icePotionFreezeTimeEnemy);
                DestroyPotion(0);
                break;

            case Item.ItemType.HealthPotion:
                DestroyPotion(0f);
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
        if (!_exploded)
        {
            switch (_item._itemType)
            {
                default:
                case Item.ItemType.DamagePotion:
                    Instantiate(_potionExplodeDamage, transform.position, Quaternion.identity);
                    break;

                case Item.ItemType.IcePotion:
                    Instantiate(_potionExplodeIce, transform.position, Quaternion.identity);
                    break;

                case Item.ItemType.HealthPotion:
                    Instantiate(_potionExplodeHealing, transform.position, Quaternion.identity);
                    break;
            }
        }
        _exploded = true;
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
