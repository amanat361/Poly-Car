using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
  public GameObject carModel;
  public Rigidbody rb;
  public Vector3 localOffset;
  private float hoverHeight = 0.2f;
  private float terrainHeight;
  private float rotationAmount;
  private RaycastHit hit;
  private Vector3 pos;
  private Vector3 forwardDirection;

  void FixedUpdate () {
    // Keep at specific height above terrain
    pos = transform.position;
    terrainHeight = Terrain.activeTerrain.SampleHeight(pos);

    //Shoot a raycast from a point on the vehicle down until it hits a collidable object
    Physics.Raycast(carModel.transform.TransformPoint(localOffset), Vector3.down, out hit);

    //Draw the raycast for debugging purposes
    Debug.DrawLine(carModel.transform.TransformPoint(localOffset),hit.point,Color.cyan);

    //Allign vertical rotation with level of the surface of the terrain
    if (hit.collider.tag != "Prop") transform.up -= (transform.up - hit.normal) * 0.1f;

    //Doesn't apply to props
    if (hit.collider.tag != "Prop")
    {
      //Keep a certain hight above the ground, and use a different variable if the ground is of type (tag) Terrain
      if (hit.collider.tag != "Terrain") transform.position = new Vector3(pos.x, hit.collider.transform.position.y + hoverHeight, pos.z);
      else transform.position = new Vector3(pos.x, Terrain.activeTerrain.SampleHeight(transform.position) + 0.65f, pos.z);
    }

    //Tilt the vehicle I think? Idk, it does something at least
    if(Physics.Raycast(transform.position, Vector3.down, hoverHeight)) GetComponent<Rigidbody>().AddForce(Vector3.up * 1f);
    else GetComponent<Rigidbody>().AddForce(Vector3.down * 1f);

    // Movement (WASD) --- (ammended for android rn)
    forwardDirection = -carModel.transform.up;
    //always move forward
    rb.AddRelativeForce(forwardDirection * GetComponent<stats>().speed);
    //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) rb.AddRelativeForce(forwardDirection * GetComponent<stats>().speed);
    //if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) rb.AddRelativeForce(-forwardDirection * GetComponent<stats>().speed);
    if (Input.GetMouseButton(0))
    {
      if (!EventSystem.current.IsPointerOverGameObject())
      {
        if(Input.mousePosition.x > Screen.width * 0.5f) rotationAmount = 110f * Time.deltaTime;
        else rotationAmount = -110f * Time.deltaTime;
        carModel.transform.Rotate (0.0f, 0.0f, rotationAmount);
      }
      else if (EventSystem.current.currentSelectedGameObject != null)
      {
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == null)
        {
          if(Input.mousePosition.x > Screen.width * 0.5f) rotationAmount = 110f * Time.deltaTime;
          else rotationAmount = -110f * Time.deltaTime;
          carModel.transform.Rotate (0.0f, 0.0f, rotationAmount);
        }
      }
    }
  }
}
