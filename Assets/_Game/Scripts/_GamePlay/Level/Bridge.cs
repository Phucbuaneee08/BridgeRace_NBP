using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BridgeBrick brickOnBridge;
    [SerializeField] private Transform parentBrick;
    [SerializeField] private Transform EndBridgeBox;

    private List<BridgeBrick> bridgeBricks = new List<BridgeBrick>();
    private int brickQuantity;
    private Vector3 offset;
    private float brickWeight = .7f;
    private float brickHeight = 0.3f;
    void Start()
    {
        brickQuantity = 16;
        offset = new Vector3(0, brickHeight, brickWeight);
        SpawnBrickOnBridge();
    }
    private void SpawnBrickOnBridge()
    {
        for(int i = -12; i <= brickQuantity; i++) {
            BridgeBrick newBricks =  Instantiate(brickOnBridge, parentBrick.position + i*offset, Quaternion.identity);
            newBricks.transform.parent = parentBrick;
            bridgeBricks.Add(newBricks);
        }
    }
    public Transform GetEndBridgeBox()
    {
        return this.EndBridgeBox.transform;
    }
    public int CountBrickByColor(ColorType botColor)
    {
        int brickCount = 0;
        for(int i = 0; i < bridgeBricks.Count; i++)
        {
            if (bridgeBricks[i].CheckColor(botColor))
            {
                brickCount += 1;
            }
        }
      
        return brickCount;
    }
    // Update is called once per frame
    
}
