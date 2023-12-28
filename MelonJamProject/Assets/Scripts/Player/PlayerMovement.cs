using System.Collections;
using System.Collections.Generic;
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

    private Animator _anim;
    private Vector2 _moveVector;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
