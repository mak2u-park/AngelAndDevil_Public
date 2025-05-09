using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem _deathParticle;
    [SerializeField] GameObject player;
    public void DeathPariclePlay()
    {
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
    }

    public void OnDestroy()
    {
        Destroy(player);
    }
}
