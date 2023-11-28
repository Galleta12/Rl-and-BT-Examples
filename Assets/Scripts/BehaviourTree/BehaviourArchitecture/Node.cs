using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Node : ScriptableObject
{
    
        
        
        
       public BlackBoard blackBoard;
       public BlackBoardRL blackBoardRL;
       
       public GlobalBlackBoard globalBlackBoard;
       
    
        
        public enum State{Running, Failure, Succes}
        [HideInInspector]public State state = State.Running;
        [HideInInspector] public Vector2 position;
        [HideInInspector]public bool started = false;
        public int sortOrder = 0;
        public string guid;
        [HideInInspector] public Vector2 positon;
        //internal positon on the treeview
      

        
        public State Update() {
            if(!started){
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if(state == State.Failure || state == State.Succes){
                OnStop();
                started = false;
            }
            
            return state;

        }

        public virtual Node Clone(){
            return Instantiate(this);
        }


        public void Abort() {
            BehaviourTree.Traverse(this, (node) => {
                node.started = false;
                node.state = State.Running;
                node.OnStop();
            });
        }

        protected abstract void OnStart();
        protected abstract State OnUpdate();
        protected abstract void OnStop();
    
}


