using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private int brickAmount;
    public void OnEnter(Bot t)
    {
        t.FindBrick();
        t.ChangeAnim(Const.ANIM_RUN);
        brickAmount = Random.Range(4, 10);
    }

    public void OnExecute(Bot t)
    {
        
        if (t.BrickCount() > brickAmount) t.ChangeState(new BuildBridgeState());
        else if(t.CheckDistance())  t.FindBrick();
    }

    public void OnExit(Bot t)
    {

    }

}
