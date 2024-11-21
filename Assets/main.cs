using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    private const int MAX_GAME_DURATION = 40;      // 3 minutes / partie
    private const int WAVE_TIMER = 20;             // 20 secondes / vague
    private const float TIMING_BETWEEN_WAVES = 3f; // 3 secondes de pause entre les vagues
    private bool isWaveDelayCounterStarted = false;
    public float GlobalTimer;
    public int wavePassed = 0;
    public float timer;
    public int seconds;
    public float durationBetweenWaves;
    public Text waveText;
    public Text time;
    public Text waitingBetweenWaves;
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        GlobalTimer = MAX_GAME_DURATION;
        timer = WAVE_TIMER;
        durationBetweenWaves = TIMING_BETWEEN_WAVES;
        waitingBetweenWaves.text = "";
        waveText.text = "Vagues pass�es : 0/9";
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalTimer > -0.1)
        {
            // R�cup�rer les interactions de l'utilisateur
            // ... Les traiter
            // ... Faire agir l'environnement alentour en cons�quence
            // R�cup�rer la positions des bots
            // ... D�placement des bots
            // ... Traiter les positions
            // ... Actions des bots ici
            // Update du nombre de proies restantes
            if (GlobalTimer > 0.00001)
            {
                Timer();
            }
            else
            {
                PrintEndScreen();
            }
        }
        // Fin de la boucle si le timer est d�sactiv�
        // printEndScreeen();
        // Afficher un �cran de fin de jeu ? (Ce serait cool)
    }

    private void PrintEndScreen()
    {
        time.text = ""; waveText.text = ""; waitingBetweenWaves.text = "";
        Debug.Log("[GAME STATUS UPDATE] -> GAME FINISHED");
        gameOverText.text = "Partie termin�e!";
        GlobalTimer = -1;
    }

    private void Timer()
    {
        if (timer > 0)
        {
            GlobalTimer -= Time.deltaTime;
            timer -= Time.deltaTime;
            seconds = (int)timer + 1;
            time.text = seconds.ToString();
        }
        else
        {
            if (!isWaveDelayCounterStarted) // Reset la dur�e d'attente � chaque fin de boucle
            {
                Debug.Log("[GAME STATUS UPDATE] -> WAVE ENDED");
                Debug.Log("[GAME STATUS UPDATE] -> 3 SECONDS TIMER INITIATED");
                time.text = "Pr�pare-toi...";
                durationBetweenWaves = 3f;
                isWaveDelayCounterStarted = true;
                wavePassed++;
                waveText.text = "Vagues pass�es : " + wavePassed.ToString() + "/9";
                if(wavePassed == 8)
                {
                    GlobalTimer = timer + 20f; // Pour la synchronisation avec la derni�re vague
                }
            }
            if (durationBetweenWaves > 0) {
                durationBetweenWaves -= Time.deltaTime;
                seconds = (int)durationBetweenWaves + 1;
                waitingBetweenWaves.text = seconds.ToString();
            }
            else // Fin des 3 secondes d'attente entre les vagues
            {
                Debug.Log("[GAME STATUS UPDATE] -> 3 SECONDS TIMER TERMINATED");
                waitingBetweenWaves.text = "";
                timer = WAVE_TIMER;
                isWaveDelayCounterStarted = false;
                Debug.Log("[GAME STATUS UPDATE] -> NEW WAVE STARTED");
            }
        }
    }
}
