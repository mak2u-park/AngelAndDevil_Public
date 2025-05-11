using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] private GameObject oppositBent;

    public Vector2 GetVentTransform()
    {
        return new Vector2(transform.position.x, transform.position.y + 1);
    }

    public Vector2 GetOppositVentTransform()
    {
        return new Vector2(oppositBent.transform.position.x, oppositBent.transform.position.y + 1);
    }

    
}
