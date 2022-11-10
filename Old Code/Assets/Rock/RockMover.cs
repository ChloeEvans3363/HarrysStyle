using UnityEngine;

public class RockMover : MonoBehaviour
{
    public float speed = 2f;
    Vector3 pos;
    public bool bossBattle = false;

    void Update()
    {
        if (!bossBattle)
        {
            pos = transform.position;
            pos.x += -speed * Time.deltaTime;
            transform.position = pos;
        }
    }
}