using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningMecanism : MonoBehaviour
{
    // Variables pour les objets du mecanisme d'ouverture
    private Transform player;
    private Transform playerHand;
    public GameObject door;
    public GameObject socket;

    // Pour ramasser l'objet 
    public Button pickUpButton;

    // Distance de ramassage de l'objet 
    public float distanceToKey;
    public float distanceToTake = 1f;

    // Si l'objet est utilis� 
    bool use = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assignation des variables 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pickUpButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Calcule de la distance entre le joueur et la cl� 
        distanceToKey = Vector3.Distance(transform.position, player.position);

        // 
        if(!use &&  distanceToKey <= distanceToTake)
        {
            // On prend la cl�
        }
    }
}


// Faire un UI de chiffre qui indique le nombre de cl� poss�d� avec l'image de la cl� � cot� 