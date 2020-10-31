using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public Text highScoreText;
    public Text sfxSymbol;
    public Text bgmSymbol;

    void Start()
    {
      if (PlayerPrefs.GetInt("sfx",0) == 0) sfxSymbol.GetComponent<Text>().text = "Mute SFX";
      else sfxSymbol.GetComponent<Text>().text = "Un-Mute SFX";

      if (PlayerPrefs.GetInt("bgm",0) == 0) bgmSymbol.GetComponent<Text>().text = "Mute Music";
      else bgmSymbol.GetComponent<Text>().text = "Un-Mute Music";

      highScoreText.GetComponent<Text>().text = "HIGH SCORE: " + PlayerPrefs.GetFloat("HighScore", 0);
    }

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (operation.progress < 0.9f)
        {
            yield return null;
            var scaledPerc = 0.7f * operation.progress / 0.9f;
            progressText.text = "Loading: " + (100f * scaledPerc).ToString("F0") + "%";
            slider.value = scaledPerc;
        }

        operation.allowSceneActivation = true;
        float perc = 0.7f;
        while (!operation.isDone)
        {
            yield return null;
            perc = Mathf.Lerp(perc, 1f, 0.05f);
            progressText.text = "Loading: " + (100f * perc).ToString("F0") + "%";
            slider.value = perc;
        }
    }

    public void muteEffects()
    {
      if (PlayerPrefs.GetInt("sfx",0) == 0) PlayerPrefs.SetInt("sfx",1);
      else PlayerPrefs.SetInt("sfx",0);
      if (sfxSymbol.GetComponent<Text>().text == "Mute SFX")
      {
        sfxSymbol.GetComponent<Text>().text = "Un-Mute SFX";
      }
      else
      {
        sfxSymbol.GetComponent<Text>().text = "Mute SFX";
      }
      PlayerPrefs.Save();
    }

    public void muteMusic()
    {
      if (PlayerPrefs.GetInt("bgm",0) == 0) PlayerPrefs.SetInt("bgm",1);
      else PlayerPrefs.SetInt("bgm",0);
      if (bgmSymbol.GetComponent<Text>().text == "Mute Music")
      {
        bgmSymbol.GetComponent<Text>().text = "Un-Mute Music";
      }
      else
      {
        bgmSymbol.GetComponent<Text>().text = "Mute Music";
      }
      PlayerPrefs.Save();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
