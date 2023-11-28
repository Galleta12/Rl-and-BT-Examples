using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRLDecorator : DecoratorNode
{
    
    
    protected override void OnStart()
    {
        
    }
    protected override State OnUpdate()
    {
       switch (child.Update()) {
            case State.Running:
                    break;
            case State.Failure:
                //Debug.Log("Failure");
                //blackBoardRL.OnNegativeRewardEndEpisode();
                return State.Running;
           
            case State.Succes:
                 //Debug.Log("Succes");
                //blackBoardRL.OnPositiveReward();
                
                return State.Running;
             
        }
            return State.Running;
    }
    protected override void OnStop()
    {
        
    }
}
