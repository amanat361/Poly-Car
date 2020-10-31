using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class boosterooni : MonoBehaviour
{
  public stats playerReference;
  public Text pauseSymbol;
  private float lastUpdate;
  private bool boostOn;
  public bool isPaused;

  void Start()
  {
    isPaused = false;
    boostOn = false;
    lastUpdate = Time.time;
    playerReference = GameObject.Find("Car").GetComponent<stats>();
    pauseSymbol = GameObject.Find("PauseText").GetComponent<Text>();
  }

  void Update()
  {
    if (playerReference.boost < 3 && boostOn) boost();

    if (Time.time - lastUpdate >= 0.05f)
    {
      if (boostOn && (playerReference.boost - 3) >= 0) playerReference.boost -= 3;
      else if (!boostOn && playerReference.boost < 150) playerReference.boost += 0.1f;
      lastUpdate = Time.time;
    }
  }

  public void boost()
  {
    boostOn = !boostOn;
    if (!boostOn) playerReference.speed /= 2;
    else playerReference.speed *= 2;
  }

  public void pause()
  {
    isPaused = !isPaused;
    if (!isPaused)
    {
      pauseSymbol.text = "| |";
      Time.timeScale = 1;
    }
    else
    {
      pauseSymbol.text = "▶";
      Time.timeScale = 0;
    }
  }

  public void goHome(){SceneManager.LoadScene(0);}
}
