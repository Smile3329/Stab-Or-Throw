using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ThrowPotion : MonoBehaviour
{
    [SerializeField] private float _throwPotionForce;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] GameObject _throwPotionGameobject;
    [SerializeField] Transform _throwPotionSpawnPos;

    public bool _canThrowPotion;

    private bool _potionAim;

    private GameObject _spawnedPotionGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_potionAim)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - _spawnedPotionGameObject.transform.position.x, mousePosition.y - _spawnedPotionGameObject.transform.position.y);

            _spawnedPotionGameObject.transform.up = direction;

            _spawnedPotionGameObject.transform.position = _throwPotionSpawnPos.position;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_potionAim)
            {
                throwPotion(_throwPotionForce);
            }
        }
    }

    public void PotionAim(Item item, Image image)
    {
        if (_canThrowPotion)
        {
            _spawnedPotionGameObject = Instantiate(_throwPotionGameobject, _throwPotionSpawnPos.position, Quaternion.identity);

            _spawnedPotionGameObject.transform.parent = _playerTransform;

            _spawnedPotionGameObject.GetComponent<SpriteRenderer>().sprite = image.sprite;

            _spawnedPotionGameObject.transform.GetChild(1).GetComponent<PotionExplodeScript>().item = item;

            _potionAim = true;

            _canThrowPotion = false;
        }
    }

    public void throwPotion(float throwForce)
    {
        _potionAim = false;

        //_spawnedPotionGameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        _spawnedPotionGameObject.GetComponent<Rigidbody2D>().velocity = _spawnedPotionGameObject.transform.up * throwForce;

        _spawnedPotionGameObject.transform.GetChild(1).GetComponent<Collider2D>().enabled = true;

        _spawnedPotionGameObject.transform.parent = null;

        _canThrowPotion = true;

        _spawnedPotionGameObject = null;
    }
}
