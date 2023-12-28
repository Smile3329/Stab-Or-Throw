using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Keybuttons")]
    [SerializeField] private InputActionReference _moveInputX;
    [SerializeField] private InputActionReference _moveInputY;
    [Space]
    [SerializeField] private AudioSource _pickupSound;
    [SerializeField] private float walkSpeed = 2f;

    [SerializeField] UI_Inventory _UI_inventory;

    private Animator _anim;
    private Vector2 _moveVector;
    private Rigidbody2D _rb;

    private Invenetory _inventory;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _inventory = new Invenetory();

        _UI_inventory.SetInventory(_inventory);

        ItemWorld.SpawnItemWorld(new Vector3(10, 20), new Item{_itemType = Item.ItemType.DamagePotion });
        ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item{_itemType = Item.ItemType.DamagePotion });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            _inventory.AddItem(new Item { _itemType = Item.ItemType.DamagePotion });
            _UI_inventory.RefreshInventoryItems();
            itemWorld.DestroySelf();
        }
    }

    private void Update()
    {
        _moveVector.x = _moveInputX.action.ReadValue<float>();
        _moveVector.y = _moveInputY.action.ReadValue<float>();

        Move(walkSpeed);

        //_anim.SetFloat("Horizontal", _moveVector.x);
        //_anim.SetFloat("Vertical", _moveVector.y);
        //_anim.SetFloat("Speed", _moveVector.sqrMagnitude);

    }

    private void Move(float speed)
    {
        //_anim.SetFloat("moveX", Mathf.Abs(_MoveVector.x));
        _rb.velocity = new Vector2(_moveVector.x * speed, _rb.velocity.y);
        _rb.velocity = new Vector2(_rb.velocity.x, _moveVector.y * speed);
    }

    public void PickupSound()
    {
        _pickupSound.Play();
    }
}
