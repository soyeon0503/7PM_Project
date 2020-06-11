using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeSpan = 0f;
    public GameObject normal;
    public const float tempScala = 1f / 100000f;

    private void Start()
    {

    }

    private void Update()
    {
        Debug.Log(timeSpan);
        if (timeSpan < 50.0f) 
            timeSpan += Time.deltaTime; // timeSpan이 50보다 작으면 timeSpan에 시간 누적
        else timeSpan = 50.0f; // timeSpan이 5보다 크거나 같으면 timeSpan을 3으로 고정
           normal.transform.localScale = new Vector3(normal.transform.localScale.x - timeSpan * tempScala, normal.transform.localScale.y - timeSpan * tempScala, normal.transform.localScale.z - timeSpan * tempScala);
      
    }
    private void Destroyed()
    {
        if (timeSpan == 50)
        {
            Destroy(this);
        }
    }
}
