using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamRepartition : MonoBehaviour
{
    private string[] roleList = { "Chicken", "Wolf" };
    public string role;
    public GameObject[] chickenList;
    public GameObject[] wolfList;

    public GameObject chickenPrefab; // Prefab pour le r�le "Chicken"
    public GameObject wolfPrefab;    // Prefab pour le r�le "Wolf"
    private GameObject currentModel; // Le mod�le actuellement utilis�
    private SpriteRenderer spriteRenderer; // R�f�rence au SpriteRenderer

    private float timeSinceLastCollision = 0;

    private void Awake()
    {
        // Initialisation du SpriteRenderer
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Attribuer un r�le al�atoire
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

    // M�thode pour changer le sprite en fonction du r�le
    private void UpdateModel()
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        // Instancier le nouveau mod�le en fonction du r�le
        if (role == "Chicken")
        {
            currentModel = Instantiate(chickenPrefab, transform);
        }
        else if (role == "Wolf")
        {
            currentModel = Instantiate(wolfPrefab, transform);
        }

        // R�initialiser la position et l'�chelle
        
        currentModel.transform.localRotation = Quaternion.Euler(-90, 0, 0);
        currentModel.transform.localScale = Vector3.one;
    }
}
