using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrioritySelector : CompositeNode
{
    
    Node[] nodeArray;
    public int current;

    private bool isOrdered = false;
    public bool setOrder = false;
    
    protected override void OnStart()
    {
        current = 0;  
        isOrdered = setOrder;  
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        
        if(!isOrdered){
            OrderNodes();
            isOrdered = true;
            setOrder = true;
        }
        
        
        for (int i = current; i < childrens.Count; ++i) {
            
            State childState = childrens[current].Update();
            switch(childState){
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


    void OrderNodes()
    {
        nodeArray = childrens.ToArray();
        Sort(nodeArray, 0, childrens.Count - 1);
        childrens  = new List<Node>(nodeArray);
    }

    int Partition(Node[] array, int low,
                                int high)
    {
        Node pivot = array[high];

        int lowIndex = low - 1;

        //2. Reorder the collection.
        for (int j = low; j < high; j++)
        {
            if (array[j].sortOrder <= pivot.sortOrder)
            {
                lowIndex++;

                Node temp = array[lowIndex];
                array[lowIndex] = array[j];
                array[j] = temp;
            }
        }

        Node temp1 = array[lowIndex + 1];
        array[lowIndex + 1] = array[high];
        array[high] = temp1;

        return lowIndex + 1;
    }

    void Sort(Node[] array, int low, int high)
    {
        if (low < high)
        {
            int partitionIndex = Partition(array, low, high);
            Sort(array, low, partitionIndex - 1);
            Sort(array, partitionIndex + 1, high);
        }
    }



}
