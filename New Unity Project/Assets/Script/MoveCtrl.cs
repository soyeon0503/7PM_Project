using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCtrl : MonoBehaviour
{

    public enum MoveType
    {
        WAY_POINT,
        UP_POINT,
        Destroy,
        CARDBOARD
    }
    public float speed = 40.0f;                     // 이동 속도
    public float damping = 3.0f;                   // 회전 속도를 조절할 계수
    
    private MoveType movetype;// 이동 방식
    private Transform tr;
    private Transform[] points; // 웨이포인트를 저장할 배열
    private int nextIdx = 1;

    public void Start()
    {
        tr = GameObject.Find("Player").GetComponent<Transform>(); 
        movetype = MoveType.UP_POINT;
        points = GameObject.Find("Path")
                .GetComponentsInChildren<Transform>();
      
    }

    void Update()
    {
        switch (movetype)
        {
            case MoveType.UP_POINT:
                MoveWayPoint();
                break;
            case MoveType.WAY_POINT:
                MoveWayPoint();
                break;
            case MoveType.CARDBOARD:
                break;
        }
    }

    void MoveWayPoint()
    {
        // 현재 위치에서 다음 웨이포인트를 바로보는 벡터를 계산
        Vector3 direction= direction = points[nextIdx].position - tr.position; ;
        // 산출된 벡터의 회전 각도를 쿼터니언 타입으로 산출
        Quaternion rot = Quaternion.LookRotation(direction);
        // 현재 각도에서 회전해야 할 각도까지 부드럽게 회전 처리
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);

        // 전진 방향으로 이동 처리
        tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("WAY_POINT"))
        {
            // 맨 마지막 웨이포인트에 도달했을 때 처음 인덱스로 변경
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
        if (coll.CompareTag("UP_POINT"))
        {
            // 맨 마지막 웨이포인트에 도달했을 때 처음 인덱스로 변경
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
        else if (coll.CompareTag("Destroy"))
        {
            Destroy(this);
            SceneManager.LoadScene("Scene_2");
   
        }

    }
  
}
