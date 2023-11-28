using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionReduceJumpTime : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
       
        globalBlackBoard.jumpingTime -= Time.fixedDeltaTime;
        
        return State.Succes;
     

    }
    protected override void OnStop()
    {
    
    }



}