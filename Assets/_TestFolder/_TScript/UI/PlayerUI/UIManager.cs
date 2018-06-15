using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {


    public Button m_ToMainMenu;
    private Animator m_MenuAnim;



    private void Awake()
    {
        m_ToMainMenu = GameObject.Find("MainMenuExit").GetComponent<Button>();
    }


    // Use this for initialization
    void Start()
    {
        m_MenuAnim = GameObject.Find("GameMenu").GetComponent<Animator>();
        m_ToMainMenu.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        StartCoroutine(LoadStartScene());

    }

    //this is changu
    IEnumerator LoadStartScene()
    {
        yield return null;

        AsyncOperation loadScene = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

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

   public void ShowMenu()
    {

        if(!m_MenuAnim.GetBool("Open"))
        {
            m_MenuAnim.SetBool("Open", true);
            
        }
        else if(m_MenuAnim.GetBool("Open"))
        {
            m_MenuAnim.SetBool("Open", false);
            
        }


    }

    public bool GetMenuState()
    {
        return m_MenuAnim.GetBool("Open");
    }
}
