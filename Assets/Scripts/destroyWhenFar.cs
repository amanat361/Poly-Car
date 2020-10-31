using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWhenFar : MonoBehaviour
{
    public GameObject Car;
    public float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
      maxDistance = 70;
      Car = GameObject.Find("Car");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Car.transform.position,transform.position) > maxDistance)
        Destroy(this.gameObject);
    }
}
