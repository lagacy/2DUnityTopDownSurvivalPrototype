using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {


    public Button m_ToMainMenu;
    public bool m_ManagerModeActif = false;
    public GameObject m_TilesList;
    private Animator m_MenuAnim;
    ManagerMode m_ManagerMode;

   

     enum FadeType
    {
        FadeIn,
        FadeOut
    };


    private void OnGUI()
    {
        if(m_ManagerModeActif)
        {
            ShowManagerModeWindow();
        }
        else
        {
            HideManagerModeWindow();
        }
    }

    private void Awake()
    {
        m_ToMainMenu = GameObject.Find("MainMenuExit").GetComponent<Button>();
        
    }


    // Use this for initialization
    void Start()
    {
        m_MenuAnim = GameObject.Find("GameMenu").GetComponent<Animator>();
        m_ToMainMenu.onClick.AddListener(StartGame);
        m_ManagerMode = new ManagerMode();
        m_TilesList = GameObject.Find("TilesList");
        m_TilesList.GetComponent<CanvasGroup>().alpha = 0;
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

    public void ShowManagerModeWindow()
    {
        CanvasGroup rendGroup = m_TilesList.GetComponent<CanvasGroup>();
        if (!m_ManagerMode.GetIsActive())
        {
            Fade(rendGroup, FadeType.FadeIn, 10f);
        }

    }

    public void HideManagerModeWindow()
    {

        CanvasGroup rendGroup = m_TilesList.GetComponent<CanvasGroup>();
        if (!m_ManagerMode.GetIsActive())
        {
            Fade(rendGroup, FadeType.FadeOut, 10f);
        }
    }

    private void Fade(CanvasRenderer canvasRenderer, FadeType fadeType , float fadeSpeed)
    {
        if(fadeType == FadeType.FadeIn && canvasRenderer.GetAlpha() != 1f)
        {
            canvasRenderer.SetAlpha(Mathf.Lerp(canvasRenderer.GetAlpha(), 1f, Time.deltaTime * fadeSpeed));
        }
        else if (fadeType == FadeType.FadeOut && canvasRenderer.GetAlpha() != 0f)
        {
            canvasRenderer.SetAlpha(Mathf.Lerp(canvasRenderer.GetAlpha(), 0f, Time.deltaTime * fadeSpeed));
        }
    }

    private void Fade(CanvasGroup CanvasGroup, FadeType fadeType, float fadeSpeed)
    {
        if (fadeType == FadeType.FadeIn && CanvasGroup.alpha != 1f)
        {
            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, 1f, Time.deltaTime * fadeSpeed);
        }
        else if (fadeType == FadeType.FadeOut && CanvasGroup.alpha != 0f)
        {
            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, 0f, Time.deltaTime * fadeSpeed);
        }
    }
}
