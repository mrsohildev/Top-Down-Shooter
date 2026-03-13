using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    int patrolIndex = 0;

    [Header("Speeds")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    [Header("Combat Settings")]
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float fireRate = 0.7f;
    float nextFireTime = 0f;

    [Header("Shooting")]
    public Transform origin;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    NavMeshAgent agent;
    Transform player;
    Animator anim;

    enum State { Patrol, Chase, Attack }
    State currentState = State.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.speed = patrolSpeed;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol(distance);
                break;

            case State.Chase:
                Chase(distance);
                break;

            case State.Attack:
                Attack(distance);
                break;
        }
    }

    // ---------------- STATES ----------------

    void Patrol(float distance)
    {
        agent.speed = patrolSpeed;

        anim.SetBool("IsWalking", true);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsAttacking", false);

        if (distance <= chaseRange)
        {
            currentState = State.Chase;
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= 0.5f)
            GoToNextPatrolPoint();
    }

    void Chase(float distance)
    {
        agent.speed = chaseSpeed;

        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", true);
        anim.SetBool("IsAttacking", false);

        if (distance > chaseRange)
        {
            currentState = State.Patrol;
            GoToNextPatrolPoint();
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (distance <= attackRange)
            currentState = State.Attack;
    }

    void Attack(float distance)
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsAttacking", true);

        if (distance > attackRange)
        {
            currentState = State.Chase;
            agent.isStopped = false;
            anim.SetBool("IsAttacking", false);
            return;
        }

        agent.isStopped = true;

        FacePlayer();
        Shoot();
    }

    // ---------------- HELPERS ----------------

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.isStopped = false;
        agent.SetDestination(patrolPoints[patrolIndex].position);

        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    void FacePlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 6f);
        }
    }

    void Shoot()
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        rb.linearVelocity = origin.forward * bulletSpeed;

        Destroy(bullet, 15f*Time.deltaTime);
    }
}
