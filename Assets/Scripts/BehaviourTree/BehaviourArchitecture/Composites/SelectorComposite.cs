using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorComposite : CompositeNode
{   

    int current;
    protected override void OnStart()
    {
         current = 0;
    }
    protected override State OnUpdate()
    {
            for (int i = current; i < childrens.Count; ++i) {
                current = i;
                var child = childrens[current];

                switch (child.Update()) {
                    case State.Running:
                        return State.Running;
                    case State.Succes:
                        return State.Succes;
                    case State.Failure:
                        continue;
                }
            }

            return State.Failure;
    }

    protected override void OnStop()
    {
        
    }

}
