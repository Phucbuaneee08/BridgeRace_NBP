using Scriptable;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DropBrick : Brick
{
    [SerializeField] private GameObject parent;
    [SerializeField] private Rigidbody rb;
    private void Start()
    {
        SetColor(ColorType.Gray);
        Debug.Log(Random.insideUnitSphere);
        rb.AddForce(Random.insideUnitSphere*Const.BRICK_FORCE,ForceMode.Impulse);
        StartCoroutine(OnDespawn());
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.PLAYER_TAG) || other.CompareTag(Const.BOT_TAG))
        {
            Character crt = other.GetComponent<Character>();
            crt.AddBrick();
            Destroy(parent);
        }
    }

    private IEnumerator OnDespawn()
    {
        yield return new WaitForSeconds(6f);
        Destroy(parent);
    }
   

}
