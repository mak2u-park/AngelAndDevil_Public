// Trap.cs
using UnityEngine;

public class Hostage : MonoBehaviour, IInteractable
{
    [SerializeField] private HostageType _type;

    public void Interact()
    {
        // ScoreManager.Instance.AddHostage();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerContoller>();
            if(player is AngelController && _type == HostageType.Knight)
            {
                Interact();
            }
            else if(player is DevilController && _type == HostageType.DarkMage)
            {
                Interact();
            }
        }
    }
}