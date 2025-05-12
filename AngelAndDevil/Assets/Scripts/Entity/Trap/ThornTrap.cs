// Trap.cs
using UnityEngine;
using System.Collections;
public class ThornTrap : MonoBehaviour
{
    [SerializeField] private float _delayTime = 2f;

    [SerializeField] private GameObject[] _thorns;

    [SerializeField] private float _startDelayTime = 1f;

    public bool IsEnable { get; set; }
    public bool IsStart { get; set; }

    public void Start()
    {
        IsEnable = false;
        IsStart = false;

        StartCoroutine(StartDelay());
    }

    public void Enable()
    {
        IsEnable = true;
    }

    public void Disable()
    {
        IsEnable = false;
    }    

    public void Update()
    {
        if(IsEnable || !IsStart) return;

        StartCoroutine(Delay());
    }

    public IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(_startDelayTime);
        IsStart = true;
    }

    public IEnumerator Delay()
    {
        Enable();
        for(int i = 0; i < _thorns.Length; i++)
        {
            _thorns[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _thorns[i].SetActive(false);
        }

        for(int i = _thorns.Length - 1; i >= 0; i--)
        {
            _thorns[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _thorns[i].SetActive(false);
        }
        
        yield return new WaitForSeconds(_delayTime);
        Disable();
    }
}