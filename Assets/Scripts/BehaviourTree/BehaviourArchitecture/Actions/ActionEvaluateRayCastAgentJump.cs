using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionEvaluateRayCastAgentJump : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
       
       
        if ((!Physics.Raycast(globalBlackBoard.m_AgentRb.position, Vector3.down, 20))
            || (!Physics.Raycast(globalBlackBoard.m_ShortBlockRb.position, Vector3.down, 20)))
        {
         
            
            globalBlackBoard.OnSetRewardEndEpisode();
            return State.Failure;
        }

        return State.Succes;

    }
    protected override void OnStop()
    {
    
    }



}