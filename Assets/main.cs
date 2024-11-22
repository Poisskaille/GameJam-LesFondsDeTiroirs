using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public const int NUMBER_OF_PLAYERS = 15;
    private const int MAX_GAME_DURATION = 40;      // 3 minutes / partie
    private const int WAVE_TIMER = 20;              // 20 secondes / vague
    private const float TIMING_BETWEEN_WAVES = 3f;  // 3 secondes de pause entre les vagues
    private bool isWaveDelayCounterStarted = false;
    public float GlobalTimer;
    public float timer;
    private int wavePassed = 0;
    private int prevWave = 0;
    private const int sheepRoleID = 1;
    private const int wolfRoleID = 2;
    private int seconds;
    private float durationBetweenWaves;
    public Text waveText;
    public Text time;
    public Text waitingBetweenWaves;
    public Text gameOverText;
    // --- Venant d'autres fichiers dans la version finale --- //
    public int[,] characters = new int [15,2];
    public int numberOfWolves = 3;

    // Start is called before the first frame update
    void Start()
    {
        durationBetweenWaves = TIMING_BETWEEN_WAVES;
        GlobalTimer = MAX_GAME_DURATION;
        timer = WAVE_TIMER;
        waveText.text = "Vagues passées : 0/9";
        waitingBetweenWaves.text = "";
        gameOverText.text = "";
        var rand = new System.Random();
        for (int i = 0; i < NUMBER_OF_PLAYERS; i++) // Initialisation de characters[i,j] = { {1,2,3,...,15}, {roleId,roleId,...,roleID} };
        {
            characters[i, 0] = i;
            characters[i, 1] = rand.Next(1,2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalTimer > 0) // Tant que la partie est en cours
        {
            if (prevWave < wavePassed)
            {
                SwapCharacters(characters);
                prevWave = wavePassed;
            }
            // Récupérer les interactions de l'utilisateur
            // ... Les traiter
            // ... Faire agir l'environnement alentour en conséquence
            // Récupérer la positions des bots
            // ... Déplacement des bots
            // ... Traiter les positions
            // ... Actions des bots ici
            // Update du nombre de proies restantes
            UpdateTimer();
        }
        else
        {
            PrintEndScreen();
        }
    }

    private void SwapCharacters(int[,] characters)
    {
        for (int i = 0; i < characters.GetLength(0); i++)
        {
            Debug.Log("ID : " + characters[i, 0] + " ROLE ID : " + characters[i, 1]);
        }
    }

    private void PrintEndScreen()
    {
        if (GlobalTimer > -1 && GlobalTimer < 0)
        {
            time.text = ""; waveText.text = ""; waitingBetweenWaves.text = "";
            Debug.Log("[GAME STATUS UPDATE] -> GAME FINISHED");
            gameOverText.text = "Partie terminée!";
            GlobalTimer = -1; // Fin temporaire du jeu, absolument pas finale X)
        }
    }

    private void UpdateTimer()
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
            if (!isWaveDelayCounterStarted) // Reset la durée d'attente à chaque fin de boucle
            {
                Debug.Log("[GAME STATUS UPDATE] -> WAVE ENDED");
                Debug.Log("[GAME STATUS UPDATE] -> 3 SECONDS TIMER INITIATED");
                time.text = "Prépare-toi...";
                durationBetweenWaves = 3f;
                isWaveDelayCounterStarted = true;
                wavePassed++;
                waveText.text = "Vagues passées : " + wavePassed.ToString() + "/9";
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
