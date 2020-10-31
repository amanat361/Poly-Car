using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour
{
  public float speed = 100f;
  private Vector3 offset;
  private Vector3 wayPointPos;

  void Start()
  {
    StartCoroutine(epstein());
    offset = new Vector3 (0f,Screen.height * 0.3f,0f);
    transform.position += offset;
  }

  void FixedUpdate()
  {
    wayPointPos = transform.position + new Vector3 (0f,350f,0f);
    transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
  }

  IEnumerator epstein()
  {
    yield return new WaitForSeconds(1.5f);
    Destroy(this.gameObject);
  }
}
