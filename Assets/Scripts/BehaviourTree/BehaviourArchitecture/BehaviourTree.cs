using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CreateAssetMenu()]
public class BehaviourTree : ScriptableObject
{
    
    //root node
        public Node rootNode;
        public Node.State treeState = Node.State.Running;
        //store nodes that not neccesary are attach to rootnode
        public List<Node> nodes = new List<Node>();

        public BlackBoard blackBoard = new BlackBoard();
        public BlackBoardRL blackBoardRL = new BlackBoardRL();
        
        public GlobalBlackBoard _globalBlackBoard = new GlobalBlackBoard();



        public Node.State Update() {
            if(rootNode.state == Node.State.Running){

                treeState =rootNode.Update();
            }
            return treeState;
            
        }
        //return empty list
        public List<Node> GetChildrens(Node parent){
            
            List<Node> internalChildren = new List<Node>();
            
            DecoratorNode decoratorNode =  parent as DecoratorNode;
            if(decoratorNode && decoratorNode.child !=null){
                internalChildren.Add(decoratorNode.child);
            }


            RootNode rootNode = parent as RootNode;
            if(rootNode  && rootNode.child !=null){
               internalChildren.Add(rootNode.child);
            }


            CompositeNode compositeNode = parent as CompositeNode;
            if(compositeNode){
                return compositeNode.childrens;
            }

            return internalChildren;
        }

        public static void Traverse(Node node, System.Action<Node> visiter) {
            if (node) {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }


        //set the blackboard on all of the node
        public void Bind() {
            Traverse(rootNode, node => {
                node.blackBoard = blackBoard;
                node.blackBoardRL = blackBoardRL;
                node.globalBlackBoard = _globalBlackBoard;
            });
        }

            //clone evrything to dont get error when instatnice object with tree objecets added to them
        public BehaviourTree Clone(){
              BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<Node>();
            Traverse(tree.rootNode, (n) => {
                tree.nodes.Add(n);
            });

            return tree;
        }
    
    


        public  static List<Node> GetChildren(Node parent) {
            List<Node> children = new List<Node>();

            if (parent is DecoratorNode decorator && decorator.child != null) {
                children.Add(decorator.child);
            }

            if (parent is RootNode rootNode && rootNode.child != null) {
                children.Add(rootNode.child);
            }

            if (parent is CompositeNode composite) {
                return composite.childrens;
            }

            return children;
        }

        public Node CreateNode(System.Type type){
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();

            Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying) {
                AssetDatabase.AddObjectToAsset(node, this);
            }

            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");

            AssetDatabase.SaveAssets();
            return node;

        }

        public void DeleteNode(Node node){
            Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
            nodes.Remove(node);

            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);

            AssetDatabase.SaveAssets();
        }
        //remember that the action node doesnt have childrens
        public void AddChild(Node parent, Node child){
            
            DecoratorNode decoratorNode =  parent as DecoratorNode;
            if(decoratorNode){
                Undo.RecordObject(decoratorNode , "Behaviour Tree (AddChild)");
                decoratorNode.child = child;
                  EditorUtility.SetDirty(decoratorNode );
            }

            RootNode rootNode = parent as RootNode;
            if(rootNode){
                  Undo.RecordObject(rootNode , "Behaviour Tree (AddChild)");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode );
            }
            
            CompositeNode compositeNode = parent as CompositeNode;
            if(compositeNode){
                Undo.RecordObject(compositeNode, "Behaviour Tree (AddChild)");
                compositeNode.childrens.Add(child);
                EditorUtility.SetDirty(compositeNode);
            }
            


        
        }


        public void RemoveChild(Node parent, Node child){
            DecoratorNode decoratorNode =  parent as DecoratorNode;
            if(decoratorNode){
                 Undo.RecordObject(decoratorNode, "Behaviour Tree (RemoveChild)");
                decoratorNode.child = null;
                EditorUtility.SetDirty(decoratorNode);
            }

            RootNode rootNode = parent as RootNode;
            if(rootNode){
                 Undo.RecordObject(rootNode, "Behaviour Tree (RemoveChild)");
    
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
            }

            CompositeNode compositeNode = parent as CompositeNode;
            if(compositeNode){
                 Undo.RecordObject(compositeNode, "Behaviour Tree (RemoveChild)");
                
                compositeNode.childrens.Remove(child);
                EditorUtility.SetDirty(compositeNode);
            }
        }




    



}
