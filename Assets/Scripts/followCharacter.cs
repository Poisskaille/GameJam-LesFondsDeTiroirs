using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCharacter : MonoBehaviour
{
    [SerializeField] Transform player;       
    [SerializeField] Vector3 offset;         
    [SerializeField] float smoothSpeed;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = new Vector3(player.position.x, transform.position.y, player.position.z) + offset;
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }
    }
}
