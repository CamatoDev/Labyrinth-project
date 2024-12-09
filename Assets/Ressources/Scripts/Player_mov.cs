using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player_mov : MonoBehaviour
{
    // Reference au stat du joueur 
    private Player_stats player_Stats;
    // Variables pour le joystick
    public DynamicJoystick joystick;
    //Vitesse de mouvement 
    public float moveSpeed = 10f;

    private Rigidbody rigidbody; 
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // On passe les components dans les variables correspondante
        rigidbody = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        player_Stats = gameObject.GetComponent<Player_stats>();
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
}



/* Objectifs :
    - Faire des UI qui apparaissent en fonction de la position du joueur pour donner des indication ou activé des mécanismes
 */ 
