using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
  //Audio
  public AudioSource source;
  public AudioClip missileFired;
  public AudioClip explosionSound;
  public AudioClip plus50Sound;
  public AudioClip bonusSound;

  void Start()
  {
    if (PlayerPrefs.GetInt("sfx",0) == 1) source.enabled = false;
  }
}
