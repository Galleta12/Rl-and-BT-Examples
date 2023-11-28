using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBallRotate2 : ActionNode
{
    public float actionX;
    public GameObject agentObject;
    protected override void OnStart()
    {
   
        actionX = blackBoardRL.floatActionX;
        agentObject = blackBoardRL.getAgentObject();
    }

        //this action cannot fail
    protected override State OnUpdate()
    {

        if ((agentObject.transform.rotation.x < 0.25f && actionX > 0f) ||
            (agentObject.transform.rotation.x > -0.25f && actionX < 0f))
        {
            agentObject.transform.Rotate(new Vector3(1, 0, 0), actionX);
        }
        
        return State.Succes;
            
    }
    protected override void OnStop()
    {
            
    }
}
