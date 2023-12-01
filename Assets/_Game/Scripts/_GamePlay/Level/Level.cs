using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Level : MonoBehaviour
{
    
    public Transform finishBox;
    public List<Transform> startBox = new List<Transform>();
    private int botAmount;
    public Platform[] platforms;


    public void OnInit()
    {
        for(int i =0; i< platforms.Length; i++) {
            platforms[i].OnInit();
            Debug.Log(i);
        }
    }
    
}
