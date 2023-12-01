using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    private void OnEnable()
    {
        SetLevelText(LevelManager.Instance.level);
    }
    public void SetLevelText(int level)
    {
        levelText.text = "Level " + level.ToString();
    }
}
