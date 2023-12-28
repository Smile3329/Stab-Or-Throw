using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float enemyRadarRadius = 3;
    [SerializeField] private float enemyIdleRadius = 3;
    private Transform player;
    private NavMeshAgent agent;
    private bool chasing = false;
    private float timer = 0;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
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
    }

    private IEnumerator Idle() {
        yield return new WaitForSeconds(5);
        Vector3 positionToIdle = Random.insideUnitCircle*enemyIdleRadius;
        agent.SetDestination(positionToIdle+transform.position);
    }

    private bool CheckForPlayer() {
        Vector3 dir = player.transform.position-transform.position;
        if (dir.magnitude < enemyRadarRadius) {
            print('g');
            if (Physics2D.Raycast(transform.position, dir.normalized, enemyRadarRadius, 7)) {
                chasing = true;
                return true;
            }
        } 
        return false;
    }
}
