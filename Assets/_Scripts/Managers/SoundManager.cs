using System.Collections.Generic;
using UnityEngine;

using Audio = CustomTypes.Audio;

public class SoundManager : MonoBehaviour {
  private static SoundManager _instance;
  public static SoundManager Instance {
    get {
      return _instance;
    }
  }

  private List<GameObject> _sounds;

  private void Awake() {
    _instance = this;

    _sounds = new List<GameObject>();
  }

  public void PlaySound(Audio.Type sound, float volume = 1f) {
    AudioClip audio = AssetsManager.Instance.Sounds(sound);

    if (audio != null) {
      GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
      AudioSource audioSource = gameObject.GetComponent<AudioSource>();

      _sounds.Add(gameObject);

      audioSource.PlayOneShot(audio, volume);

      Invoke(nameof(deleteSoundObject), audio.length + .5f);
    }
  }

  private void deleteSoundObject() {
    GameObject gameObject = _sounds[0];
    Destroy(gameObject);
    _sounds.Remove(gameObject);
  }
}
