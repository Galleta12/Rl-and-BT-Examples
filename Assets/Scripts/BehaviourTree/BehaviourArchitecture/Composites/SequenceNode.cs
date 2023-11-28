using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{

        //always start with the index 0
        int current =0;

        public Node currentChild;
        protected override void OnStart()
        {
            current = 0;
        }

        protected override State OnUpdate()
        {
              for (int i = current; i < childrens.Count; ++i) {
                current = i;
                currentChild = childrens[current];
               
                switch (currentChild.Update()) {
                    case State.Running:
                        return State.Running;
                    case State.Failure:
                        return State.Failure;
                    case State.Succes:
                        continue;
                }
            }

            return State.Succes;
        }
        protected override void OnStop()
        {
            
        }

}
