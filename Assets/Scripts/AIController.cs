using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private float ChickenTimer = 0;
    private float timeBeforeChangingDirection = 0;

    public Material chickenMaterial;
    public Material wolfMaterial;

    private float timeSinceLastColision = 0;

    private void Awake()
    {
        //role = roleList[Random.Range(0, roleList.Length)]; // Assign random role to AI
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

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(90, 90, 0);
        chickenList = GameObject.FindGameObjectsWithTag("Chicken");
        wolfList = GameObject.FindGameObjectsWithTag("Wolf");
        agent = GetComponent<NavMeshAgent>();
        
        agent.SetDestination(target);
    }

    // Update is called once per frame

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
                collision.gameObject.GetComponent<AIController>().ChickenOnColisionWithWolf();

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

    IEnumerator waitForFive() 
    {
        yield return new WaitForSeconds(5f);
    }

    void Update()
    {
        timeSinceLastColision += Time.deltaTime;
        chickenList = GameObject.FindGameObjectsWithTag("Chicken");
        wolfList = GameObject.FindGameObjectsWithTag("Wolf");

        if (timeSinceLastColision > 3 || role == "Chicken")
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
                if (ChickenTimer > timeBeforeChangingDirection)
                {
                    ChickenTimer -= 3;
                    timeBeforeChangingDirection = Random.Range(1, 4);
                    target = new Vector3((transform.position.x + Random.Range(-10, 10)), transform.position.y, (transform.position.z + Random.Range(-10, 10)));
                }
                ChickenTimer += Time.deltaTime;
                for (int i = 0; i < wolfList.Length; i++)
                {
                    if (Vector3.Distance(transform.position, wolfList[i].transform.position) < 5)
                    {
                        target = -(wolfList[i].transform.position - transform.position) + transform.position;
                        break;
                    }
                }

                agent.SetDestination(target);
            }
        }

    }

}
