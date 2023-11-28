using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatDecorator : DecoratorNode
{
    
    public bool restartOnSuccess = true;
    public bool restartOnFailure = false;
    
    protected override void OnStart()
    {
        
    }
    protected override State OnUpdate()
    {
       switch (child.Update()) {
            case State.Running:
                    break;
            case State.Failure:
                if (restartOnFailure) {
                    return State.Running;
                } else {
                    return State.Failure;
                }
            case State.Succes:
                if (restartOnSuccess) {
                    return State.Running;
                } else {
                    return State.Succes;
                }
            }
            return State.Running;
    }
    protected override void OnStop()
    {
        
    }
}
