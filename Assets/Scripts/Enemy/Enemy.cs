using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
public class Enemy : MonoBehaviour
{
    [Header("AI")]
    [SerializeField] protected NavMeshAgent enemy;
    [SerializeField] float detectRange;
    [SerializeField] float attackRange;
    [SerializeField] float chanseMaxRange;
    [SerializeField] float attackCooldown;
    [SerializeField] float standStillTime;
    [SerializeField] float knockbackTime;
    [SerializeField] float walkRadius;
    [SerializeField] float timeBetweenDestinations;
    [SerializeField] float walkSpeed, chaseSpeed;
    [SerializeField] Animator anim;
    [SerializeField] float rotationSpeed;
    [SerializeField] float chaseRotationSpeed;

    [Header("Damage")]
    [SerializeField] int maxHearts;
    [SerializeField] int coinsDropAmmount;
    [SerializeField] float coinsDropLocationOffsetY;
    [SerializeField] BoxCollider takeDamgeCollider;
    [SerializeField] BoxCollider damagePlayerCollider;
    [SerializeField] SkinnedMeshRenderer model;
    [SerializeField] Material[] takeDamageMaterials;
    [SerializeField] float knockbackMultiplier;
    Material[] defaultModelMaterials;

    Transform target;
    Vector3 initialPosition;
    float destTime;
    bool canMove = true;
    bool isDead = false;
    bool isAttacking = false;
    float currentAttackCooldown;
    GameManager gameManager;
    bool invulnerable = false;
    bool returningToStartPosition = false;
    int hearts;
    protected float currentSpeed;
    protected float currentRotationSpeed = 0;
    protected void Start()
    {
        currentAttackCooldown = 0;
        defaultModelMaterials = model.materials;
        gameManager = GameManager.instance;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        destTime = timeBetweenDestinations;
        hearts = maxHearts;
        enemy.updateRotation = false;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }
    protected void Move()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float positionDistance = Vector3.Distance(transform.position, initialPosition);
        if (canMove)
        {
            enemy.isStopped = false;
            if (returningToStartPosition)
            {
                anim.SetTrigger("Walk");
                anim.ResetTrigger("Idle");
                destTime = timeBetweenDestinations;
                currentRotationSpeed = rotationSpeed;
                enemy.SetDestination(initialPosition);
                currentSpeed = walkSpeed;
            }
            if (distance < attackRange && !returningToStartPosition)
            {
                if (enemy.speed != 0)
                {
                    anim.ResetTrigger("Chase");
                    anim.SetTrigger("Idle");
                    enemy.speed = 0;
                }
                if (!isAttacking && !invulnerable)
                {
                    if (currentAttackCooldown >= attackCooldown)
                    {
                        anim.SetTrigger("Attack");
                        isAttacking = true;
                    }
                    else if (currentAttackCooldown >= attackCooldown / 2)
                    {
                        currentAttackCooldown += Time.deltaTime;
                    }
                    else
                    {

                        currentAttackCooldown += Time.deltaTime;
                    }
                }
            }
            else if (distance < detectRange)
            {
                if (positionDistance <= chanseMaxRange && !returningToStartPosition)
                {
                    anim.SetTrigger("Chase");
                    currentRotationSpeed = chaseRotationSpeed;
                    enemy.SetDestination(target.position);
                    currentSpeed = chaseSpeed;
                }

                else
                {
                    if (enemy.remainingDistance <= enemy.stoppingDistance)
                    {
                        returningToStartPosition = false;
                        invulnerable = false;
                    }
                    else
                    {
                        returningToStartPosition = true;
                        invulnerable = true;
                    }
                }
                currentAttackCooldown = 0;
            }
            else if (enemy.remainingDistance <= enemy.stoppingDistance)
            {
                currentAttackCooldown = 0;
                if (destTime <= 0f)
                {
                    anim.SetTrigger("Walk");
                    anim.ResetTrigger("Idle");
                    currentRotationSpeed = rotationSpeed;
                    destTime = timeBetweenDestinations;

                    enemy.SetDestination(RandomNavMeshLocation());
                    currentSpeed = walkSpeed;
                }
                else
                {
                    if (returningToStartPosition)
                    {
                        returningToStartPosition = false;
                        invulnerable = false;
                        hearts = maxHearts;

                    }
                    anim.SetTrigger("Idle");
                    anim.ResetTrigger("Walk");
                    destTime -= Time.deltaTime;
                }

            }
            SetMovementSpeed();
        }
    }
    protected virtual void SetMovementSpeed()
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 directionToDestination = (enemy.destination - transform.position).normalized;
        float dotProduct = Vector3.Dot(forwardDirection, directionToDestination);
        if (dotProduct >= 0.9f)
            enemy.speed = currentSpeed;
        else enemy.speed = 0.2f;

        if (directionToDestination != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionToDestination), currentRotationSpeed * Time.deltaTime);
    }
    
    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            invulnerable = true;
            model.materials = takeDamageMaterials;
            enemy.isStopped = true;
            enemy.speed = 0;
            hearts -= damage;
            canMove = false;
            if (hearts <= 0)
                isDead = true;
            anim.SetTrigger("TakeDamage");

            StartCoroutine(ApplyKnockback());
        }
    }
    private IEnumerator ApplyKnockback()
    {
        // Calculate direction away from the player (damage source)
        Vector3 knockbackDirection = transform.position - target.position;
        knockbackDirection.y = 0; // Keep the knockback horizontal

        // Normalize the direction to ensure consistent knockback force
        knockbackDirection.Normalize();

        float timeElapsed = 0f;

        // Gradually move the enemy using Lerp to simulate smooth knockback
        while (timeElapsed < 0.2f)
        {
            // Move the agent towards the knockback direction
            enemy.Move(knockbackDirection * knockbackMultiplier * Time.deltaTime);

            // Update elapsed time
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Once knockback is done, allow the enemy to move again
        yield return new WaitForSeconds(0.1f);
        canMove = true;
        enemy.isStopped = false;
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += initialPosition;
        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    private IEnumerator Die()
    {
        anim.SetTrigger("Die");
        takeDamgeCollider.enabled = false;
        damagePlayerCollider.enabled = false;
        isDead = true;
        yield return new WaitForSeconds(2f);
        gameManager.SpawnCoinsAmmount(coinsDropAmmount, new Vector3(transform.position.x, transform.position.y + coinsDropLocationOffsetY, transform.position.z));
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    #region Anmator triggers
    public void StartAttack()
    {
        damagePlayerCollider.enabled = true;
        enemy.speed = 0;
        canMove = false;
    }
    public void EndAttack()
    {
        damagePlayerCollider.enabled = false;
        currentAttackCooldown = 0;
        isAttacking = false;
        canMove = true;
    }
    public void EndTakeDamage()
    {
        model.materials = defaultModelMaterials;
        if (isDead)
            StartCoroutine(Die());
        else
        {
            invulnerable = false;
            canMove = true;
        }
    }
    #endregion
}
