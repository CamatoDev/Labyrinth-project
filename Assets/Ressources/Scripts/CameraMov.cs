using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    // Variables pour le joueur 
    private Transform player;

    //Variables pour la camera 
    public float cameraOffsetX = 3.0f;
    public float cameraOffsetZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Assignation des variables 
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + cameraOffsetX, transform.position.y, player.position.z + cameraOffsetZ);
    }
}
