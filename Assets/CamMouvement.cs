using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouvement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speed;
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }
}
