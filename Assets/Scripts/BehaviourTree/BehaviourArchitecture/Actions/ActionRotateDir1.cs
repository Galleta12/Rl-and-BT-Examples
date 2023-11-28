using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionRotateDir1 : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
        if(globalBlackBoard.rotateDirAction==1){
            globalBlackBoard.rotateDir = globalBlackBoard.agentObject.transform.up * -1f;
            return State.Succes;
        }        

        return State.Failure;

    }
    protected override void OnStop()
    {
    
    }

}