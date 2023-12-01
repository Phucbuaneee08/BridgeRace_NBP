using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void ClickPlayAgainButton()
    {
        LevelManager.Instance.LoadLevel();
        StartNewGame();
    }
    private void StartNewGame()
    {
        //Camera.main.GetComponent<CameraFL>().ChangeState(CameraState.Start);
        UIManager.Instance.mainMenuUI.SetActive(true);
        UIManager.Instance.victoryUI.SetActive(false);
        UIManager.Instance.loseUI.SetActive(false);
        UIManager.Instance.gamePlay.SetActive(false);
    }
}
