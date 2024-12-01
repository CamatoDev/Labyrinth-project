using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Variables pour gérer la navigation auto de l'ennemi
    private NavMeshAgent agent;
    private float Distance;
    public float patrolingSpeed = 2.0f;
    public float chaseRange = 4.0f;
    public float attackRange = 1.3f;

    // Variable pour la cible
    private Transform target;
    // Pour la gestion des animations 
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // On passe les components dans les variables correspondante
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calcul de la distance entre l'ennemi et le joueur 
        Distance = Vector3.Distance(transform.position, target.position);

        if (target != null)
        {
            if(Distance > chaseRange)
            {
                //On se repose
                //Idle();
                // On patrouille simplement 
                Patroling();
            }

            if (Distance <= chaseRange)
            {
                // On pourchase le joueur 
                Chase();
            }

            if(Distance <= attackRange)
            {
                // On attaque le joueur 
                Attack();
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
        agent.destination = target.position;
        animator.SetFloat("State", 1.0f, 0.2f, Time.deltaTime);
    }

    // Fonction pour l'attaque
    public void Attack()
    {
        // L'ennemi s'arrête 
        agent.destination = transform.position;
        Debug.Log("Attaque...");
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

/* Objectifs :

    - Faire en NavMeshAgent qui ce deplace dans le labyrinthe, cherche le joueur, et l'attaque une fois trouver
    - Faire une zone de detection
    - Potentiellement limité la zone d'action 
 */
