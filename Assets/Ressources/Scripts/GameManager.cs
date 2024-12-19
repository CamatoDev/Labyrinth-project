using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Reference aux Stats du joueur 
    public Player_stats player_Stats;
    // Nombre de temps prévu pour le niveau 
    public float Leveltime = 100f;
    // Si le joueur possède une clé
    public Image keyImage;

    // Nombre de temps passé dans le niveau 
    public Text timer;

    public static bool gameIsOver;

    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
        //Le temps du que le joueur dois passer dans le niveau est défini  
        Player_stats.playerTime = Leveltime;
        keyImage.color = new Color(255f, 255f, 255, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player_stats.isDead)
        {
            //Le temps que le joueur passe est incrémenté à chaque seconde 
            Player_stats.playerTime -= Time.deltaTime;
            //On bloque la valeur du temps à 0
            if (Player_stats.playerTime <= 0f)
                Player_stats.playerTime = 0f;
            //on actualise le temps passé dans le niveau 
            timer.text = string.Format("{0:00.00}", Player_stats.playerTime);
        }

        // Si le joueur possède une clé
        if (player_Stats.haveKey)
        {
            keyImage.color = new Color(255f, 255f, 255, 255f);
        }
    }
}
