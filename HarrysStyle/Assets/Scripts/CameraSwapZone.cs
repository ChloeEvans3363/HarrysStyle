using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwapZone : MonoBehaviour
{
    [SerializeField] private CameraMovement cam;
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            cam.SetCam(scrollY, minBound, maxBound, nonScrollingPos);
        }
    }
}
