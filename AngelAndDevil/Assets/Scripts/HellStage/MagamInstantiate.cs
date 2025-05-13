using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagamInstantiate : MonoBehaviour
{
    [SerializeField] GameObject magamPrefab;
    float magmaMinGenerateTime = 5f;
    float magmaMaxGenerateTime = 10f;
    float magmaGenerateTime = 0f;
    float randomTime = 0f;
    public void GenerateMagma()
    {
        GameObject magma = Instantiate(magamPrefab, transform.position, Quaternion.identity);
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);
    }

    public void RandomGenerateMagma()
    {
        randomTime = Random.Range(magmaMinGenerateTime, magmaMaxGenerateTime);
        Invoke("GenerateMagma", randomTime);
    }

    void Update()
    {
       if(magmaGenerateTime <= 0)
        {
            RandomGenerateMagma();
            magmaGenerateTime = randomTime;
        }
        else
        {
            magmaGenerateTime -= Time.deltaTime;
        }

    }
}
