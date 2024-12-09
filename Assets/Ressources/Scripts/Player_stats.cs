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
    public bool haveKey = false;
    public Image keyImage;

    // Barre de vie 
    public Image healthBar;

    //Nombre de temps passé dans le niveau 
    public static float playerTime;
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = startHealth;
        haveKey = false;
        //Le temps du que le joueur dois passer dans le niveau est défini  
        playerTime = manager.Leveltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //Le temps que le joueur passe est incrémenté à chaque seconde 
            playerTime -= Time.deltaTime;
            //On bloque la valeur du temps à 0
            if (playerTime <= 0f)
                playerTime = 0f;
            //on actualise le temps passé dans le niveau 
            timer.text = string.Format("{0:00.00}", playerTime);
        }
    }

    public void TakeDamage(float damage)
    {
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


// Variable haveKey qui fais apparaitre et disparaite une image de clé quand le joueur en à une
// (ou qui ecrit un numéro indicant son nombre de clé)