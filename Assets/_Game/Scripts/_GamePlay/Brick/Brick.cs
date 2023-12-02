using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{


    [SerializeField] ColorData colorData;
    [SerializeField] MeshRenderer brickRenderer;


    protected ColorType brickColor = ColorType.None;

    public bool CheckColor(ColorType cl)
    {
        return cl == brickColor ;
    }
    
    protected void SetRandomColor(GameObject brick, List<ColorType> colorList)
    {
        int colorIndex = Random.Range(0, colorList.Count);
        brickRenderer.material = colorData.GetMat(colorList[colorIndex]);
        brickColor = colorList[colorIndex];
    }
    public void SetColor(ColorType colorType)
    {
        brickRenderer.material = colorData.GetMat(colorType);
        brickColor = colorType;
    }
    public void SetRandomAllColors(GameObject brick)
    {
        int colorIndex = Random.Range(1, 5);
        brickRenderer.material = colorData.GetMat((ColorType)colorIndex);
        brickColor = (ColorType)colorIndex;
    }
   

}
