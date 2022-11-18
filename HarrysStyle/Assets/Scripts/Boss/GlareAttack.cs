using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlareAttack : MonoBehaviour
{
    [SerializeField] private float stunDuration = 1.1f;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            otherCollider.gameObject.GetComponent<CharacterController>().Stun(stunDuration);
            otherCollider.gameObject.GetComponent<weapon>().Stun(stunDuration);
        }
    }
}
