using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private int attackDamage = 1;
    public GameObject checkpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController player = collision.GetComponent<CharacterController>();

            StartCoroutine(spikeEffect(player));
        }
    }

    //freeze player for 1 second then teleport them to the checkpoint
    public IEnumerator spikeEffect(CharacterController player)
    {
        if (!player.invincible)
        {
            player.Stun(1f);
            player.Damage(attackDamage);
        }

        yield return new WaitForSeconds(0.95f);

        player.transform.position = checkpoint.transform.position;
    }
}
