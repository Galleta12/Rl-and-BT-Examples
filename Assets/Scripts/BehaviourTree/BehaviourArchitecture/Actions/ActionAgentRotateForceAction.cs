using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionAgentRotateForceAction : ActionNode
{
    

    
    protected override void OnStart()
    {
        
    }

    protected override State OnUpdate()
    {
        globalBlackBoard.agentObject.transform.Rotate(globalBlackBoard.rotateDir, Time.fixedDeltaTime * 300f);
        globalBlackBoard.m_AgentRb.AddForce(globalBlackBoard.dirToGo * globalBlackBoard.wallJumpSettings.agentRunSpeed,
            ForceMode.VelocityChange);
        
        
        return State.Succes;
     

    }
    protected override void OnStop()
    {
    
    }



}