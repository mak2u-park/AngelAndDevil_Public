using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauronAnimationEvent : MonoBehaviour
{
    GameObject dr;
    Sauron sauron;
    [SerializeField] GameObject beamPivot;
    
    
    

    private void Awake()
    {
        sauron = GetComponent<Sauron>();
        
    }

    private void Start()
    {
        sauron.SetBeamPivot(beamPivot);
        dr = sauron.detectRange;
    }

    public void Enable()
    {
        BoxCollider2D boxCollider;
        boxCollider = dr.GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
    }

    public void Disable()
    {
        BoxCollider2D boxCollider;
        boxCollider = dr.GetComponent <BoxCollider2D>();
        if (boxCollider != null) 
        {
            boxCollider.enabled = false;
        }
    }

    public void AttackAngel(GameObject sauronBeam)
    {
        sauron.SetBeam(Instantiate(sauronBeam, beamPivot.transform.position, Quaternion.identity));
    }

    public void AngelDead()
    {
        sauron.KillAngel();
        sauron.EndDetect(); 
    }
    


}
