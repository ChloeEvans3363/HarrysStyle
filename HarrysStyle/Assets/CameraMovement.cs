using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool scrollY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scrollY)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, -10);
        }
    }
}
