using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private ItemWorld itemPrefab;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private AudioSource source;

    private Animator anim;
    private bool openable = false;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (openable && inputAction.action.IsPressed()) {
            OpenChest();
            openable = false;
        }
    }

    private void OpenChest() {
        for (int i = 0; i < Random.Range(1, 3); i++) {
            Vector3 circle = Random.insideUnitCircle;
            ItemWorld item = Instantiate(itemPrefab.gameObject, transform.position + circle, Quaternion.identity).GetComponent<ItemWorld>();
            int itemType = Random.Range(0, 10);

            if (itemType < 4) {
                item._itemType = Item.ItemType.DamagePotion;
            } else {
                if (itemType > 7) {
                    item._itemType = Item.ItemType.IcePotion;
                } else {
                    item._itemType = Item.ItemType.HealthPotion;
                }
            }
        }

        ItemWorld obj = Instantiate(itemPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<ItemWorld>();
        obj._itemType = Item.ItemType.DamagePotion;

        if (Random.Range(0, 5) > 3) {
            Instantiate(swordPrefab, transform.position + Vector3.one, Quaternion.identity);
        }

        anim.SetTrigger("Open");
        source.Play();

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            openable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            openable = false;
        }
    }
}
