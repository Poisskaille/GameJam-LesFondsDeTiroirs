using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamRepartition : MonoBehaviour
{
    private string[] roleList = { "Chicken", "Wolf" };
    public string role;
    public GameObject[] chickenList;
    public GameObject[] wolfList;

    public GameObject chickenPrefab; // Prefab pour le rôle "Chicken"
    public GameObject wolfPrefab;    // Prefab pour le rôle "Wolf"
    private GameObject currentModel; // Le modèle actuellement utilisé
    private SpriteRenderer spriteRenderer; // Référence au SpriteRenderer

    private float timeSinceLastCollision = 0;

    private void Awake()
    {
        // Initialisation du SpriteRenderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Attribuer un rôle aléatoire
        role = roleList[Random.Range(0, roleList.Length)];
        gameObject.tag = role;

        // Appliquer le sprite correspondant
        UpdateModel();
    }

    private void Update()
    {
        chickenList = GameObject.FindGameObjectsWithTag("Chicken");
        wolfList = GameObject.FindGameObjectsWithTag("Wolf");
        timeSinceLastCollision += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chicken")
        {
            if (timeSinceLastCollision > 5 && role == "Wolf")
            {
                timeSinceLastCollision = 0;

                Debug.Log(gameObject.name);

                role = "Chicken";
                UpdateModel();

                collision.gameObject.GetComponent<TeamRepartition>().ChickenOnCollisionWithWolf();

                gameObject.tag = role;
            }
        }
    }

    public void ChickenOnCollisionWithWolf()
    {
        role = "Wolf";
        UpdateModel();
        gameObject.tag = role;
        timeSinceLastCollision = 0;
    }

    // Méthode pour changer le sprite en fonction du rôle
    private void UpdateModel()
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // Instancier le nouveau modèle en fonction du rôle
        if (role == "Chicken")
        {
            currentModel = Instantiate(chickenPrefab, transform);
        }
        else if (role == "Wolf")
        {
            currentModel = Instantiate(wolfPrefab, transform);
        }

        // Réinitialiser la position et l'échelle
        
        currentModel.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        currentModel.transform.localScale = Vector3.one;
    }
}
