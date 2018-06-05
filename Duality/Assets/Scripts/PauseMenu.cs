using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Settings;
    public GameObject player;
    custom_FPInput input;
    public GameObject icons;
    vp_SimpleHUD HUD;
    vp_SimpleCrosshair crosshair;
    public EventSystem es;
    private GameObject StoreSelected;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        HUD = player.GetComponent<vp_SimpleHUD>();
        crosshair = player.GetComponent<vp_SimpleCrosshair>();
        input = player.GetComponent<custom_FPInput>();
        StoreSelected = es.firstSelectedGameObject;
        
        Resume();
        //Pause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        //if (es.currentSelectedGameObject != StoreSelected)
        //{
        //    if (es.currentSelectedGameObject != null)
        //    {
        //        es.SetSelectedGameObject(StoreSelected);
        //    }
        //    else
        //    {
        //        StoreSelected = es.currentSelectedGameObject;
        //    }
        //}
    }

    public void Resume()
    {
        GameIsPaused = false;
        Settings.SetActive(false);
        pauseMenuUI.SetActive(false);
        vp_Utility.LockCursor = true;
        input.MouseCursorForced = false;
        icons.SetActive(true);
        HUD.enabled = true;
        crosshair.Hide = false;
        Time.timeScale = 1f;
        

    }

    void Pause()
    {
        
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
        vp_Utility.LockCursor = false;
        input.MouseCursorForced = true;
        icons.SetActive(false);
        HUD.enabled = false;
        crosshair.Hide = true;
        Time.timeScale = 0f;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("UI_UX");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        SceneManager.LoadScene("MainMenu");
    }


}


