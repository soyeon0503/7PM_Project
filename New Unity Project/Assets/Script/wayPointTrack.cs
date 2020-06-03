using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointTrack : MonoBehaviour
{

    public Color lineColor = Color.red;   
    private Transform[] point;

    void OnDrawGizmos()
    {
        // 라인의 색상 지정
        Gizmos.color = lineColor;
        // WayPointGroup 게임 오브젝트 아래에 있는 모든 Point 게임오브젝트 추출
        point = GetComponentsInChildren<Transform>();
        int nextIdx = 1;

        Vector3 currPos = point[nextIdx].position;
        Vector3 nextPos;

        // Point 게임오브젝트를 순회하면서 라인을 그림
        for (int i = 0; i <= point.Length; i++)
        {
            // 마지막 Point일 경우 첫 번째 Point로 지정
            nextPos = (++nextIdx >= point.Length) ? point[1].position :
                point[nextIdx].position;
            // 시작 위치에서 종료 위치까지 라인을 그림
            Gizmos.DrawLine(currPos, nextPos);

            currPos = nextPos;
        }
    }
}
