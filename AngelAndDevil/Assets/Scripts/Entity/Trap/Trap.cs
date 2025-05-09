// Trap.cs
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private TrapType _type;
    private GameObject _target;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _target = collision.gameObject;
            var player = _target.GetComponent<PlayerContoller>();
            if(player is AngelController && _type == TrapType.Lava)
            {
                player.Die();
            }
            else if(player is DevilController && _type == TrapType.HolyWater)
            {
                player.Die();
            }
            else if(_type == TrapType.Poison)
            {
                player.Die();
            }
        }
    }
}