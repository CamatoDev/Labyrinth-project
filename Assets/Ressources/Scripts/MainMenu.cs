using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Variables pour le son 
    private AudioSource audioSource;
    public AudioSource buttonAudioSource;
    private bool mute = false;
    public Sprite muteImage;
    public Sprite unmuteImage;
    // Variable pour le bouton de volume
    public Button soundButton;

    // Variales pour le chargement du niveau
    public string levelToLoad = "Level01";
    public SceneFader sceneFader;

    // Start is called before the first frame update
    void Start()
    {
        // Assignation des variables 
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Fonction pour le lacement du jeu 
    public void Play()
    {
        //Lacement du jeu 
        buttonAudioSource.Play();
        sceneFader.FadeTo(levelToLoad);
    }

    //Fonction pour le son 
    public void MenuSound()
    {
        mute = !mute;
        if (mute)
        {
            soundButton.image.sprite = muteImage;
            audioSource.mute = true;
        }
        else
        {

            soundButton.image.sprite = unmuteImage;
            audioSource.mute = false;
        }
    }

    // Fonction pour les réglages
    public void Help()
    {
        //
        buttonAudioSource.Play();
        Debug.Log("Ouvertures des options de personnalisations");
    }

    // Fonction pour quitter le jeu 
    public void Quit()
    {
        buttonAudioSource.Play();
        Application.Quit();
    }
}
