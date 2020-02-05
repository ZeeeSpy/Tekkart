using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public GameObject Holder;
    private CountdownGo GameStarter;

    private float lowrange = 1.5f;
    private float highrange = 3.5f;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LoadingScreen");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowLoadingScreen(string SceneName)
    {
        StartCoroutine(Load(SceneName));
    }

    public void ShowLoadingScreen(int SceneIndex)
    {
        StartCoroutine(LoadIndex(SceneIndex));
    }

    IEnumerator Load(string SceneName)
    {
        Holder.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

        while (!operation.isDone)
        {
            yield return null;
            //we wait
        }
        AudioSource[] EveryAudioSource = FindObjectsOfType<AudioSource>();

        foreach (AudioSource Audio in EveryAudioSource)
        {
            Audio.enabled = false;
        }

        yield return new WaitForSeconds(Random.Range(lowrange, highrange));
        //Fake loading screen LUL

        //In new scene
        IfInRace();
        foreach (AudioSource Audio in EveryAudioSource)
        {
            Audio.enabled = true;
        }
    }

    IEnumerator LoadIndex(int SceneIndex)
    {
        Holder.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        while (!operation.isDone)
        {
            yield return null;
            //we wait
        }
        //In new scene

        yield return new WaitForSeconds(Random.Range(lowrange, highrange));
        //Fake loading screen LUL

        IfInRace();
    }

    private void IfInRace()
    {
        try //Game Start
        {
            GameStarter = GameObject.FindGameObjectWithTag("ThreeTwoOne").GetComponent<CountdownGo>();
            GameStarter.StartCountDown();
        } catch
        {
            //Destroy Player points when appropirate
        }
        Holder.SetActive(false);
    }
}
