using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadingScreenButton()
    {
        StartCoroutine(LoadingScreen());
    }
    public IEnumerator LoadingScreen()
    {
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(1);
    }
    
}
