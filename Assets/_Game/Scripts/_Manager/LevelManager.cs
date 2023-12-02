using Scriptable;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private Bot botPrefab;
    public Player player;
    private Bot[] listBots = new Bot[3];
    private int botAmount;
    public Level currentLevel;

    public int level = 1;
    private void Start()
    {
        UIManager.Instance.OpenMainMenuUI();
        LoadLevel();
    }
    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {

        //UIManager.Instance.SetNextLevelText(indexLevel);
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            for(int i = 0; i < listBots.Length; i++)
            {
                Destroy(listBots[i].gameObject);
            }
        }

        currentLevel = Instantiate(levels[indexLevel - 1]);
        currentLevel.OnInit();
    }
    public void SetLevel(int level)
    {
        this.level = level;
        LoadLevel();
    }
    public void NextLevel()
    {
        level++;
        LoadLevel();
    }

    public void OnInit()
    {
        //player.transform.position = currentLevel.startPoint.position;
        //player.destination = currentLevel.endPoint;
        //player.OnInit();
       
        player.ChangeColor((ColorType)1);
        player.SetPosition(currentLevel.startBox[0]);

        int rand = Random.Range(4, 7);
        for(int i = 0; i < 3; i++)
        {
            listBots[i] = Instantiate(botPrefab, currentLevel.startBox[i+1].position, Quaternion.identity);
            listBots[i].ChangeColor((ColorType)(i+2));
            listBots[i].SetPlatform(currentLevel.platforms[0]);
            listBots[i].SetDes(currentLevel.platforms[0].FindBridgeByID((i+rand)%4));
        }


    }

    private void ActiveBot()
    {
        for(int i=0;i<listBots.Length;i++) {
            listBots[i].OnInit();
            listBots[i].agent.isStopped = false;
        }
    }

    public void OnStart()
    {
        UIManager.Instance.mainMenuUI.SetActive(false);
        UIManager.Instance.gamePlay.SetActive(true);
        GameManager.Instance.ChangeState(GameState.GamePlay);
        ActiveBot();
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenVictoryUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public bool CheckNextLevel()
    {
        if (level <= levels.Count - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
