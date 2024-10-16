using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform[] attackPoints;

    public float attackRange = 0.5f;
    public int damage = 10;

    public Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("AttackingOne");
            animator.SetTrigger("EvilWizardAttacking");
            animator.SetFloat("Speed", 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger("AttackingTwo");
            animator.SetTrigger("EvilWizardAttacking");
            animator.SetFloat("Speed", 0.0f);
        }

        if (CompareTag("MainHero"))
        {
            if(Input.GetKeyDown(KeyCode.F)) 
            {
                animator.SetTrigger("AttackingThree");
                animator.SetFloat("Speed", 0.0f);
            }
        }
    }


    public void PerformAttack(int attackIndex)
    {
        switch (attackIndex)
        {
            case 0:
                Attack(attackPoints[0]);
                Attack(attackPoints[1]);
                break;
            case 1:
                Attack(attackPoints[2]);
                Attack(attackPoints[3]);
                break;
            case 2:
                Attack(attackPoints[4]);
                Attack(attackPoints[5]);
                break;
            default:
                Debug.LogWarning("Invalid attack index: " + attackIndex);
                break;
        }
    }


    void Attack(Transform attackPoint)
    {
        Debug.Log("Attack performed at " + attackPoint.name);

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
        for (int i = 0; i < attackPoints.Length; i++)
        {
            if (attackPoints[i] != null)
            {
                Gizmos.DrawWireSphere(attackPoints[i].position, attackRange);
            }
        }
    }
}
