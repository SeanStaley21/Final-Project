using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour  // renamed from EnemyAI
{
    public enum State { Patrol, Chase, Attack }

    [Header("Detection")]
    public float sightRange = 15f;
    public float attackRange = 5f;

    [Header("Patrol")]
    public Transform[] patrolPoints;

    [Header("Attack")]
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private Transform player;
    private State state = State.Patrol;
    private int patrolIndex = 0;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= attackRange)
            state = State.Attack;
        else if (distToPlayer <= sightRange)
            state = State.Chase;
        else
            state = State.Patrol;

        switch (state)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(); break;
            case State.Attack: Attack(); break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();
    }

    void GoToNextPatrolPoint()
    {
        agent.destination = patrolPoints[patrolIndex].position;
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    void Chase()
    {
        agent.destination = player.position;
    }

    void Attack()
    {
        agent.destination = transform.position;
        transform.LookAt(player);

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            player.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        }
    }
}