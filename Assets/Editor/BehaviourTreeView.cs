using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

using System;
using System.Linq;


public class BehaviourTreeView : GraphView
{
    
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits>{}
    BehaviourTree tree;


    public struct ScriptTemplate {
            public TextAsset templateFile;
            public string defaultFileName;
            public string subFolder;
    }


    public BehaviourTreeView(){
        
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        
        this.AddManipulator(new ContentDragger());
        
        
        this.AddManipulator(new SelectionDragger());
        
        this.AddManipulator(new RectangleSelector());
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    
    }


    
    private void OnUndoRedo() {
            PopulateView(tree);
            AssetDatabase.SaveAssets();
    }

    NodeView FindNodeView(Node node){
        return GetNodeByGuid(node.guid) as NodeView;
    }


    internal void PopulateView(BehaviourTree  tree)
    {
              this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements.ToList());
            graphViewChanged += OnGraphViewChanged;

            if (tree.rootNode == null) {
                tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }

            // Creates node view
            tree.nodes.ForEach(n => CreateNodeView(n));

            // Create edges
            tree.nodes.ForEach(n => {
                var children =  BehaviourTree.GetChildren(n);
                children.ForEach(c => {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                });
            });
    }

    ///
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        //dont
        return ports.ToList().Where(endPort=>
        endPort.direction !=startPort.direction &&
        endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        
              if (graphViewChange.elementsToRemove != null) {
                graphViewChange.elementsToRemove.ForEach(elem => {
                    NodeView nodeView = elem as NodeView;
                    if (nodeView != null) {
                        tree.DeleteNode(nodeView.node);
                    }

                    Edge edge = elem as Edge;
                    if (edge != null) {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView.node, childView.node);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null) {
                graphViewChange.edgesToCreate.ForEach(edge => {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.AddChild(parentView.node, childView.node);
                });
            }


           nodes.ForEach((n) => {
                NodeView view = n as NodeView;
                view.SortChildren();
            });
        
        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        
        
         evt.menu.AppendAction($"Create Script.../New Action Node", (a) => CreateNewScript());
        
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach(var type in types){
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }

        
        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach(var type in types){
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }


        
        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach(var type in types){
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }
        
        
        {
            var types = TypeCache.GetTypesDerivedFrom<RootNode>();
            foreach(var type in types){
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a)=>CreateNode(type));
            }
        }

    }


    void CreateNewScript(){
        string templateFile = "Assets/Editor/ScriptTemplates/NewActionScriptNode.cs.txt";
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templateFile, "NewActionNode.cs");

    }

    void CreateNode(System.Type type){
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
    }
    void CreateNodeView(Node node){
        NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
    }

       public void UpdateNodeStates() {
            nodes.ForEach(n => {
                NodeView view = n as NodeView;
                view.UpdateState();
            });
        }
}