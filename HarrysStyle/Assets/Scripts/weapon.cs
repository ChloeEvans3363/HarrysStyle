using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firingPoint;
    public GameObject projectile;
    public Quaternion bulletVec;
    [SerializeField] private float shotCooldownOG = .5f;
    private float shotCooldown;

    public void Start()
    {
        shotCooldown = shotCooldownOG;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shotCooldown <= 0)
        {
            Shoot();
            shotCooldown = shotCooldownOG;
        }
       if(shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        //gets rotation of player firing point to mouse position
        Vector2 positionOnScreen = firingPoint.position;

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float angle = Vector2.SignedAngle(positionOnScreen, mouseOnScreen);
        float angle = Mathf.Atan2(mouseOnScreen.y - positionOnScreen.y, mouseOnScreen.x - positionOnScreen.x) * 180 / Mathf.PI;
        bulletVec = Quaternion.Euler(new Vector3(0f, 0f, angle));
        //creates bullet facing the rotation made above
        //if it is facing straight, check our rigidbody setup (freeze rotation?)
        Instantiate(projectile, firingPoint.position, bulletVec);

    }
}
