using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionJumpAction1 : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
        if(globalBlackBoard.jumpAction == 1){
            return State.Succes;
        }        

        return State.Failure;

    }
    protected override void OnStop()
    {
    
    }

}