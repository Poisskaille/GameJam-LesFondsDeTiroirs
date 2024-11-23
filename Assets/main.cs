using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    // --- Venant d'autres fichiers dans la version finale --- //
    // -- Constantes --- //
    public const int SHEEP_ROLE_ID = 1;
    public const int WOLF_ROLE_ID = 2;
    public const int MAX_GAME_DURATION = 140;      // 20 secondes / vague -> 5 vagues donc 100 secondes / partie
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
    public TMP_Text waveText;
    public TMP_Text time;
    public TMP_Text waitingBetweenWaves;
    public TMP_Text gameOverText;
    // --- Variables exclusives à ce fichier (privées) --- //
    private bool isWaveDelayCounterStarted = false;
    private int currentWave = 1;
    private int prevWave = 0;
    private int seconds;
    private float pauseBetweenWaves;
    private float xPos = 0f;

    // --- FONCTIONS D'INITIALISATION --- //
    private void InitTimers()
    {
        pauseBetweenWaves = TIMING_BETWEEN_WAVES;
        GlobalTimer = MAX_GAME_DURATION;
        timer = WAVE_TIMER;
    }                      // Initialise les durées variables du programme
    private void InitTexts()
    {
        waveText.text = "Vague : 1/7";
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
        if(numberOfPlayers > 9)
        {
            numberOfPlayers -= 3;
        }
        else if (numberOfPlayers > 3 && numberOfPlayers <= 9)
        {
            numberOfPlayers -= 2;
        }
        else
        {
            Debug.Log("Match à mort ici"); // A parametrer
            numberOfWolves = 1;
            PrintEndScreen();
        }
        characters = new int[numberOfPlayers, 2]; // 15, 12, 9, 7, 5, 3, Match à mort
        InitCharacters(characters);
    }

    private void PrintEndScreen()
    {
        time.text = ""; waveText.text = ""; waitingBetweenWaves.text = "";
        gameOverText.text = "Partie terminee!";
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
                time.fontSize = 90;
                time.text = "Prepare toi...";
                pauseBetweenWaves = 3f;
                isWaveDelayCounterStarted = true;
                waveText = GameObject.Find("wavePassedTextTMP").GetComponent<TMP_Text>();
                currentWave++;
                waveText.text = "Vague : " + currentWave.ToString() + "/7";
            } // Reset la durée d'attente à chaque fin de boucle
            if (pauseBetweenWaves > 0)
            {
                pauseBetweenWaves -= Time.deltaTime;
                seconds = (int)pauseBetweenWaves + 1;
                waitingBetweenWaves.text = seconds.ToString();
                // Actualisation de la position du texte "Vague : n/7"
                xPos += 0.6f;
                waveText.rectTransform.position = new Vector3 (xPos, waveText.rectTransform.position.y, waveText.rectTransform.position.z);
            }
            else
            {
                waveText.rectTransform.position = new Vector3(0f, waveText.rectTransform.position.y, waveText.rectTransform.position.z);
                time.fontSize = 120;
                waitingBetweenWaves.text = "";
                timer = WAVE_TIMER;
                isWaveDelayCounterStarted = false;
                xPos = 0f;
            } // Fin des 3 secondes d'attente entre les vagues, passage à la prochaine vague ou fin de programme
        }
    }
}