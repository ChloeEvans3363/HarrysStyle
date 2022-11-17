using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool scrollY;
    [SerializeField] private float minBound;
    [SerializeField] private float maxBound;
    [SerializeField] private float nonScrollingPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scrollY)
        {
            transform.position = new Vector3(nonScrollingPos, playerTransform.position.y, -10);
            if (transform.position.y < minBound)
            {
                transform.position = new Vector3(transform.position.x, minBound, transform.position.z);
            }
            else if (transform.position.y > maxBound)
            {
                transform.position = new Vector3(transform.position.x, maxBound, transform.position.z);
            }
        }
        else
        {
            transform.position = new Vector3(playerTransform.position.x, nonScrollingPos, -10);
            if(transform.position.x < minBound)
            {
                transform.position = new Vector3(minBound, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > maxBound)
            {
                transform.position = new Vector3(maxBound, transform.position.y, transform.position.z);
            }
        }
    }

    public void SetCam(bool _scrollY, float _minBound, float _maxBound, float _nonScrollPos)
    {
        scrollY = _scrollY;
        minBound = _minBound;
        maxBound = _maxBound;
        nonScrollingPos = _nonScrollPos;
    }
}
