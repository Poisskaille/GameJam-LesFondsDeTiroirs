using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private string[] roleList = { "Chicken", "Wolf" }; 

    public string role;
    public GameObject[] chickenList;
    public GameObject[] wolfList;
    public Vector3 target;
    public NavMeshAgent agent;

    private bool changedTarget = false;


    private void Awake()
    {
        role = roleList[Random.Range(0, roleList.Length)]; // Assign random role to AI
        gameObject.tag = role;
    }

    // Start is called before the first frame update
    void Start()
    {

        chickenList = GameObject.FindGameObjectsWithTag("Chicken");
        wolfList = GameObject.FindGameObjectsWithTag("Wolf");
        agent = GetComponent<NavMeshAgent>();
        
        if (role == "Wolf")
        {
            
        }
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {

        if (role == "Wolf")
        {
            target = chickenList[0].transform.position;
            // Get new target based on distance
            for (int i = 0; i < chickenList.Length; i++)
            {
                if (Vector3.Distance(transform.position, chickenList[i].transform.position) < Vector3.Distance(transform.position, target))
                {
                    target = chickenList[i].transform.position;
                }
            }
            agent.SetDestination(target);
        }

        if (role == "Chicken") 
        {
            target = new Vector3((transform.position.x + Random.Range(-1, 1)), transform.position.y, (transform.position.z + Random.Range(-1, 1)));
            for (int i = 0; i < wolfList.Length; i++)
            {
                if (Vector3.Distance(transform.position, wolfList[i].transform.position) < 5) 
                {
                    target = -(wolfList[i].transform.position - transform.position);
                    break;
                }
            }

            agent.SetDestination(target);
        }

        // Poule va en 0, 0 si chass�e ?




    }

}