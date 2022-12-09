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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if(!scrollY)
            Gizmos.DrawWireCube(new Vector3(minBound + (maxBound - minBound) * 0.5f, nonScrollingPos, 0f), new Vector3(20f + (maxBound - minBound), 11.25f, 0f));
        else
            Gizmos.DrawWireCube(new Vector3(nonScrollingPos, minBound + (maxBound - minBound) * 0.5f, 0f), new Vector3(20f, 11.25f + (maxBound - minBound), 0f));
    }
}
