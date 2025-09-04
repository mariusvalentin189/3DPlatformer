using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public enum PlayerState
{
    idle,
    moving,
    jumping,
    attacking,
    dodging,
    falling
}
public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Player State")]
    public PlayerState currentState = PlayerState.idle;
    [Header("Movement")]
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float dodgeSpeed;
    [SerializeField] float dodgeTime;
    [SerializeField] float dodgeCooldown;
    [SerializeField] float speedMultiplyer;
    [SerializeField] CharacterController player;
    [SerializeField] Transform cam;
    [SerializeField] Animator anim;
    [SerializeField] float turnSmoothTime;
    [SerializeField] float afkTime;

    [Header("Jumping")]
    [SerializeField] float jumpForce;
    [SerializeField] float gravityMultiplyer;
    [SerializeField] float airTime;
    [SerializeField] float startFallingAirVelocity;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float maxGroundAngle = 30f;
    [SerializeField] float timeToJumpAfterNotGrounded;
    [SerializeField] GameObject dustParticle;
    [SerializeField] GameObject particlePosLeft;
    [SerializeField] GameObject particlePosRight;
    public bool isGrounded;
    public Vector3 initialPos;

    [Header("Attacking")]
    [SerializeField] BoxCollider weaponCollider;
    [Range(0, 1)]
    [SerializeField] float minAnimTimeNextHit = 0.4f;
    [Range(0, 1)]
    [SerializeField] float maxAnimTimeNextHit = 0.7f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float[] comboMoveSpeeds;
    [SerializeField] HealthManager health;
    public int noOfClicks = 0;

    private Vector3 moveDirection;
    private float speed = 0f, cpyjump;
    private int jumpCounter;
    private float turnSmoothVelocity;  
    private float cpyAirTime;
    private RaycastHit hit;
    private int dirX, dirZ;
    private bool jumped;
    private float groundAngle;
    private Vector3 projected;
    private float dodgeTimer;
    private AudioManager sounds;
    private bool doubleJumpUnlocked, dodgeUnlocked;
    private int jumpOnEnemyX, jumpOnEnemyZ;
    bool canJumpInAir;
    float afkTimer;
    float attackCooldownTimer;
    int idleAnimID;
    float dodgeAngle = 0;

    public bool isInTutorial;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        sounds = AudioManager.instance;
        attackCooldownTimer = attackCooldown;
        weaponCollider = GetComponentInChildren<PlayerSword>().GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        player = GetComponent <CharacterController>();
        initialPos = transform.position;
        cpyAirTime = airTime;
        if (!isInTutorial)
            LoadAdditionalStats();
        else UnlockStats();
    }
    void Update()
    {
        if (PauseMenu.instance.isPaused)
            return;
        CheckGround();
        CalculateGroundAngle();
        GetInput();
        Attack();
        Move();
        Jump();
        ApplyGravity();
    }
    void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position, new Vector3(player.radius - 0.05f, player.radius, player.radius - 0.05f), Vector3.down, out hit, Quaternion.identity, groundCheckDistance,whatIsGround))
            if (hit.collider.CompareTag("MovingPlatform"))
            {
                player.Move(hit.collider.GetComponent<Rigidbody>().linearVelocity * Time.fixedDeltaTime);
                sounds.ChangeStepSound(2);
                isGrounded = true;
            }
    }
    void GetInput()
    {
        cpyjump = moveDirection.y;
        moveDirection = new Vector3(dirX, moveDirection.y, dirZ);
        moveDirection = moveDirection.normalized;
        moveDirection.y = cpyjump;

        if (Input.GetKey(PlayerInput.upKey))
            dirZ = 1;
        else if (Input.GetKey(PlayerInput.downKey))
            dirZ = -1;
        else dirZ = 0;

        if (Input.GetKey(PlayerInput.rightKey))
            dirX = 1;
        else if (Input.GetKey(PlayerInput.leftKey))
            dirX = -1;
        else dirX = 0;
    }
    void CalculateGroundAngle()
    {
        if(!isGrounded)
        {
            groundAngle = 0f;
            return;
        }
        groundAngle = Vector3.Angle(hit.normal, Vector3.up);
    }
    void CheckGround()
    {
        if (Physics.BoxCast(transform.position, new Vector3(player.radius-0.05f, player.radius, player.radius-0.05f), Vector3.down, out hit, Quaternion.identity, groundCheckDistance,whatIsGround))
        {
            projected = Vector3.Cross(hit.normal, -transform.right).normalized;
            jumpOnEnemyX = 0;
            jumpOnEnemyZ = 0;
            if (hit.collider.CompareTag("Enemy") && !isGrounded)
            {
                jumped = false;
                jumpOnEnemyX = UnityEngine.Random.Range(-2, 3);
                jumpOnEnemyZ = UnityEngine.Random.Range(-2, 3);
                moveDirection.y = jumpForce;
                sounds.PlayJumpOnEnemySound();
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(1);
            }
            else if (hit.collider.CompareTag("RotatingPlatform"))
            {
                sounds.ChangeStepSound(2);
                float rotationSpeed = hit.collider.GetComponent<RotatingPlatform>().rotationSpeed;
                Vector3 curPos = transform.position - hit.collider.transform.position;
                Vector3 newPos = Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f) * curPos;
                Vector3 finalPos = newPos - curPos;
                player.Move(finalPos);
                transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
                isGrounded = true;
            }
            else if (hit.collider.CompareTag("JumpPad"))
            {
                sounds.ChangeStepSound(2);
                sounds.PlayTrampolineSound();
                anim.SetBool("Fall", true);
                jumped = false;
                JumpPad jumpPad = hit.collider.GetComponent<JumpPad>();
                moveDirection.y = jumpPad.forceMultiplier * Math.Abs(moveDirection.y);
                if (moveDirection.y > jumpPad.maxForce)
                    moveDirection.y = jumpPad.maxForce;
            }
            else if (hit.collider.CompareTag("ShrinkingPlatform"))
            {
                sounds.ChangeStepSound(2);
                hit.collider.GetComponent<ShrinkingPlatform>().triggered = true;
                isGrounded = true;
            }
            else if (hit.collider.CompareTag("DisappearingPlatform"))
            {
                sounds.ChangeStepSound(2);
                hit.collider.GetComponent<DisappearingPlatform>().disappear = true;
                isGrounded = true;
            }
            else if (hit.collider.CompareTag("StandingPlatform"))
            {
                sounds.ChangeStepSound(2);
                isGrounded = true;
            }
            else if(hit.collider.CompareTag("Ground"))
            {
                sounds.ChangeStepSound(1);
                isGrounded = true;
            }
            else
            {
                sounds.ChangeStepSound(0);
                isGrounded = true;
            }

        }
        else
        {
            if (!canJumpInAir && isGrounded && !jumped)
            {
                canJumpInAir = true;
                StartCoroutine(JumpAfterNotGrounded());
            }
            isGrounded = false;
            projected = Vector3.zero;
        }
    }    
    void Move()
    {
        if (isGrounded)
        {
            if (CanMove() || IsInAir())
            {
                if (speed == 0)
                    currentState = PlayerState.idle;
                else currentState = PlayerState.moving;
            }
            if (Input.GetKeyDown(PlayerInput.dodgeKey) &&(CanMove() || currentState == PlayerState.attacking) && dodgeTimer <= 0f && dodgeUnlocked)
            {
                if (idleAnimID != 0)
                {
                    EndIdle();
                }
                if (currentState == PlayerState.attacking)
                    EndAttacking();
                transform.rotation = Quaternion.Euler(0f, dodgeAngle, 0f);
                currentState = PlayerState.dodging;
                dodgeTimer = dodgeCooldown;
                health.Invulnerable = true;
                anim.SetTrigger("Dodge");
                sounds.PlayJumpSound();
            }
            if (CanMove())
            {
                float blend = speed / runSpeed;
                anim.SetFloat("Blend", blend, 0.05f, Time.deltaTime);
                if (Input.GetKey(PlayerInput.leftKey) || Input.GetKey(PlayerInput.rightKey) ||
                    Input.GetKey(PlayerInput.upKey) || Input.GetKey(PlayerInput.downKey))
                {
                    if (idleAnimID != 0)
                    {
                        EndIdle();
                    }
                    if (speed < runSpeed)
                    {
                        speed += speedMultiplyer * Time.deltaTime;
                    }
                    if (speed > runSpeed)
                        speed = runSpeed;
                    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    dodgeAngle = angle;
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    moveDir = moveDir.normalized;
                    if (groundAngle <= maxGroundAngle)
                    {
                        player.Move(new Vector3(moveDir.x * speed, projected.y * speed, moveDir.z * speed) * Time.deltaTime);
                    }

                }
                else
                {
                    speed = 0;
                    if (idleAnimID == 0 && currentState == PlayerState.idle)
                    {
                        afkTimer += Time.deltaTime;
                        if (afkTimer >= afkTime)
                        {
                            afkTimer = afkTime;
                            idleAnimID = 1;
                            anim.SetInteger("IdleID", idleAnimID);
                        }
                    }
                }
            }
            else if (currentState == PlayerState.dodging)
            {
                player.Move(transform.forward * dodgeSpeed * Time.deltaTime);
            }
            else if (currentState == PlayerState.attacking)
            {
                if (Input.GetKey(PlayerInput.leftKey) || Input.GetKey(PlayerInput.rightKey) ||
                    Input.GetKey(PlayerInput.upKey) || Input.GetKey(PlayerInput.downKey))
                {
                    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    dodgeAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                }
                player.Move(transform.forward * comboMoveSpeeds[noOfClicks - 1] * Time.deltaTime);
            }
        }
        else if(IsInAir() || (CanMove() && !isGrounded))
        {
            if (Input.GetKey(PlayerInput.leftKey) || Input.GetKey(PlayerInput.rightKey) ||
                    Input.GetKey(PlayerInput.upKey) || Input.GetKey(PlayerInput.downKey))
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDir = moveDir.normalized;
                if (groundAngle <= maxGroundAngle)
                {
                    player.Move(new Vector3(moveDir.x * speed, projected.y * speed, moveDir.z * speed) * Time.deltaTime);
                }
            }
        }
        if (dodgeTimer >= 0f)
            dodgeTimer -= Time.deltaTime;
        else dodgeTimer = 0f;
    }
    void Jump()
    {
        anim.SetBool("OnGround", isGrounded);
        if (moveDirection.y <= -startFallingAirVelocity && !isGrounded)
        {
            anim.SetBool("Fall", true);
            currentState = PlayerState.falling;
        }
        else anim.SetBool("Fall", false);
        if ((isGrounded || canJumpInAir) && CanMove())
        {
            jumped = false;
            anim.ResetTrigger("Jump");
            moveDirection.y = 0f;
            jumpCounter = 0;
            if (cpyAirTime <= 0)
            {
                anim.SetBool("Land", true);
                currentState = PlayerState.idle;
            }
            if (Input.GetKeyDown(PlayerInput.jumpKey))
            {
                currentState = PlayerState.jumping;
                if (idleAnimID != 0)
                {
                    EndIdle();
                }
                canJumpInAir = false;
                jumpCounter++;
                moveDirection.y = jumpForce;
                anim.SetTrigger("Jump");
                sounds.PlayJumpSound();
                jumped = true;
            }
            cpyAirTime = airTime;
        }
        else if (jumpCounter == 1 && !jumped && !isGrounded && doubleJumpUnlocked)
        {
            if (Input.GetKeyDown(PlayerInput.jumpKey))
            {
                currentState = PlayerState.jumping;
                if (idleAnimID != 0)
                {
                    EndIdle();
                }
                moveDirection.y = jumpForce;
                sounds.PlayJumpSound();
                cpyAirTime = airTime;
                jumpCounter++;
            }
        }
        else
        {
            cpyAirTime -= Time.deltaTime;
            anim.SetBool("Land", false);
        }

        if (Input.GetKeyUp(PlayerInput.jumpKey) && jumped == true && moveDirection.y > 0 && !isGrounded)
        {
            moveDirection.y = jumpForce / 3f;
            jumped = false;
        }
    }
    void ApplyGravity()
    {
        if (!isGrounded)
            if (moveDirection.y > Physics.gravity.y * gravityMultiplyer)
                moveDirection.y += Physics.gravity.y * gravityMultiplyer * Time.deltaTime;
            else moveDirection.y = Physics.gravity.y * gravityMultiplyer;
        player.Move(new Vector3(jumpOnEnemyX, moveDirection.y, jumpOnEnemyZ) * Time.deltaTime);
    }
    void LoadAdditionalStats()
    {
        int v = 0;
        if (PlayerPrefs.HasKey("JumpUnlocked"))
            v = PlayerPrefs.GetInt("JumpUnlocked");
        if (v == 0)
            doubleJumpUnlocked = true; //Test
        else doubleJumpUnlocked = true;
        v = 0;
        if (PlayerPrefs.HasKey("DodgeUnlocked"))
            v = PlayerPrefs.GetInt("DodgeUnlocked");
        if (v == 0)
            dodgeUnlocked = true; //Test
        else dodgeUnlocked = true;

    }

    void Attack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > minAnimTimeNextHit && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false); 
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > minAnimTimeNextHit && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > minAnimTimeNextHit && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
        }
        if (attackCooldownTimer >= attackCooldown)
        {
            // Check for mouse input
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
        else
        {
            attackCooldownTimer += Time.deltaTime;
        }
    }
    void OnClick()
    {
        if (noOfClicks == 0 && anim.GetBool("hit3") == false && CanMove())
        {
            if (idleAnimID != 0)
            {
                EndIdle();
            }
            anim.SetBool("hit1", true);
            currentState = PlayerState.attacking;
            noOfClicks++;
        }

        else if (noOfClicks == 1 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= minAnimTimeNextHit 
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= maxAnimTimeNextHit && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
            currentState = PlayerState.attacking;
            noOfClicks++;
        }
        else if (noOfClicks == 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > minAnimTimeNextHit
            && anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= maxAnimTimeNextHit && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
            currentState = PlayerState.attacking;
            noOfClicks++;
        }
    }


    #region Action Event Functions
    public void PlayJumpSound()
    {
        sounds.PlayJumpSound();
    }
    public void PlayStepSound()
    {
        if (isGrounded)
            sounds.PlayStepSound();
    }
    public void EnableSwordColldier()
    {
        weaponCollider.enabled = true;
    }
    public void DisableSwordColldier()
    {
        weaponCollider.enabled = false;
    }
    public void EndAttacking()
    {
        attackCooldownTimer = 0;
        currentState = PlayerState.idle;
        noOfClicks = 0;
        anim.SetBool("hit1", false);
        anim.SetBool("hit2", false);
        anim.SetBool("hit3", false);
    }
    public void EndIdle()
    {
        idleAnimID = 0;
        afkTimer = 0;
        noOfClicks = 0;
        anim.SetInteger("IdleID", idleAnimID);
    }
    public void EndDodge()
    {
        EndIdle();
        health.Invulnerable = false;
        currentState = PlayerState.idle;
    }
    #endregion

    public void UnlockStats()
    {
        doubleJumpUnlocked = true;
        dodgeUnlocked = true;
    }
    public void SpawnDustParticleLeft()
    {
        if(isGrounded)
            Instantiate(dustParticle, particlePosLeft.transform.position, Quaternion.identity);
    }
    public void SpawnDustParticleRight()
    {
        if (isGrounded)
            Instantiate(dustParticle, particlePosRight.transform.position, Quaternion.identity);
    }

    IEnumerator JumpAfterNotGrounded()
    {
        yield return new WaitForSeconds(timeToJumpAfterNotGrounded);
        canJumpInAir = false;
    }
    bool CanMove()
    {
        return currentState == PlayerState.idle || currentState == PlayerState.moving;
    }
    bool IsInAir()
    {
        return currentState == PlayerState.jumping || currentState == PlayerState.falling;
    }
}
