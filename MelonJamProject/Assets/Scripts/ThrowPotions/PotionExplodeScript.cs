using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionExplodeScript : MonoBehaviour
{
    public Item item;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplodePotionWall()
    {
        switch (item._itemType)
        {
            default:
            case Item.ItemType.DamagePotion:
                DestroyPotion(2f);
                break;

            case Item.ItemType.IcePotion:
                DestroyPotion(2f);
                break;

            case Item.ItemType.HealthPotion:
                DestroyPotion(2f);
                break;
        }
    }

    public void ExplodePotionEnemy(HealthController healthController)
    {
        switch (item._itemType)
        {
            default:
            case Item.ItemType.DamagePotion:
                healthController.health--;
                DestroyPotion(2f);
                break;

            case Item.ItemType.IcePotion:
                DestroyPotion(2f);
                break;

            case Item.ItemType.HealthPotion:
                DestroyPotion(2f);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            ExplodePotionWall();
        }

        if (collision.tag == "Enemy")
        {
            ExplodePotionEnemy(collision.GetComponent<HealthController>());     
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
            ExplodePotionEnemy(collision.GetComponent<HealthController>());
        }
    }

    public void DestroyPotion(float time)
    {
        Destroy(transform.parent.gameObject, time);
    }
}
