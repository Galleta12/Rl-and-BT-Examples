using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAgentMaster : MonoBehaviour
{
    public BehaviourTree tree;

       

    public virtual void Start()
    {
        //clonnign to avoid any potential error when add more objects with the same tree
        tree = tree.Clone();
        tree.Bind();
    }

   

    void Update()
    {
        
        if(tree){

            tree.Update();
        }
    

        //Debug.Log("Tree state:" + tree.treeState);

    
    
    
    }




}
