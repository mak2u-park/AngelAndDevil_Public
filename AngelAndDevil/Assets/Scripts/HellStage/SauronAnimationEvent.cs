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
    }



    public void SearchPlayer(GameObject detectRange)
    {
        dr = sauron.detectRange;
        if(dr != null)
        {
        Detecting Detect = dr.GetComponent<Detecting>();
        Detect.SetSauron(sauron);
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
