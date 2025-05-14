// Trap.cs
using UnityEngine;
using System.Collections;
public class Trap : MonoBehaviour
{
    [SerializeField] private TrapType _type;
    [SerializeField] private GameObject _trapEffect;
    private GameObject _target;

    private const float TrapEffectOffsetX = -0.1f;
    private const float TrapEffectOffsetY = 0.3f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _target = collision.gameObject;
            var player = _target.GetComponent<PlayerController>();

            if (_trapEffect != null)
            {
                Vector3 trapEffectPosition = new Vector3(_target.transform.position.x + TrapEffectOffsetX, _target.transform.position.y + TrapEffectOffsetY, _target.transform.position.z);
                StartCoroutine(CreateTrapEffect(trapEffectPosition, Quaternion.identity));
            }

            if((player is AngelController && _type == TrapType.Lava)
            || (player is DevilController && _type == TrapType.HolyWater)
            || _type == TrapType.Poison)
            {
                player.Die();
            }
        }
    }

    public IEnumerator CreateTrapEffect(Vector3 position, Quaternion rotation)
    {
        GameObject trapEffect = Instantiate(_trapEffect, position, rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(trapEffect);
    }
}