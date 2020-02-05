using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VolumeMaster : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MASTER_VOLUME"))
        {
            PlayerPrefs.SetFloat("MASTER_VOLUME", 0.5f);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
         SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        AudioSource[] EveryAudioSource = FindObjectsOfType<AudioSource>();

        foreach (AudioSource Audio in EveryAudioSource)
        {
            Audio.volume = Audio.volume * PlayerPrefs.GetFloat("MASTER_VOLUME");
        }
    }

    public void SetVolume(float val)
    {
        AudioSource[] EveryAudioSource = FindObjectsOfType<AudioSource>();

        foreach (AudioSource Audio in EveryAudioSource)
        {
            Audio.volume = Audio.volume / PlayerPrefs.GetFloat("MASTER_VOLUME");
        }

        PlayerPrefs.SetFloat("MASTER_VOLUME", val);

        AudioSource[] EveryAudioSourceTwo = FindObjectsOfType<AudioSource>();

        foreach (AudioSource Audio in EveryAudioSourceTwo)
        {
            Audio.volume = Audio.volume * PlayerPrefs.GetFloat("MASTER_VOLUME");
        }
    }
}