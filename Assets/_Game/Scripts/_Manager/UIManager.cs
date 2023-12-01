using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text pointText;
     public GameObject victoryUI;
     public GameObject mainMenuUI;
     public GameObject gamePlay;
     public GameObject loseUI;


    private bool isActive;

    private void OnInit()
    {
       

    }
    void Start()
    {
        OnInit();
    }

    public void SetPoint(int point)
    {
        pointText.text = point.ToString();
    }
    public void OpenVictoryUI()
    {
        victoryUI.SetActive(true);
    }
    public void OpenMainMenuUI()
    {
        mainMenuUI.SetActive(true);

    }
    public void OpenLoseUI()
    {
        loseUI.SetActive(true);
   
    }

    public void OpenGamePlay()
    {
        gamePlay.SetActive(true);
    }
    public void ClickPlayButton()
    {
        mainMenuUI.SetActive(false);
        gamePlay.SetActive(true);
        LevelManager.Instance.OnStart();
    }
   

}