using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int damage = 10;

    public Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AttackOne();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AttackTwo();
        }

        if(CompareTag("MainHero"))
        {
            if(Input.GetKeyDown(KeyCode.F)) 
            {
                MainHeroAttack();
            }
        }
    }


    void AttackOne()
    {
        animator.SetTrigger("AttackingOne");
        animator.SetTrigger("EvilWizardAttacking");
        Debug.Log("Attack performed");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);         // Detect all colliders in the attack range

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + enemy.name);
            }
        }
    }

    void AttackTwo()
    {
        animator.SetTrigger("AttackingTwo");
        animator.SetTrigger("EvilWizardAttacking");
        Debug.Log("Attack performed");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);         // Detect all colliders in the attack range

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + enemy.name);
            }
        }
    }

    void MainHeroAttack()
    {
        animator.SetTrigger("AttackingThree");
        Debug.Log("Attack performed");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);         // Detect all colliders in the attack range

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy: " + enemy.name);
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
