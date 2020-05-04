using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMove : MonoBehaviour
{
    public Transform[] waypoints;
    int cur = 0;

    public float speed = 0.3f;

    void FixedUpdate()
    {
        if (transform.position != waypoints[cur].position)
        {
            Vector3 p = Vector3.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;


    }

    /*  void OnTriggerEnter3D(Collider3D co)
      {
          if (co.name == "cube")
              Destroy(co.gameObject); 
      }->웨이포인트가 안멈추니 북극곰을 디스트로이 해서 멈춰버리기
      */

}