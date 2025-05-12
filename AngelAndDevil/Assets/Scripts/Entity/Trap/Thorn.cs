// Trap.cs
using UnityEngine;
using System.Collections;
public class Thorn : MonoBehaviour
{
    GameObject _target;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.gameObject.CompareTag("Player"))
        {
            _target = collision.gameObject;
            var player = _target.GetComponent<PlayerController>();
            player.Die();
        }
    }
}