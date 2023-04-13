using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.90f;
    public float playerSprint = 3f;

    [Header("Player Health Things")]
    private float playerHealth = 120f;
    public float PresentHealth;
    public HealthBar healthBar;
    public AudioClip playerHeartSound;
    public AudioSource audioSource;
    public GameObject playerUI;
    public GameObject miniMap;
    public GameObject miniMapCam;
    public GameObject playerProp;

    [Header("Player Script Camera")]
    public Transform playerCam;
    public GameObject deathCamera;
    public GameObject endGameMenu;

    [Header("Player Animator & Gravity")]
    public CharacterController characterCntrl;
    public float gravity = -9.81f;
    public Animator animator;
         

    [Header("Player Jumping & Velocity")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public float jumpRange= 1f;
    Vector3 veclocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float SurfaceDistance = 0.4f;
    public LayerMask surfaceMask;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PresentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, SurfaceDistance, surfaceMask);

        if(onSurface && veclocity.y < 0)
        {
            veclocity.y = -2f;
        }
        veclocity.y += gravity * Time.deltaTime;
        characterCntrl.Move(veclocity * Time.deltaTime);

        PlayerMove();

        Jump();

        PlayerSprint();
    }

    void PlayerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if(direction.magnitude >= 0.1f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            characterCntrl.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }



    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Jump", true);
            veclocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Jump", false);
        }
    }

    void PlayerSprint()
    {
        if(Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);


                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                characterCntrl.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Running", false);
            }
        }
    }

    public void PlayerHitDamage(float takeDamage)
    {
        PresentHealth -= takeDamage;
        healthBar.SetHealth(PresentHealth);
        audioSource.PlayOneShot(playerHeartSound);

        if (PresentHealth <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        playerProp.SetActive(false);
        miniMap.SetActive(false);
        miniMapCam.SetActive(false);
        playerUI.SetActive(false);
        endGameMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        deathCamera.SetActive(true);
        Object.Destroy(gameObject);
    }

}
