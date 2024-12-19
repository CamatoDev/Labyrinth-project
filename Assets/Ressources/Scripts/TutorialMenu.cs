using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour
{
    // Reference au SceneFader 
    public SceneFader sceneFader;
    
    // Source audio
    public AudioSource audioSource;

    public void Back()
    {
        // Passage au slide suivant
        audioSource.Play();
        SceneManager.LoadScene("Tutorial 1");
    }
    public void Next()
    {
        // Passage au slide suivant
        audioSource.Play();
        SceneManager.LoadScene("Tutorial 2");
    }
    public void Next2()
    {
        // Passage au slide suivant
        audioSource.Play();
        SceneManager.LoadScene("Tutorial 3");
    }
    public void Next3()
    {
        // Passage au slide suivant
        audioSource.Play();
        SceneManager.LoadScene("Tutorial 4");
    }
    public void Play()
    {
        // Passage au slide suivant
        audioSource.Play();
        sceneFader.FadeTo("Level01");
    }

    public void SkipTutorial()
    {
        // Passer le tutoriel et jouer directement 
        audioSource.Play();
        sceneFader.FadeTo("Level01");
    }
}
