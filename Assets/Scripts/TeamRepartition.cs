using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TeamRepartition : MonoBehaviour
{
    private string[] roleList = { "Chicken", "Wolf" };
    public string role;
    public GameObject[] chickenList;
    public GameObject[] wolfList;
    public Material chickenMaterial;
    public Material wolfMaterial;
    private float timeSinceLastColision = 0;
    private void Awake()
    {
        role = roleList[Random.Range(0, roleList.Length)]; // Assign random role to Entity
        gameObject.tag = role;
        if (role == "Chicken")
        {
            gameObject.GetComponent<MeshRenderer>().material = chickenMaterial;
        }
        if (role == "Wolf")
        {
            gameObject.GetComponent<MeshRenderer>().material = wolfMaterial;
        }

    }
    private void Update()
    {
        chickenList = GameObject.FindGameObjectsWithTag("Chicken");
        wolfList = GameObject.FindGameObjectsWithTag("Wolf");
        timeSinceLastColision += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Chicken")
        {
            if (timeSinceLastColision > 5 && role == "Wolf")
            {
                timeSinceLastColision = 0;

                Debug.Log(gameObject.name);

                role = "Chicken";
                gameObject.GetComponent<MeshRenderer>().material = chickenMaterial;
                collision.gameObject.GetComponent<TeamRepartition>().ChickenOnColisionWithWolf();

                gameObject.tag = role;
            }
        }
    }

    public void ChickenOnColisionWithWolf()
    {
         role = "Wolf";
         gameObject.GetComponent<MeshRenderer>().material = wolfMaterial;
         gameObject.tag = role;
         timeSinceLastColision = 0;
    }
}
