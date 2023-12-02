using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    
    public void ClickPlayButton()
    {
        Debug.Log("click");
        LevelManager.Instance.OnStart();
       
    }
    public void OpenLevel(int level)
    {
        LevelManager.Instance.SetLevel(level);
        LevelManager.Instance.OnStart();
    }

}
