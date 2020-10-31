using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {

    public stats stats;
    public Transform player;
    public Rigidbody rb4Speed;
    public GameObject HUD;
    public Vector3 colliderOffset;
    public GameObject bonusText;

    void Start() { HUD = GameObject.Find("HUD"); }

    void OnCollisionEnter(Collision collisionInfo)
    {
      if (collisionInfo.collider.tag == "Building")
      {
        if ((collisionInfo.relativeVelocity.magnitude) > 8) stats.health -= collisionInfo.relativeVelocity.magnitude * 0.5f;
      }
      if (collisionInfo.collider.tag == "Enemy")
      {
        //lower health
        stats.health -= 50f;
        //destroy enemy
        Destroy(collisionInfo.collider.gameObject);
      }
    }

    void OnTriggerEnter(Collider col)
    {
      if (col.GetComponent<pickUp>() != null)
      {
        //check if powerup IS as powerup
        pickUp temp = col.GetComponent<pickUp>();

        //extra health becomes score
        if (stats.health + temp.health >= 150)
        {
          temp.money = temp.health;
          temp.health = 150 - stats.health;
          temp.money -= temp.health;
          temp.health = Mathf.Round(temp.health);
          temp.money = Mathf.Round(temp.money);
        }

        //extra boost becomes score
        if (stats.boost + temp.boost >= 149) temp.boost = 149 - stats.boost;

        //pickup stats
        stats.score += temp.money;
        stats.boost += temp.boost;
        stats.health += temp.health;

        //output tag of what got hit (e.g. +10 health)
        var tempText = Instantiate(bonusText, HUD.transform.position, Quaternion.identity);
        tempText.transform.SetParent(HUD.transform,true);
        if (temp.health>0) tempText.GetComponent<Text>().text = "Health + " + temp.health;
        if (temp.boost>0) tempText.GetComponent<Text>().text = "Boost + " + temp.boost.ToString("F0");
        if (temp.money>0)
        {
          if (temp.health > 0) tempText.GetComponent<Text>().text = "Health + " + temp.health + " / Score + " + temp.money;
          else tempText.GetComponent<Text>().text = "Score + " + temp.money;
        }
        tempText.name = "Bonus";
        tempText.GetComponent<AudioPlayer>().source.clip = tempText.GetComponent<AudioPlayer>().bonusSound;
        tempText.GetComponent<AudioPlayer>().source.Play();

        //delete powerup game object
        Destroy(col.gameObject);
      }
    }
}
