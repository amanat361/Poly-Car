using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public AudioSource source;
    public AudioClip[] playlist;

    void Start() { NextTrack(); }

    private void OnDestroy() { source.Stop(); }

    void NextTrack()
    {
      source.Stop();
      source.clip = playlist[Random.Range(0,14)];
      source.Play();
      Invoke("NextTrack", source.clip.length);
    }

    private void Update()
    {
      if (PlayerPrefs.GetInt("bgm",0) == 1) source.enabled = false;
      else if (source.enabled == false)
      {
        source.enabled = true;
        CancelInvoke();
        NextTrack();
      }
      if (Input.GetKeyDown(KeyCode.Q))
      {
        CancelInvoke();
        NextTrack();
      }
    }
}
