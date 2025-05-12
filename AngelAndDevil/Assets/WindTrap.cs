using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrap : MonoBehaviour
{
    [SerializeField] private float _windPower;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WindTrap");
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.AddForce(transform.up * _windPower, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody2D가 없습니다.");
            }
        }
    }
}
