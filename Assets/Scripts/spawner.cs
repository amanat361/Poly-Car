using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
  //Missile spawner data
  public GameObject[] missiles;
  public GameObject missilePrefab;
  public int numOfMissiles;
  private Vector3 spawnPoint;
  public float spawnDistance;
  private AudioPlayer ap;

  //Obstacle spawner data
  public GameObject[] buildings; //contain buildings in an  organized way
  private bool spawnCheck = false; //check if something was recently spawned (delay)
  private Vector3 spawnOffset, spawnLookAt, spawnClear;
  private float offX, offZ;
  private int randBuilding; //pick building to spawn for variation
  private int noneAvailable; //try this many times to spawn then give up

  // Start is called before the first frame update
  void Start()
  {
    spawnClear = new Vector3(10f,10f,10f);
    numOfMissiles = 0;
    spawnDistance = 10f;
    missiles = new GameObject[1];
  }

  // Update is called once per frame
  void Update()
  {
    //spawn new missile if there are none
    if (missiles[0] == null && !GetComponent<stats>().gameEnded) spawnMissile();

    //spawn items in randomly around the car
    if (!spawnCheck) StartCoroutine(spawnSomething());
  }

  //spawn missiles into the game based on camera position
  void spawnMissile()
  {
    //make AI
    spawnPoint = (GameObject.Find("Main Camera").transform.position) - (GameObject.Find("CarBody").transform.forward*spawnDistance);
    //spawnPoint = transform.position + (transform.GetChild(0).transform.up * spawnDistance);
    missiles[numOfMissiles] = (GameObject)Instantiate(missilePrefab, spawnPoint, new Quaternion(0f,0f,0f,0f));
    missiles[numOfMissiles].name = "AICAR " + numOfMissiles;
    ap = missiles[numOfMissiles].GetComponent<AudioPlayer>();
    ap.source.clip = ap.missileFired;
    ap.source.Play();
    if (numOfMissiles == 0) missiles[numOfMissiles].GetComponent<AIFollow>().idNum = numOfMissiles;// numOfMissiles++;
    else missiles[numOfMissiles].GetComponent<AIFollow>().idNum = numOfMissiles;//numOfMissiles++;
  }

  //spawn in obstacles and buildings (will later split)
  IEnumerator spawnSomething()
  {
    //add a time to wait
    spawnCheck = true;
    yield return new WaitForSeconds(0.8f);
    randBuilding = Random.Range(0,5);
    noneAvailable = 0;

    //find open spot to spawn something
    do
    {
      noneAvailable++;
      offX = Random.Range(-10f,10f);
      spawnOffset = new Vector3 (0f,-0.1f,0f);
      spawnOffset -= GameObject.Find("CarBody").transform.up * 60f;
      spawnOffset -= GameObject.Find("CarBody").transform.right * offX;
    } while (Physics.CheckBox(transform.position + spawnOffset, spawnClear, Quaternion.identity, 11) && noneAvailable < 3);

    //spawn in the item
    if (noneAvailable != 3)
    {
      var temp = Instantiate(buildings[randBuilding], transform.position + spawnOffset, Quaternion.identity);
      spawnLookAt = new Vector3(transform.position.x,temp.transform.position.y,transform.position.z);
      temp.transform.LookAt(spawnLookAt);
    }
    spawnCheck = false;
  }
}
