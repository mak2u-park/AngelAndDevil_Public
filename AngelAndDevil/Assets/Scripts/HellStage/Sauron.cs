using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Sauron : MonoBehaviour
{
    Animator animator;

    float AlertDelayTime = 5f;
    float CountAlertTime = 0f;
    bool isAlert = false;
    bool isDetected = false;
    bool isFliped = false;
    SpriteRenderer _renderer;
    [SerializeField] public GameObject detectRange;
    private GameObject beam;
    private GameObject beamPivot;
    private GameObject angel; 
    static readonly int IsAlert = Animator.StringToHash("IsAlert");
    static readonly int IsAttack = Animator.StringToHash("IsAttack");

    public void SetAngel(GameObject go)
    {
        angel = go;
    }

    public void SetBeam(GameObject go)
    {
        beam = go;
    }


    public void SetBeamPivot(GameObject go)
    {
        beamPivot = go;
    }




    public void Alert()
    {
        animator.SetBool(IsAlert, true);
        isAlert = true;
    }

    public void EndAlert()
    {
        animator.SetBool(IsAlert, false);
        isAlert = false;
        CountAlertTime = 0f;

    }

    public void DetectAngel()
    {
        animator.SetBool(IsAttack, true);
        isDetected = true;
    }

    public void EndDetect()
    {
        animator.SetBool(IsAttack, false);
        isDetected = false;
    }

    public void KillAngel()
    {
        AngelController ac = angel.GetComponent<AngelController>();
        ac.Die();
    }


    IEnumerator StartAlertProcess()
    {
        Alert();
        yield return new WaitForSeconds(3f);
        EndAlert();
    }

    public void AimingAngel()
    {   
        float totalXScale = Vector2.Distance(angel.transform.position, beamPivot.transform.position) / 4;
        beam.transform.localScale = new Vector2(totalXScale, 2f);
        Vector2 test = angel.transform.position - beamPivot.transform.position;
        float angle = Mathf.Atan2(test.y, test.x) * Mathf.Rad2Deg;
        beam.transform.rotation = Quaternion.Euler(0, 0, angle);
        
    }

    public void XFlip()
    {
        if (angel.transform.position.x > beamPivot.transform.position.x)
        {
            if (isFliped)
            {
                beam.transform.Translate(-1, 0, 0);
                isFliped = false;
            }
            _renderer.flipX = false;
        }
        else
        {
            if (!isFliped)
            {
                beam.transform.Translate(1, 0, 0);
                isFliped = true;
            }
            _renderer.flipX = true;
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
         animator = GetComponent<Animator>();
         _renderer  = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CountAlertTime > AlertDelayTime && isAlert==false)
        {
            StartCoroutine(StartAlertProcess());
            
        }
        else
        {
            CountAlertTime += Time.deltaTime;
        }

        if(beam != null && angel != null)
        {
            
            AimingAngel();
            XFlip();
            
        }

        
        
    }
}
