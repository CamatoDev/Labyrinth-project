using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player_mov : MonoBehaviour
{
    // Reference au stat du joueur 
    private Player_stats player_Stats;
    //verifie si le joueur est mort
    public bool isDead = false;
    //attaque du personnage
    [Header("Player Attack")]
    public float attackRange;
    private bool isAttacking;
    //Pour la ligne d'attaque 
    public GameObject rayHit;

    // Variables pour le joystick
    [Header("Joystick Settings")]
    public DynamicJoystick joystick;
    //Vitesse de mouvement 
    public float moveSpeed = 10f;

    private Rigidbody rigidbody; 
    private Animator animator;
    private AudioSource audioSource;
    public AudioSource walk;

    public AudioClip punch;
    //public AudioClip walk;

    // Start is called before the first frame update
    void Start()
    {
        // On passe les components dans les variables correspondante
        rigidbody = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        player_Stats = gameObject.GetComponent<Player_stats>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!Player_stats.isDead)
        {
            //Création du déplacement du joueur en fonction du joystick
            rigidbody.velocity = new Vector3(-joystick.Vertical * moveSpeed, rigidbody.velocity.y, joystick.Horizontal * moveSpeed);
            //Verification des valeurs du Joystick  
            float moveAmount = Mathf.Clamp01(Mathf.Abs(joystick.Vertical) + Mathf.Abs(joystick.Horizontal));
            // Gestion de la rotation 
            if (moveAmount != 0)
            {
                transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
            }
                //On met à jour l'animation de déplacement
                animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
        }
        else
        {
            //On met à jour l'animation de mort
            animator.SetFloat("moveAmount", 2, 0.2f, Time.deltaTime);
        }
    }

    //fonction pour l'attaque
    public void Attack()
    {
        if (!isDead)
        {
            animator.SetTrigger("Punch");
            audioSource.PlayOneShot(punch);
            RaycastHit hit;

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                 if (hit.transform.tag == "Enemy")
                 {
                    hit.transform.GetComponent<EnemyAI>().ApplyDamage(player_Stats.damage);
                 }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(rayHit.transform.position, attackRange);
    //}
}



/* Objectifs :
    - Faire des UI qui apparaissent en fonction de la position du joueur pour donner des indication ou activé des mécanismes
    - Faire une fonction d'attaque pour le joueur (The la guardian).
 */