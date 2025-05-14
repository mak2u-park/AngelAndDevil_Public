// Trap.cs
using UnityEngine;

public class Hostage : MonoBehaviour, IInteractable
{
    [SerializeField] private HostageType _type;

    public HostageType Type {get; private set;}

    private void Awake()
    {
        if(_type == HostageType.Knight)
        {
            ScoreManager.Instance.IncreaseOneAngelHostage();
        }
        else
        {
            Type = HostageType.DarkMage;
            ScoreManager.Instance.IncreaseOneDevilHostage();
        }
    }

    public void Interact()
    {
        ScoreManager.Instance.RemoveHostage(_type);
        SoundManager.Instance.PlaySFX("Hostage");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
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