using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
  //Raycasst variables
  private Vector3 pos;
  private RaycastHit hit;

  //Get Waypoint Gamebject Variables
  private GameObject wayPoint;
  private Vector3 wayPointPos;

  //AI Follow Speed and Offset from other AI
  public float speed;
  public float aiOffset = 0f;

  //Is this the leader car?
  public int idNum = 0;

  void Start ()
  {
    speed = 12f;
    wayPoint = GameObject.Find("Car");
  }

  void FixedUpdate()
  {
    //Move to and look at the waypoint
    wayPointPos = wayPoint.transform.position;
    transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
    transform.LookAt(wayPointPos);
  }
}
