using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Boss : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    
    public Transform[] attackPoints;
    public float attackRange = 0.5f;

    public Transform proximityRange;

    private Vector2 movement;
    private Rigidbody2D rb;

    public int damage = 40;
    private bool canMove = true;
    public bool isFlipped;

    public float speed = 2.5f;
    public float pointDifference = 2.3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        LookAtPlayer();
        CheckPlayerProximity();
        MoveTowardsPlayer();
    }


    private void MoveTowardsPlayer()
    {
        Vector2 target = new Vector2(player.position.x, player.position.y + pointDifference);
        float distance = Vector2.Distance(rb.position, target);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        animator.SetFloat("Speed", distance > 0.1f ? speed : 0f); 
    }


    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x && isFlipped)          // Check if the boss is to the right of the player
        {
            FlipSprite();
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)                // Check if the boss is to the left of the player
        {
            FlipSprite();
            isFlipped = true;
        }
    }


    private void FlipSprite()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1;  
        transform.localScale = flipped;
    }

    private void CheckPlayerProximity()
    {
        Collider2D[] nearbyHeroes = Physics2D.OverlapBoxAll(proximityRange.position, proximityRange.localScale, 0f);

        foreach (Collider2D hero in nearbyHeroes)
        {
            if (hero.CompareTag("MainHero") || hero.CompareTag("Player"))
            {
                Debug.Log("Hero nearby: " + hero.name);
                TriggerAttackAnimation();
                continue;
            }
        }
    }

    private void TriggerAttackAnimation()
    {
        animator.SetTrigger("Attacking"); 
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
        Debug.Log("Boss Attack performed at " + attackPoint.name);

        Collider2D[] hitHeroes = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);         // Detect all colliders in the attack range

        foreach (Collider2D hero in hitHeroes)
        {
            if (hero.CompareTag("MainHero") || hero.CompareTag("Player"))
            {
                Debug.Log("Hit Hero: " + hero.name);
            }
        }
    }

    public void StopBossMovement()
    {
        canMove = false; 
        movement = Vector2.zero; 
        rb.velocity = Vector2.zero; 
    }

    public void AllowBossMovement()
    {
        canMove = true; 
    }

    private void StopAllMovement()
    {
        rb.velocity = Vector2.zero; 
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
