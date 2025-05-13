using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauronBeam : MonoBehaviour
{
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
