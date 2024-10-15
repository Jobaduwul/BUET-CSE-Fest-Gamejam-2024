using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int damage = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attack();
        }
    }

    // Basic attack functionality
    void Attack()
    {
        Debug.Log("Attack performed");

        // Detect all colliders in the attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // Loop through the colliders and check if they have the "Enemy" tag
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + enemy.name);

                // Call a damage function on the enemy (assuming the enemy has a TakeDamage() method)
                // Example:
                // enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    // To visualize the attack range in the Scene view (optional)
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
