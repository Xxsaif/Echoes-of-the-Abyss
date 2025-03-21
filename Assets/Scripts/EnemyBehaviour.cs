using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public float health;
    [SerializeField] private TextMeshPro healthText; // Temporary health text, should be replaced with health bar

    [SerializeField] private NavMeshAgent agent;
    private float moveSpeed = 5f;

    [SerializeField] private GameObject player;
    private Vector3 playerPosition;
    private EnemyStates state;

    private bool attacking;
    private float damage = 15f;

    void Start()
    {
        health = 200f;
        playerPosition = Vector3.zero;

        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.SetDestination(Vector3.zero);

        state = EnemyStates.Chasing;
        attacking = false;
    }
    
    void Update()
    {
        playerPosition = player.transform.position;
        if (agent.destination != playerPosition)
        {
            agent.SetDestination(playerPosition);
        }
        transform.LookAt(new Vector3(playerPosition.x, transform.position.y, playerPosition.z));

        if (Vector3.Distance(transform.position, playerPosition) <= agent.stoppingDistance)
        {
            Stop();
            state = EnemyStates.Attacking;
        }
        else if (agent.isStopped == true)
        {
            Move();
            state = EnemyStates.Chasing;
            CancelInvoke();
            attacking = false;
        }

        if (state == EnemyStates.Attacking)
        {
            if (!attacking)
            {
                Invoke(nameof(Attack), 0.65f);
                attacking = true;
            }
        }
    }
    public void TakeDmg(float dmg)
    {
        health -= dmg;
        healthText.text = health.ToString() + "hp";
        if (health <= 0f)
        {
            Die();
        }
    }
    
    private void Die()
    {
        //gameObject.SetActive(false);
        // temporarily turned of dying for the sake of testing
        health = 200f;
        healthText.text = health.ToString() + "hp";
    }

    private void Move()
    {
        agent.isStopped = false;
        agent.speed = moveSpeed;
    }
    private void Stop()
    {
        agent.isStopped = true;
        agent.speed = 0;
    }

    private void Attack()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        attacking = false;
    }

    private enum EnemyStates
    {
        Chasing,
        Attacking
    }
   
}
