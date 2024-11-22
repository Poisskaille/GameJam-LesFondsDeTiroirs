using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // --- Venant d'autres fichiers dans la version finale --- //
    // -- Constantes --- //
    public const int SHEEP_ROLE_ID = 1;
    public const int WOLF_ROLE_ID = 2;
    public const int MAX_GAME_DURATION = 100;      // 20 secondes / vague -> 5 vagues donc 100 secondes / partie
    public const int WAVE_TIMER = 20;              // 20 secondes / vague
    public const float TIMING_BETWEEN_WAVES = 3f;  // 3 secondes de pause entre les vagues
    // --- Variables --- //
    public int numberOfPlayers;                    // Modifié à 15 lors de la première vague
    public int[,] characters;                      // arguments : [identifiantPersonnage, rolePersonnage]
    public int numberOfWolves = 3;
    // --- Timer --- //
    public float GlobalTimer;
    public float timer;
    // --- Texts --- //
    public Text waveText;
    public Text time;
    public Text waitingBetweenWaves;
    public Text gameOverText;
    // --- Variables exclusives à ce fichier (privées) --- //
    private bool isWaveDelayCounterStarted = false;
    private int currentWave = 1;
    private int prevWave = 0;
    private int seconds;
    private float pauseBetweenWaves;

    // --- FONCTIONS D'INITIALISATION --- //
    private void InitTimers()
    {
        pauseBetweenWaves = TIMING_BETWEEN_WAVES;
        GlobalTimer = MAX_GAME_DURATION;
        timer = WAVE_TIMER;
    }                      // Initialise les durées variables du programme
    private void InitTexts()
    {
        waveText.text = "Vague : 1/5";
        waitingBetweenWaves.text = "";
        gameOverText.text = "";
    }                       // Initialise les textes
    private void InitCharacters(int[,] characters)
    {
        var rand = new System.Random();
        for (int i = 0; i < numberOfPlayers; i++) // Initialisation des ID des personnages : {1,2,3,...,15}
        {
            characters[i, 0] = i + 1;
        }
        for (int i = 0; i < numberOfWolves; i++)
        {
            int tempID = rand.Next(1, numberOfPlayers);
            characters[tempID, 1] = WOLF_ROLE_ID; // Choisir 3 loups random
        }
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (characters[i, 1] != WOLF_ROLE_ID)
                characters[i, 1] = SHEEP_ROLE_ID;
        }
    } // Nomme trois loups aléatoirement

    // Start is called before the first frame update
    void Start()
    {
        InitTimers();
        InitTexts();
        numberOfPlayers = 18;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalTimer > 0)            // Tant que la partie est en cours
        {
            if (prevWave < currentWave) // S'éxécute au changement de vague
            {
                Debug.Log("Changement de vague");
                UpdateCharacters();
                prevWave = currentWave;
            }
            // Récupérer les interactions de l'utilisateur
                // ... Les traiter
                // ... Faire agir l'environnement alentour en conséquence
            // Récupérer la positions des bots
                // ... Déplacement des bots
                // ... Traiter les positions
                // ... Actions des bots ici
            UpdateTimer();
        }
        else
        {
            PrintEndScreen();
        }
    }

    private void UpdateCharacters()
    {
        if (numberOfPlayers > 6)
        {
            Debug.Log("Personnages avant changement : " + numberOfPlayers);
            numberOfPlayers -= 3;
            Debug.Log("Personnages après changement : " + numberOfPlayers);
            characters = new int[numberOfPlayers, 2]; // 15, 12, 9, 6, 3, Match à mort
            InitCharacters(characters);
        }
        else
        {
            // Match à mort, dernière vague. A parmetrer.
            numberOfPlayers -= 3;
            PrintEndScreen();
        }
    }

    private void PrintEndScreen()
    {
        time.text = ""; waveText.text = ""; waitingBetweenWaves.text = "";
        gameOverText.text = "Partie terminée!";
        Application.Quit();
        GlobalTimer = -1; // Façon de terminer la partie dans le play mode de l'editeur de Unity
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
        else // Temps d'attente de 3 secondes entre les vagues
        {
            if (!isWaveDelayCounterStarted)
            {
                time.text = "Prépare toi...";
                pauseBetweenWaves = 3f;
                isWaveDelayCounterStarted = true;
                currentWave++;
                waveText.text = "Vague : " + currentWave.ToString() + "/5";
            } // Reset la durée d'attente à chaque fin de boucle
            if (pauseBetweenWaves > 0) {
                pauseBetweenWaves -= Time.deltaTime;
                seconds = (int)pauseBetweenWaves + 1;
                waitingBetweenWaves.text = seconds.ToString();
            }   // Affichage du texte "Prépare toi..." et "3...2...1"
            else 
            {
                waitingBetweenWaves.text = "";
                timer = WAVE_TIMER;
                isWaveDelayCounterStarted = false;
            }                            // Fin des 3 secondes d'attente entre les vagues, passage à la prochaine vague ou fin de programme
        }
    }
}