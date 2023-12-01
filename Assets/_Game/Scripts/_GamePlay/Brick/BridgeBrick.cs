using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBrick : Brick
{
    private Character characterComponent;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.PLAYER_TAG) || other.CompareTag(Const.BOT_TAG))
        {
        
            characterComponent = other.GetComponent<Character>();
          
            if (!CheckColor(characterComponent.color) && characterComponent.CheckBrick())
            {
                characterComponent.RemoveOneBrick();
                SetColor(characterComponent.color);

            }

        }
    }
}
