using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> projectiles;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddProjectileToList(GameObject projectile)
    {
        projectiles.Add(projectile);
    }

    public bool RemoveProjectileFromList(GameObject projectile)
    {
        return projectiles.Remove(projectile);
    }

    public float calculateTheta(Vector3 pos)
    {
        float A, B, C, t;
        float p1, p2, q1, q2;
        float w, s;
        float discriminant;
        float theta;

        q1 = pos.x;
        q2 = pos.y;

        p1 = player.transform.position.x;
        p2 = player.transform.position.y;

        w = 2f; 
        s = 10f; 

        A = (Mathf.Pow(s, 2) - Mathf.Pow(w, 2));
        B = 2 * w * (q1 - p1);
        C = -(Mathf.Pow(q1 - p1, 2) + Mathf.Pow(q2 - p2, 2));

        discriminant = Mathf.Pow(B, 2) - (4 * A * C);

        t = (-B + Mathf.Sqrt(discriminant)) / (2 * A);

        theta = (Mathf.Asin((q2 - p2) / (t * s))) * Mathf.Rad2Deg;

        return theta;
    }
}
