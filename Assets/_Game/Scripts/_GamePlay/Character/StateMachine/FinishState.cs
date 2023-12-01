using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    public void OnEnter(Bot t)
    {
        t.BuildBridge();
    }

    public void OnExecute(Bot t)
    {
        if (t.BrickCount() == 0)
        {
            t.ChangeState(new PatrolState());
        }


    }

    public void OnExit(Bot t)
    {

    }

}
