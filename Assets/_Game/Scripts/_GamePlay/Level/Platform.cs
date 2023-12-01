using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Platform : MonoBehaviour
{


    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private MeshRenderer brickRenderer;
    [SerializeField] private Transform parentBricks;
    [SerializeField] private int column = 7;
    [SerializeField] private int row = 5;


    public List<GameObject> platformBricks = new List<GameObject>();
    public List<Bridge> listBridge = new List<Bridge>();
    public List<Transform> listDess = new List<Transform>();
    public List<ColorType> colorTypes = new List<ColorType>();
    

    

  

    public void OnInit()
    {
       
        SpawnAllBricks();
        if (this.gameObject.CompareTag(Const.NEXT_PLATFORM))
        {
            for (int i = 0; i < platformBricks.Count; i++)
            { 
                   platformBricks[i].SetActive(false);
            }
        }
        
    }
    // Update is called once per frame

    private void SpawnBrickByColor(ColorType color)
    {
        for(int i = 0; i<platformBricks.Count;i++)
        {
            if (platformBricks[i].GetComponent<Brick>().CheckColor(color))
            {
                platformBricks[i].SetActive(true);
            }
        }
    }

    private void SpawnAllBricks()
    {
        for(int i = -column;i<= column; i++)
        {
            for(int j = -row;j<= row - 1; j++)
            {
                GameObject brick = Instantiate(brickPrefab,transform.position + new Vector3(i*2.5f,0,j*2+1),Quaternion.identity);
                brick.GetComponent<Brick>().SetRandomAllColors(brick);
                brick.transform.parent = parentBricks;
                platformBricks.Add(brick);
            }
        }
        
    }

    public Transform FindBridgeByID(int id)
    {
        return listBridge[id].transform;
    }
    public Transform FindBridgeByColor(ColorType color)
    {
        Bridge chooseBridge = null;
        int maxBrick = int.MinValue;
        int currentBrick;
        for(int i = 0; i < listBridge.Count; i++)
        {
            currentBrick = listBridge[i].CountBrickByColor(color);
            if (currentBrick > maxBrick)
            {
                maxBrick = currentBrick;
                chooseBridge = listBridge[i];
            }
        }
        return chooseBridge.transform;
    }
    public GameObject FindNeareastBrick(Transform bot, ColorType color)
    {
      
        GameObject nearestBrick =null;
        float minDistance = int.MaxValue;
        float distance;
    
        for(int i= 0; i < platformBricks.Count; i++)
        {
            if (platformBricks[i].GetComponent<Brick>().CheckColor(color)) {
                
                distance = Vector3.Distance(platformBricks[i].transform.position, bot.position);
                if (distance < minDistance) {
                    minDistance = distance;
                    nearestBrick = platformBricks[i];
                    
                }

            }
            
        }
        
        return nearestBrick;
    }

    /*------------------------------------------------------
        CHECK COLLISION OF PLAYER AND BOT TO EACH PLATFORM
     *-----------------------------------------------------*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Const.PLAYER_TAG) || other.CompareTag(Const.BOT_TAG))
        {
            if (this.gameObject.CompareTag(Const.NEXT_PLATFORM))
            {
                SpawnBrickByColor(other.GetComponent<Character>().color);
                
            }  
        }
        if (other.CompareTag(Const.BOT_TAG)){
            Bot bot = other.GetComponent<Bot>();
            if (this.gameObject.CompareTag(Const.START_PLATFORM)) { 
             
                if (bot.BrickCount() > 0)
                    bot.SetDes(LevelManager.Instance.currentLevel.finishBox);
                else
                {
                    bot.SetDes(LevelManager.Instance.currentLevel.platforms[0].FindBridgeByColor(bot.color));
                }
            }
            if (this.gameObject.CompareTag(Const.NEXT_PLATFORM))
            {
                bot.SetPlatform(this);
            }
            if (this.gameObject.CompareTag(Const.WIN_PLATFORM))
            {
                UIManager.Instance.OpenLoseUI();
            }
        }
        if (other.CompareTag(Const.PLAYER_TAG))
        {
            if (this.gameObject.CompareTag(Const.WIN_PLATFORM))
            {
                other.GetComponent<Player>().OnVictory();
            }
        }
    }
    private void DeActiveBricks()
    {

    }

    private void LimitArea()
    {

    }


}
