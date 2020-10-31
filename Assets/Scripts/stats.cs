using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stats : MonoBehaviour
{
  private Vector3 startingPos; //starting position of car for reswpawn
  private GameObject HUD; //access the game HUD for displaying messages
  public GameObject highScore; //store the high score to be displayed

  public Text healthText, boostText, moneyText, respawningText;
  public GameObject healthBar;
  public GameObject boostBar;
  public float health = 0f;
  public float boost = 0f;
  public float money = 0f;
  public float speed = 0f;
  public float score = 0f;
  private float respawnTime;
  public bool gameEnded;
  private float lastUpdate;

  void Start()
  {
    HUD = GameObject.Find("HUD");
    startingPos = transform.position;
    lastUpdate = Time.time;
    health = 100f;
    boost = 100f;
    speed = 6500f;
    respawnTime = 2.3f;
    gameEnded = false;
  }

  void FixedUpdate()
  {
    //Update score
    if (!gameEnded) moneyText.text = "Score: " + score;
    else
    {
      if (score > PlayerPrefs.GetFloat("HighScore", 0))
      {
        //Set new high score
        PlayerPrefs.SetFloat("HighScore", score);
        PlayerPrefs.Save();
        moneyText.text = "New High: " + score;
      }
    }

    //increase score over time (linear)
    if (Time.time - lastUpdate >= 0.5f && !gameEnded)
    {
      score += 1;
      lastUpdate = Time.time;
    }

    //show respawning time left
    //increase score over time (linear)
    if (Time.time - lastUpdate >= 0.1f && gameEnded)
    {
      respawningText.text = "Respawning in....." + respawnTime.ToString("F1");
      respawnTime -= 0.1f;
      lastUpdate = Time.time;
    }
  }

  void Update()
  {
    //Update health
    healthText.text = health.ToString("F0") + "/150";
    healthBar.GetComponent<Slider>().value = health;

    //Update boost
    boostText.text = boost.ToString("F0") + "/150";
    boostBar.GetComponent<Slider>().value = boost;

    // HOLD SHIFT TO GO ZOOOOOOOOOOOOM
    if (Input.GetKeyDown(KeyCode.LeftShift)) speed = speed * 3;
    if (Input.GetKeyUp(KeyCode.LeftShift)) speed = speed / 3;

    //if dead
    if (health <= 0f && !gameEnded)
    {
      gameEnded = true;
      GetComponent<PlayerMovement>().enabled = false;
      StartCoroutine(Restart());
    }
  }

  IEnumerator Restart()
  {
    respawningText.GetComponent<Text>().enabled = true;
    GetComponentInChildren<MeshRenderer>().enabled = false;
    yield return new WaitForSeconds(respawnTime);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }
}
