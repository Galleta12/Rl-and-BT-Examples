using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
public class BehaviourTreesEditor : EditorWindow
{
    //[SerializeField]
    //private VisualTreeAsset m_VisualTreeAsset = default;

    BehaviourTree tree;
    SerializedObject treeObject;
    BehaviourTreeView treeView;
    InspectorView inspectorView;
    //[MenuItem("Window/UI Toolkit/BehaviourTreesEditor")]
    [MenuItem("Behaviour Tree/Editor ...")]
    public static void OpenWindow()
    {
        BehaviourTreesEditor wnd = GetWindow<BehaviourTreesEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }


    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceId, int line) {
        if (Selection.activeObject is BehaviourTree) {
                OpenWindow();
                return true;
        }
        return false;
    }




    private void OnPlayModeStateChanged(PlayModeStateChange obj) {
            switch (obj) {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
    }
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Instantiate UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTreeEditor.uxml");
       // VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        //root.Add(labelFromUXML);
        visualTree.CloneTree(root);
        
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);


        treeView = root.Q<BehaviourTreeView>();
        treeView.OnNodeSelected = OnNodeSelectionChanged;
        
        inspectorView = root.Q<InspectorView>();

        
        if (tree == null) {
                OnSelectionChange();
            } else {
                SelectTree(tree);
        }

    }
    private void OnEnable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable() {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnSelectionChange() {
            EditorApplication.delayCall += () => {
                BehaviourTree tree = Selection.activeObject as BehaviourTree;
                if (!tree) {
                    if (Selection.activeGameObject) {
                        BTAgentMaster runner = Selection.activeGameObject.GetComponent<BTAgentMaster>();
                        if (runner) {
                            tree = runner.tree;
                        }
                    }
                }

                SelectTree(tree);
            };

    }


    
    void SelectTree(BehaviourTree newTree) {

            if (treeView == null) {
                return;
            }

            if (!newTree) {
                return;
            }

            this.tree = newTree;

       
            if (Application.isPlaying) {
                treeView.PopulateView(tree);
            } else {
                treeView.PopulateView(tree);
            }

            
            treeObject = new SerializedObject(tree);

            EditorApplication.delayCall += () => {
                treeView.FrameAll();
            };
    }

    // private void OnSelectionChange() {
    //     BehaviourTree tree = Selection.activeObject as BehaviourTree;
        
    //     //if a new bt is create. and if the asset is not ready. with the root node it can throw an error about a object that 
    //     //is not serialize
    //     //hence we need to use with the asset
    //     if(tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID())){
    //         treeView.PopulateView(tree);
    //     }

    // }


    void OnNodeSelectionChanged(NodeView nodeView){
        inspectorView.UpdateSelection(nodeView);
    }

    private void OnInspectorUpdate() {
            treeView?.UpdateNodeStates();
    }
}