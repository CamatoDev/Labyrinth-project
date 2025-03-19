using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CompleteLevel : MonoBehaviour
{
    // Reference au joueur 
    public Transform player;
    // La clé 
    public GameObject key;
    //Source audio pour les boutons
    public AudioSource buttons;

    public string levelToLoad;
    //Reférence à scene fader 
    [Header("UI")]
    public SceneFader sceneFader;
    // Indication dans le cas où le joueur arrive devant la sortie sans clé
    public GameObject indication;

    //menu de victoire 
    [Header("Win Menu")]
    public GameObject levelWin;
    public Text winMessage;
    public string EndLabyrinthMessage;
    //Source audio pour la victoire
    public AudioSource winLevelSound;

    // Variable des objet d'ouverture de la porte
    float Distance;
    [Header("Open Door Variable")]
    public GameObject door;
    public GameObject doorClosed;
    public Transform partToRotate;
    public float openDistance = 1.3f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameIsOver)
        {
            return;
        }

        Distance = Vector3.Distance(door.transform.position, player.position);

        if (Distance > openDistance)
        {
            // On donne les indication au joueur
            indication.SetActive(false);
        }

        if (Distance <= openDistance && !player.GetComponent<Player_stats>().haveKey)
        {
            // On donne les indication au joueur
            indication.SetActive(true);
        }

        if (Distance <= openDistance && player.GetComponent<Player_stats>().haveKey)
        {
            OpenDoor();
        }
    }
    
    // Ouverture de la porte
    public void OpenDoor()
    {
        key.SetActive(false);
        doorClosed.SetActive(false);
        WinLevel();
        // Lancer l'animation d'ouverture de la porte
    }

    //fonction pour la victoire 
    public void WinLevel()
    {
        winMessage.text = EndLabyrinthMessage;
        GameManager.gameIsOver = true;
        levelWin.SetActive(true);
        winLevelSound.Play();
    }

    // Fonction pour aller au niveau suivant 
    public void NextLevel()
    {
        //On joue le son au click sur le bouton
        buttons.Play();
        sceneFader.FadeTo(levelToLoad);
    }
    public void Retry()
    {
        //On joue le son au click sur le bouton
        buttons.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Retourner au menu 
    public void MainMenu()
    {
        //On joue le son au click sur le bouton
        buttons.Play();
        sceneFader.FadeTo("MainMenu");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(door.transform.position, openDistance);
    }
}
