using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

public class NodeView : UnityEditor.Experimental.GraphView.Node

{
    
    public Action<NodeView> OnNodeSelected;
    public Node node;
    public Port input;
    public Port output;

    public NodeView(Node node) : base("Assets/Editor/NodeView.uxml"){
        this.node = node;
        this.title = node.name;

        this.viewDataKey = node.guid;

        style.left = node.positon.x;
        style.top = node.positon.y;


        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();
    }

    
    
    
    private void SetupClasses() {
            if (node is ActionNode) {
                AddToClassList("action");
            } else if (node is CompositeNode) {
                AddToClassList("composite");
            } else if (node is DecoratorNode) {
                AddToClassList("decorator");
            } else if (node is RootNode) {
                AddToClassList("root");
            }
    }
    
    private void CreateInputPorts()
    {
        
        if(node is ActionNode){

            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        
        }

        else if(node is CompositeNode){
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

        }


        else if (node is DecoratorNode){
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

        }

        else if (node is RootNode){
            //input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));

        }
    

        if(input !=null){
            input.portName = "";
            input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input);
        }


    
    
    
    }
    private void CreateOutputPorts()
    {
        
        if(node is ActionNode){
           
            //action node doesnt have child
        }
        
        else if(node is CompositeNode){
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }


        else if (node is DecoratorNode){
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        else if (node is RootNode){
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }



        if(output !=null){
            output.portName = "";
            output.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(output);
        }
    
    
    }


    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(node, "Behaviour Tree (Set Position");
        node.positon.x = newPos.xMin;
        node.positon.y = newPos.yMin;
        EditorUtility.SetDirty(node);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        if(OnNodeSelected !=null){
            OnNodeSelected.Invoke(this);
        }
    }

    public void SortChildren() {
        if (node is CompositeNode composite) {
            composite.childrens.Sort(SortByHorizontalPosition);
        }
    }

    private int SortByHorizontalPosition(Node left, Node right) {
            return left.position.x < right.position.x ? -1 : 1;
    }

        public void UpdateState() {

            RemoveFromClassList("running");
            RemoveFromClassList("failure");
            RemoveFromClassList("success");

            if (Application.isPlaying) {
                switch (node.state) {
                    case Node.State.Running:
                        if (node.started) {
                            AddToClassList("running");
                        }
                        break;
                    case Node.State.Failure:
                        AddToClassList("failure");
                        break;
                    case Node.State.Succes:
                        AddToClassList("success");
                        break;
                }
            }
        }

}