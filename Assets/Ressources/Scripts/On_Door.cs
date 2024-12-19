using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Door : MonoBehaviour
{
    //Reference au GameManager
    public GameManager manager;
    // Variable des objet d'ouverture de la porte
    float Distance;
    public GameObject doorClosed;
    public Transform partToRotate;
    public float openDistance = 1.3f;

    // Reference au joueur 
    public Transform player;

    // La clé 
    public GameObject key;
    // Indication dans le cas où le joueur arrive devant la sortie sans clé
    public GameObject indication;

    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(transform.position, player.position);

        if(Distance > openDistance)
        {
            // On donne les indication au joueur
            indication.SetActive(false);
        }
        
        if(Distance <= openDistance && !player.GetComponent<Player_stats>().haveKey)
        {
            // On donne les indication au joueur
            indication.SetActive(true);
        }

        if(Distance <= openDistance && player.GetComponent<Player_stats>().haveKey)
        {
            OpenDoor();
        }
    }

    // Ouverture de la porte
    public void OpenDoor()
    {
        key.SetActive(false);
        doorClosed.SetActive(false);
        manager.GetComponent<CompleteLevel>().WinLevel();
        // Lancer l'animation d'ouverture de la porte
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, openDistance);
    }
}
