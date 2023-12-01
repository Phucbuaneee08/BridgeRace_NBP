using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrick : Brick
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.PLAYER_TAG) || other.CompareTag(Const.BOT_TAG))
        {
            Character crt = other.GetComponent<Character>();
            if (crt.color == brickColor || crt.color == ColorType.Gray )
            {
                crt.AddBrick();
                StartCoroutine(ChangeColor());
            }

        }
    }
    private IEnumerator ChangeColor()
    {
        SetColor(ColorType.None);
        yield return new WaitForSeconds(3f);
        SetRandomAllColors(gameObject);
    }
}
