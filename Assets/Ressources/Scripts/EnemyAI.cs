using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    // Variable pour la cible
    private Transform target;
    // Variables pour le son 
    private AudioSource audioSource;
    // Pour la gestion des animations 
    private Animator animator;

    // Variables pour g�rer la navigation auto de l'ennemi
    [Header("Ennemy Navigation")]
    private NavMeshAgent agent;
    public FieldOfView fieldOfView;     // Sur la t�te (head) de l'ennemi
    private float Distance;
    private float DistanceToBase;
    private Vector3 basePosition;
    public float patrolingSpeed = 2.0f;
    public float chaseSpeed = 3.0f;
    public float chaseRange = 4.0f;
    public float followDistance = 6.5f;
    bool followPlayer = false;

    //pour g�rer l'attaque de l'ennemi
    [Header("Ennemy Stats")]
    public float enemyHealth;
    private bool isDeath = false;// Port� des attaques
    public float attackRange = 1.3f;
    // Timing des attques 
    public float attackRepeatTime = 2f;
    private float attackTime;
    // Puissance des d�gats
    public float TheDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        // On passe les components dans les variables correspondante
        agent = gameObject.GetComponent<NavMeshAgent>();
        audioSource = gameObject.GetComponent<AudioSource>();
        //fieldOfView = gameObject.GetComponent<FieldOfView>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        basePosition = transform.position;
        attackTime = Time.time;
        followPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            // Calcul de la distance entre l'ennemi et le joueur 
            if (target != null)
            {
                Distance = Vector3.Distance(transform.position, target.position);
            }

            // Calcul de la distance entre l'ennemi et sa position de base 
            DistanceToBase = Vector3.Distance(basePosition, transform.position);

            if (target != null)
            {
                if (!fieldOfView.canSeePlayer && DistanceToBase <= 0.6 && !followPlayer)
                {
                    //On se repose
                    Idle();
                    // On patrouille simplement 
                    //Patroling();
                }

                if (fieldOfView.canSeePlayer && Distance > attackRange)
                {
                    // On pourchase le joueur 
                    Chase();
                }
                if (followPlayer)
                {
                    // On pourchase le joueur 
                    Chase();
                }

                if (fieldOfView.canSeePlayer && Distance <= attackRange)
                {
                    // On attaque le joueur 
                    Attack();
                }

                if (!fieldOfView.canSeePlayer && DistanceToBase > 0.6 && !followPlayer || Player_stats.isDead)
                {
                    // On rentre � la base 
                    BackToBase();
                }

                // On arr�te de poursuivre le jouer si 
                if (Distance >= followDistance)
                {
                    followPlayer = false;
                }
            }
        }
    }

    // Fonction pour le repos 
    public void Idle()
    {
        // Repos sur place 
        agent.destination = transform.position;
        animator.SetFloat("State", 0.0f, 0.2f, Time.deltaTime);
    }

    // Fonction pour la patrouille 
    public void Patroling()
    {
        agent.speed = patrolingSpeed;
        animator.SetFloat("State", 0.5f, 0.4f, Time.deltaTime);
        Debug.Log("Patrouille...");
    }

    // Fonction pour la chase
    public void Chase()
    {
        followPlayer = true;
        agent.speed = chaseSpeed;
        agent.destination = target.position;
        animator.SetFloat("State", 1.0f, 0.2f, Time.deltaTime);
    }

    // Fonction pour l'attaque
    public void Attack()
    {
        // L'ennemi s'arr�te 
        animator.SetFloat("State", 2.0f, 0.2f, Time.deltaTime);
        agent.destination = transform.position;

        //pas de cooldown
        if (Time.time > attackTime && target.GetComponent<Player_stats>().currentHealth > 0)
        {
            audioSource.Play();
            target.GetComponent<Player_stats>().TakeDamage(TheDamage);
            Debug.Log("L'ennemi a envoy� " + TheDamage + " points de d�g�ts");
            attackTime = Time.time + attackRepeatTime;
        }
    }

    // Fonction pour le retour � la base 
    public void BackToBase()
    {
        agent.speed = patrolingSpeed;
        agent.destination = basePosition;
        transform.LookAt(transform.position);
        animator.SetFloat("State", 0.5f, 0.4f, Time.deltaTime);
    }

    //fonction pour appliqu� les d�gats sur l'enemi
    public void ApplyDamage(float TheDamage)
    {
        if (!isDeath)
        {
            //animator.SetFloat("State", 0.0f, 0.2f, Time.deltaTime); //Valeur de coups encaisser au blend tree
            enemyHealth = enemyHealth - TheDamage;
            print(gameObject.name + " � subit " + TheDamage + " points de d�g�ts");

            if (enemyHealth <= 0 && !isDeath)
            {
                animator.SetFloat("State", 0.0f, 0.2f, Time.deltaTime);
                Dead();
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }

    //fonction pour la mort de l'enemi 
    public void Dead()
    {
        isDeath = true;
        //animator.SetTrigger("Die");
        animator.SetFloat("State", 0.0f, 0.2f, Time.deltaTime); //Valeur de mort dans le blend tree
        // Sons de mort.
        Destroy(transform.gameObject, 1);
    }
}

/* Objectifs :
    Fonction pour prendre les d�gats et gestions des points de vie (�limination direct).
 */
