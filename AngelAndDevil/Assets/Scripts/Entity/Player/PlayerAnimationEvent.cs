using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] GameObject player;
    public void DeathPariclePlay(ParticleSystem _deathParticle)
    {
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
    }

    public void DestroyPlayer( )
    {
        Destroy(player);
    }

    public void PlayerVentTranslate()
    {
        DevilController dc = player.GetComponent<DevilController>();
        dc.VentTranslate();
    }

    public void PlayerVentComplete()
    {
        DevilController dc = player.GetComponentInParent<DevilController>();
        dc.VentingComplete();
    }
    
}
