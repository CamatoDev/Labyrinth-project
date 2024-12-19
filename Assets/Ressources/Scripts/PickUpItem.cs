using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    // La clé 
    public GameObject key;
    //Bouton d'activation 
    public Button pickUp;
    //Distance d'activation 
    public float range = 0.25f;
    //Joueur 
    public Transform player;
    //Le boost a été utilisé
    private bool use;
    //Source audio 
    AudioSource audioSource;
    //Audio clip
    public AudioClip activated;

    // Start is called before the first frame update
    void Start()
    {
        use = false;
        //le bouton n'est pas visible au lancement 
        pickUp.gameObject.SetActive(false);
        //On recupère la source audio
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance entre le boost et le bouton 
        float distanceToKey = Vector3.Distance(transform.position, player.position);
        //On vérifie si la distance est plus petite que la distance d'activation 
        if (distanceToKey <= range && !use)
        {
            //Si c'est le cas on active le bouton pour prendre le boost de vie
            pickUp.gameObject.SetActive(true);
            use = false;
        }
        else
        {
            //On desactive le bouton 
            pickUp.gameObject.SetActive(false);
        }
    }

    // Fonction pour le ramassage de la de la clé
    public void PickUpKey()
    {
        use = true;
        //On lance le son d'activation 
        audioSource.PlayOneShot(activated);
        //On desactive le bouton 
        pickUp.gameObject.SetActive(false);
        player.GetComponent<Player_stats>().haveKey = true;
        Destroy(gameObject, 0.5f);
        key.SetActive(true);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
