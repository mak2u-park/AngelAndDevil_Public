using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem _deathParticle;
    [SerializeField] GameObject Player;
    public void DeathPariclePlay()
    {
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
        
    }
}
