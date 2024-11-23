using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{


    public TeamRepartition teamRepartition;
    public Vector3 target;
    public NavMeshAgent agent;

    private bool changedTarget = false;
    private float ChickenTimer = 0;
    private float timeBeforeChangingDirection = 0;

    

    private float timeSinceLastColision = 0;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(90, 90, 0);
        teamRepartition.chickenList = GameObject.FindGameObjectsWithTag("Chicken");
        teamRepartition.wolfList = GameObject.FindGameObjectsWithTag("Wolf");
        agent = GetComponent<NavMeshAgent>();
        
        agent.SetDestination(target);
    }

    // Update is called once per frame

    

    IEnumerator waitForFive() 
    {
        yield return new WaitForSeconds(5f);
    }

    void Update()
    {
        timeSinceLastColision += Time.deltaTime;
        

        if (timeSinceLastColision > 3 || teamRepartition.role == "Chicken")
        {
            if (teamRepartition.role == "Wolf")
            {
                target = teamRepartition.chickenList[0].transform.position;
                // Get new target based on distance
                for (int i = 0; i < teamRepartition.chickenList.Length; i++)
                {
                    if (Vector3.Distance(transform.position, teamRepartition.chickenList[i].transform.position) < Vector3.Distance(transform.position, target))
                    {
                        target = teamRepartition.chickenList[i].transform.position;
                    }
                }

                agent.SetDestination(target);
            }

            if (teamRepartition.role == "Chicken")
            {
                if (ChickenTimer > timeBeforeChangingDirection)
                {
                    ChickenTimer -= 3;
                    timeBeforeChangingDirection = Random.Range(1, 4);
                    target = new Vector3((transform.position.x + Random.Range(-10, 10)), transform.position.y, (transform.position.z + Random.Range(-10, 10)));
                }
                ChickenTimer += Time.deltaTime;
                for (int i = 0; i < teamRepartition.wolfList.Length; i++)
                {
                    if (Vector3.Distance(transform.position, teamRepartition.wolfList[i].transform.position) < 5)
                    {
                        target = -(teamRepartition.wolfList[i].transform.position - transform.position) + transform.position;
                        break;
                    }

                agent.SetDestination(target);
                }
            }
        }

    }

}
