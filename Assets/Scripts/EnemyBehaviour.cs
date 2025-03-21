using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public float health;
    private bool immunity;
    void Start()
    {
        immunity = false;
        health = 200f;
    }
    
    void Update()
    {
        
    }
    public void TakeDmg(float dmg)
    {
        health -= dmg;
        if (health <= 0f)
        {
            Die();
        }
        
    }
    
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
