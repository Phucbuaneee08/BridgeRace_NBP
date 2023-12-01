using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VictoryUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text pointText;

    private void OnEnable()
    {
        SetLevelText(LevelManager.Instance.level);
    }

    public void SetPointText(int point)
    {
        pointText.text = point.ToString();
    }
    public void SetLevelText(int level)
    {
        levelText.text = "Level "+ level.ToString();  
    }
  
    private void StartNewGame()
    {
        //Camera.main.GetComponent<CameraFL>().ChangeState(CameraState.Start);
        UIManager.Instance.mainMenuUI.SetActive(true);
        UIManager.Instance.victoryUI.SetActive(false);
        UIManager.Instance.gamePlay.SetActive(false);
    }
    public void ClickNextLevelButton()
    {
        if (LevelManager.Instance.CheckNextLevel())
        {
            LevelManager.Instance.NextLevel();
            GameManager.Instance.ChangeState(GameState.MainMenu);
            StartNewGame();
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.Finish);

        }
    }
}
