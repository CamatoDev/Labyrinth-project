using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_Door : MonoBehaviour
{
    // Variable des objet d'ouverture de la porte
    public GameObject doorClosed;
    public Transform partToRotate;
    float angle;

    // Reference au joueur 
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        angle = -80f;
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(transform.position, player.position);

        if(Distance <= 1.5f)
        {
            if (player.GetComponent<Player_stats>().haveKey)
            {
                OpenDoor();

                partToRotate.Rotate(0, angle, 0);
            }
        }
    }

    // Ouverture de la porte
    public void OpenDoor()
    {
        doorClosed.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
