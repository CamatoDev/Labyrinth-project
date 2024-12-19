using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyAI : MonoBehaviour
{
    // Variables pour gérer la navigation auto de l'ennemi
    private NavMeshAgent agent;
    public FieldOfView fieldOfView;     // Sur la tête (head) de l'ennemi
    private float Distance;
    private float DistanceToBase;
    private Vector3 basePosition;
    public float patrolingSpeed = 2.0f;
    public float chaseSpeed = 3.0f;
    public float chaseRange = 4.0f;
    public float followDistance = 6.5f;
    bool followPlayer = false;

    // Porté des attaques
    public float attackRange = 1.3f;

    // Timing des attques 
    public float attackRepeatTime = 2f;
    private float attackTime;

    // Puissance des dégats
    public float TheDamage = 20;

    // Variables pour le son 
    private AudioSource audioSource;

    // Variable pour la cible
    private Transform target;
    // Pour la gestion des animations 
    private Animator animator;

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
        // Calcul de la distance entre l'ennemi et le joueur 
        if(target != null)
        {
            Distance = Vector3.Distance(transform.position, target.position);
        }

        // Calcul de la distance entre l'ennemi et sa position de base 
        DistanceToBase = Vector3.Distance(basePosition, transform.position);

        if (target != null)
        {
            if(!fieldOfView.canSeePlayer && DistanceToBase <= 0.6 && !followPlayer)
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

            if(fieldOfView.canSeePlayer && Distance <= attackRange)
            {
                // On attaque le joueur 
                Attack();
            }

            if(!fieldOfView.canSeePlayer && DistanceToBase > 0.6 && !followPlayer || Player_stats.isDead)
            {
                // On rentre à la base 
                BackToBase();
            }

            // On arrête de poursuivre le jouer si 
            if(Distance >= followDistance)
            {
                followPlayer = false;
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
        // L'ennemi s'arrête 
        animator.SetFloat("State", 2.0f, 0.2f, Time.deltaTime);
        agent.destination = transform.position;

        //pas de cooldown
        if (Time.time > attackTime && target.GetComponent<Player_stats>().currentHealth > 0)
        {
            audioSource.Play();
            target.GetComponent<Player_stats>().TakeDamage(TheDamage);
            Debug.Log("L'ennemi a envoyé " + TheDamage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
    }

    // Fonction pour le retour à la base 
    public void BackToBase()
    {
        agent.speed = patrolingSpeed;
        agent.destination = basePosition;
        transform.LookAt(transform.position);
        animator.SetFloat("State", 0.5f, 0.4f, Time.deltaTime);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}

/* Objectifs :
    
 */
