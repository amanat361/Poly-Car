using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
  // Stats
  public float health = 0f;
  public float boost = 0f;
  public float money = 0f;
  public bool speed = false;

  // Haha item go bounce bounce spin spin
  private float degreesPerSecond = 45.0f;
  private float amplitude = 0.3f;
  private float frequency = 1.5f;

  // Position Storage Variables
  Vector3 posOffset = new Vector3 ();
  Vector3 tempPos = new Vector3 ();

  // Use this for initialization
  void Start () {
      // Store the starting position & rotation of the object
      tempPos = new Vector3 (transform.position.x, transform.position.y + 0.7f, transform.position.z);
      transform.position = tempPos;
      posOffset = transform.position;
  }

  // Update is called once per frame
  void Update () {
      // Spin object around Y-Axis
      transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

      // Float up/down with a Sin()
      tempPos = posOffset;
      tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

      transform.position = tempPos;
  }
}
