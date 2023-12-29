using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    [Header("Keybuttons")]
    [SerializeField] private InputActionReference _moveInputX;
    [SerializeField] private InputActionReference _moveInputY;
    [Space]

    [SerializeField] private AudioSource _pickupPotionSound;
    [SerializeField] private float walkSpeed = 2f;

    [SerializeField] UI_Inventory _UI_inventory;

    private Animator _anim;
    private Vector2 _moveVector;
    private Rigidbody2D _rb;

    private Invenetory _inventory;

    private bool faceRight = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _inventory = new Invenetory();

        _UI_inventory.SetInventory(_inventory);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            _inventory.AddItem(new Item { _itemType = itemWorld._item._itemType });
            _UI_inventory.RefreshInventoryItems();
            itemWorld.DestroySelf();
            PickupPotionSound();
        }
    }

    private void Update()
    {
        _moveVector.x = _moveInputX.action.ReadValue<float>();
        _moveVector.y = _moveInputY.action.ReadValue<float>();

        Move(walkSpeed);
            
        Reflect();

        _anim.SetFloat("Horizontal", _moveVector.x);
        _anim.SetFloat("Vertical", _moveVector.y);
        _anim.SetFloat("Speed", _moveVector.sqrMagnitude);

    }

    private void Move(float speed)
    {
        // _anim.SetFloat("moveX", Mathf.Abs(_moveVector.x));
        _rb.velocity = new Vector2(_moveVector.x * speed, _rb.velocity.y);
        _rb.velocity = new Vector2(_rb.velocity.x, _moveVector.y * speed);
        //_rb.AddForce(new Vector2(_moveVector.x * speed, 0), ForceMode2D.Impulse);
        //_rb.AddForce(new Vector2(0, _moveVector.y * speed), ForceMode2D.Impulse);
    }

    private void Reflect()
    {
        if ((_moveVector.x > 0 && !faceRight) || (_moveVector.x < 0 && faceRight))
        {
            if (faceRight)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (!faceRight)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            faceRight = !faceRight;
        }
    }

    public void PickupPotionSound()
    {
        _pickupPotionSound.Play();
    }
}
