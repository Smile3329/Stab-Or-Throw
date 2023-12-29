using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform circleOrigin;
    [Header("Values")]
    [SerializeField] private float radius;
    [SerializeField] private float damage;
    [SerializeField] private float swordTime;
    [SerializeField] private float attackCooldown;
    private bool hasSword = false;
    private float timer;
    private bool onCooldown = false;

    void Update()
    {
        if (hasSword && timer < swordTime) {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            mousePosition = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.right = Vector3.Lerp(transform.right, mousePosition, Time.deltaTime * 7);

            Vector2 scale = transform.localScale;
            if (Mathf.Abs(transform.eulerAngles.z-180) < 90) {
                scale.y = -1;
            } else {
                scale.y = 1;
            }
            transform.localScale = scale;

            timer += Time.deltaTime;

            if (Input.GetMouseButtonDown(1)) {
                Attack();
            }
        } else if (timer > swordTime) {
            hasSword = false;
            transform.GetChild(0).gameObject.SetActive(hasSword);
        }
    }

    public void ActivateSword() {
        hasSword = true;
        transform.GetChild(0).gameObject.SetActive(hasSword);
        timer = 0;
    }

    private void Attack() {
        if (!onCooldown) {
            GetComponentInChildren<Animator>().SetTrigger("Attack");
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius)) {
                if (collider.CompareTag("Enemy")) {
                    collider.GetComponent<HealthController>().health -= damage;
                    onCooldown = true;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }

    private IEnumerator Cooldown() {
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }
}
