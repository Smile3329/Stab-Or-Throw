using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [SerializeField] private GameObject swordPrefab;
    private bool hasSword = true;
    private SpriteRenderer weaponRenderer;
    private SpriteRenderer characterRenderer;
    private float timer;
    private float swordTime;

    void Update()
    {
        if (hasSword && timer < swordTime) {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 right = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.right = Vector3.Lerp(transform.right, right, Time.deltaTime * 7);

            Vector2 scale = transform.localScale;
            scale.y = 1 * Mathf.Sign(mousePosition.x);
            transform.localScale = scale;

            timer += Time.deltaTime;

            if (Input.GetMouseButtonDown(1)) {

            }

        } else if (timer > swordTime) {
            timer = 0;
        }
    }

    private void Attack() {
        
    }
}
