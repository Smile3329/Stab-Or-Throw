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

    private int _canPlayAnim;

    private void Update()
    {
        if (_item._itemType == Item.ItemType.HealthPotion)
        {
            if (_throwed)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().HealUsed();
                GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>().health = 20;
                StartCoroutine(DestroyPotion(0f));
            }
        }
    }

    public void ExplodePotionWall()
    {
        switch (_item._itemType)
        {
            default:
            case Item.ItemType.DamagePotion:
                StartCoroutine(DestroyPotion(0f));
                break;

            case Item.ItemType.IcePotion:
                StartCoroutine(DestroyPotion(0f));
                break;

            case Item.ItemType.HealthPotion:
                StartCoroutine(DestroyPotion(0));
                break;
        }
    }

    public void ExplodePotionEnemy(HealthController healthController, Collider2D enemy)
    {
        if (_throwed) {
            switch (_item._itemType)
            {
                default:
                case Item.ItemType.DamagePotion:
                    healthController.health -= 5;
                    StartCoroutine(DestroyPotion(0f));
                    break;

                case Item.ItemType.IcePotion:
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManager>().IcePotionCrashSound();
                    enemy.transform.GetComponent<EnemyAI>().StartFreezeEnemy(_icePotionFreezeTimeEnemy);
                    StartCoroutine(DestroyPotion(0f));
                    break;

                case Item.ItemType.HealthPotion:
                    StartCoroutine(DestroyPotion(0f));
                    break;
            }
            // Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            ExplodePotionEnemy(collision.GetComponent<HealthController>(), collision);     
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Wall" && _throwed)
        {
            ExplodePotionWall();
        }

        if (collision.tag == "Enemy")
        {
            ExplodePotionEnemy(collision.GetComponent<HealthController>(), collision);
        }
    }

    public IEnumerator DestroyPotion(float time)
    {
        if (_throwed) {
            yield return new WaitForSeconds(time);

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

            Destroy(transform.parent.gameObject);
        }
    }

    public void DefaultCrashPotion()
    {
        StartCoroutine(DestroyPotion(_timeToDestroy));
    }
    
    public void SetItem(Item item)
    {
        _item = item;
    }
}
