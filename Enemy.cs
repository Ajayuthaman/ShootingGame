using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health & Damage")]
    public float enemyHealth =120f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar healthBar; 
        


    [Header("Enemy Things")]
    public Transform playerBody;
    public Transform lookPoint;
    public Camera shootingRaycastArea; 
    public NavMeshAgent enemyAgent;
    public LayerMask playerLayer;

    [Header("Enemy Guarding war")]
    public GameObject[] walkPoints;
    int currentEnemyPosition = 0;
    public float enemySpeed;
    float walkingPointRadious = 2;


    [Header("Sounds & UI")]
    public AudioClip shootingSound;
    public AudioSource audioSource;

    [Header("Enemy Shooting var")]
    public float timeBtwShoot;
    bool previouslyShoot;


    [Header("Enemy Animation & Spark effect")]
    public Animator anim;
    public ParticleSystem muzzleSpark;


    [Header("Enemy Mood/Situation")]
    public float visionRadious;
    public float shootingRadious;
    public bool playerInVisionRadious;
    public bool playerInShootingRadious;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        presentHealth = enemyHealth;
        healthBar.GiveFullHealth(enemyHealth);
        playerBody = GameObject.Find("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInVisionRadious = Physics.CheckSphere(transform.position, visionRadious, playerLayer);
        playerInShootingRadious = Physics.CheckSphere(transform.position, shootingRadious, playerLayer);

        if(!playerInShootingRadious && !playerInVisionRadious)
        {
            Guard();
        }

        if(playerInVisionRadious && !playerInShootingRadious)
        {
            PursuePlayer();
        }

        if(playerInShootingRadious && playerInVisionRadious)
        {
            ShootPlayer();
        }
    }

    private void Guard()
    {
        if (Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingPointRadious)
        {
            currentEnemyPosition = Random.Range(0, walkPoints.Length);

            if(currentEnemyPosition >= walkPoints.Length)
            {
                currentEnemyPosition = 0;

            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);
     
        //changing enemy face
        transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
    }

    private void PursuePlayer()
    {
        if (enemyAgent.SetDestination(playerBody.position))
        {
            //animation
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", true);
            anim.SetBool("Fire", false);

            //increase vision & shooting Radious
            visionRadious = 30f;
            shootingRadious = 16f;
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Fire", false);

            GetComponent<RagDoll>().DoRagDoll(true);
        }
    }

    private void ShootPlayer()
    {
        enemyAgent.SetDestination(transform.position);

        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            muzzleSpark.Play();
            audioSource.PlayOneShot(shootingSound);

            RaycastHit hit;

            if(Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit, shootingRadious))
            {
                Debug.Log("Shooting" + hit.transform.name);

                PlayerScript playerHealth = hit.transform.GetComponent<PlayerScript>();

                if(playerHealth != null)
                {
                    playerHealth.PlayerHitDamage(giveDamage);
                }

                anim.SetBool("Fire", true);
                anim.SetBool("Walk", false);
                anim.SetBool("AimRun", false);
            }

            previouslyShoot = true;
            Invoke(nameof(ActiveShooting), timeBtwShoot);
        }
    }

    private void ActiveShooting()
    {
        previouslyShoot = false;
    }

    public void EnemyHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);

        //increase vision & shooting Radious
        visionRadious = 40f;
        shootingRadious = 19f;

        if (presentHealth <= 0)
        {
            GetComponent<RagDoll>().DoRagDoll(true);
            EnemyDie();
        }
    }
    private void EnemyDie()
    {
        //enemyAgent.SetDestination(transform.position);
        enemySpeed = 0f;
        shootingRadious = 0f;
        visionRadious = 0f;
        playerInShootingRadious = false;
        playerInVisionRadious = false;

        Object.Destroy(gameObject, 5f);
    }
}
