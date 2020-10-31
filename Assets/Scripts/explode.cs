using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class explode : MonoBehaviour
{
  public GameObject explosion;
  public GameObject playerStats;
  public GameObject HUD;
  public Text evadedText;
  private float evadeDistance = 15f;
  private AudioPlayer ap;

  void Start()
  {
    StartCoroutine(LateStart());
    playerStats = GameObject.Find("Car");
    HUD = GameObject.Find("HUD");
    ap = GameObject.Find("Car").GetComponent<AudioPlayer>();
  }

  void OnCollisionEnter(Collision col)
  {
    Instantiate(explosion, transform.position, transform.rotation);
    ap.source.clip = ap.explosionSound;
    ap.source.Play();

    if (col.collider.name != "CarBody" && Vector3.Distance(transform.position, playerStats.transform.position) < evadeDistance)
    {
      var temp = Instantiate(evadedText, HUD.transform.position, Quaternion.identity);
      temp.transform.SetParent(HUD.transform,true);
      temp.GetComponent<Text>().text = "MISSILE EVADED +50 POINTS";
      temp.name = "MissileEvaded";
      temp.GetComponent<AudioPlayer>().source.clip = temp.GetComponent<AudioPlayer>().plus50Sound;
      temp.GetComponent<AudioPlayer>().source.Play();

      playerStats.GetComponent<stats>().score += 50;
    }
    Destroy(this.gameObject);
  }

  IEnumerator LateStart()
  {
    yield return new WaitForSeconds(0.2f);
    GetComponent<AIFollow>().speed = 15.5f + (playerStats.GetComponent<stats>().score / 1600);
  }
}
