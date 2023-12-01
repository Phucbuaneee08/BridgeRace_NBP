using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
   
    public void ClickPlayButton()
    {
        Debug.Log("click");
        UIManager.Instance.mainMenuUI.SetActive(false);
        LevelManager.Instance.OnStart();
        UIManager.Instance.gamePlay.SetActive(true);
    }

}
