using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Devil"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Die();
        }
    }
}
