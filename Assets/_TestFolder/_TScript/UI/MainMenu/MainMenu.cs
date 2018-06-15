using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    

    
    public Button m_StartGame;
    public Button m_ExitGame;

    

    private void Awake()
    {
        m_StartGame = GameObject.Find("SinglePlayerStart").GetComponent<Button>();
        m_ExitGame = GameObject.Find("Exit").GetComponent<Button>();

    }

  
    // Use this for initialization
    void Start () {
        m_StartGame.onClick.AddListener(StartGame);
        m_ExitGame.onClick.AddListener(ExitGame);
    }
	
	// Update is called once per frame
	void Update () {

        
        
        
    }

    void StartGame()
    {
        StartCoroutine(LoadStartScene());
        
    }

    void ExitGame()
    {
        Application.Quit();
    }

    //this is changu
    IEnumerator LoadStartScene()
    {
        yield return null;

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);

        loadScene.allowSceneActivation = false;

        while (!loadScene.isDone)
        {
            Debug.LogError("progress:" + loadScene.progress);

            if (loadScene.progress >= 0.9f)
            {
                loadScene.allowSceneActivation = true;
            }

            yield return null;
        }
        
    }
}
