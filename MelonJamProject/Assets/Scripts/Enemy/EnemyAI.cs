using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //TODO: Enemy AI for different enemy types
    [SerializeField] private float radarRadius = 3;
    [SerializeField] private float idleRadius = 3;
    [SerializeField] private float damage = 1;
    [SerializeField] private float attackCooldown = 1;
    private Room linkedRoom;
    private Transform player;
    private NavMeshAgent agent;
    private bool chasing = false;
    private float timer = 0;
    private bool attacking = false;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GetComponent<HealthController>().InitScript(gameObject);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        transform.eulerAngles = Vector3.zero;
    }

    private void Update() {
        if (!chasing && !agent.hasPath) {
            StartCoroutine(Idle());
        }

        if (CheckForPlayer()) {
            agent.SetDestination(player.position);
        } else if (chasing) {
            timer += Time.deltaTime;
            if (timer > 3) {
                chasing = false;
                timer = 0;
            }
        }

        if (agent.velocity.magnitude > 0) {
            spriteRenderer.flipX = agent.velocity.x < 0;
        }

        anim.SetFloat("Speed", agent.velocity.sqrMagnitude);

        transform.GetChild(0).gameObject.SetActive(linkedRoom.playerInRoom);
    }

    private IEnumerator Idle() {
        yield return new WaitForSeconds(5);
        Vector3 positionToIdle = Random.insideUnitCircle*idleRadius;
        positionToIdle = new Vector3(Mathf.Clamp(positionToIdle.x, -linkedRoom.sizes.x, linkedRoom.sizes.x), 
                                    Mathf.Clamp(positionToIdle.y, -linkedRoom.sizes.y, linkedRoom.sizes.y),
                                    0);
        agent.SetDestination(positionToIdle+transform.position);
    }

    private bool CheckForPlayer() {
        Vector3 dir = player.transform.position-transform.position;
        if (dir.magnitude < radarRadius && linkedRoom.playerInRoom) {
            if (Physics2D.Raycast(transform.position, dir.normalized, radarRadius, 7)) {
                chasing = true;
                return true;
            }
        } 
        return false;
    }

    public void Die() {
        ScoreCounter.instance.AddScore(10);
        LevelController.instance.EnemyDied();
        
        attacking = true;
        chasing = false;
        StopAllCoroutines();

        anim.enabled = false;
        linkedRoom.EnemyKilled();

        agent.velocity = Vector2.zero;
        // Animation of dying
        Destroy(gameObject, 2);
        enabled = false;
    }

    public void SetRoom(Room room) {
        linkedRoom = room;
    }

    private IEnumerator Attack(HealthController healthController) {
        if (!attacking) { // and is playing
            attacking = true;
            
            // Animation of attacking
            healthController.GetDamage(damage);
            yield return new WaitForSeconds(attackCooldown);

            attacking = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.transform.CompareTag("Player")) {
            StartCoroutine(Attack(other.transform.GetComponent<HealthController>()));
        }
    }

    public IEnumerator FreezeEnemy(float freezTime)
    {
        attacking = true;
        float enemySpeed = agent.speed;
        anim.enabled = false;
        agent.speed = 0;
        yield return new WaitForSeconds(freezTime);
        anim.enabled = true;
        agent.speed = enemySpeed;
        attacking = false;
    }

    public void StartFreezeEnemy(float freeztime)
    {
        StartCoroutine(FreezeEnemy(freeztime));
    }
}
