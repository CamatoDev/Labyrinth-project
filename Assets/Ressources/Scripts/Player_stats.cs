using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_stats : MonoBehaviour
{
    // Refernce au GameManager
    public GameManager manager;
    // Variables 
    public float currentHealth = 100f;
    private float startHealth = 100f;
    public float damage = 10f;
    public static bool isDead;

    // Si le joueur possède une clé
    public bool haveKey;

    // Barre de vie 
    public Image healthBar;

    //Nombre de temps passé dans le niveau 
    public static float playerTime;

    // Variables pour le son 
    private AudioSource audioSource;
    public AudioClip damageSound;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = startHealth;
        haveKey = false;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void TakeDamage(float damage)
    {
        audioSource.PlayOneShot(damageSound);
        currentHealth -= damage; 
        healthBar.fillAmount = currentHealth / startHealth;


        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    // Fonction pour la mort 
    public void Die()
    {
        isDead = true;
        Destroy(gameObject, 2.0f);
    }
}