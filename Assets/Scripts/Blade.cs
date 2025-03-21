using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    private Weapon weaponScr;
    void Start()
    {
        weaponScr = weapon.GetComponent<Weapon>();
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && !weaponScr.enemiesHit.Contains(other.gameObject)) // 8 is the enemy layer. other.gameObject.layer returns an int and not a layermask for some reason.
        {
            EnemyBehaviour enemy = other.GetComponent<EnemyBehaviour>();
            if (enemy != null)
            {
                enemy.TakeDmg(50f);
            }
            weaponScr.enemiesHit.Add(other.gameObject); // adds enemy to list of enemies hit to make sure that the same enemy can't be hit twice from the same attack
        }
    }
}
