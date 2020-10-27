using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace ForestVision.FV_TreeEditor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FV_Tree))]
    public class LevelInspector : Editor
    {


        private FV_Items _itemSelected;
        private Texture2D _itemPreview;
        //private LevelPiece _pieceSelected;


        private FV_Tree _myTarget;
        private int _newTotalBranches;
        private int _newTotalCards;

        private void OnEnable()
        {
            Debug.Log("OnEnable was called...");
            _myTarget = (FV_Tree)target;

            InitTree();
            ResetTreeValues();
            SubscribeEvents();
        }

        private void InitTree()
        {

        }



        private void UpdateCurrentPieceInstance(FV_Items item, Texture2D preview)
        {
            _itemSelected = item;
            _itemPreview = preview;
            //_pieceSelected = (LevelPiece) item.GetComponent<LevelPiece>();
            Repaint();
        }

        private void SubscribeEvents()
        {
            FV_EditorWindow.ItemSelectedEvent += new FV_EditorWindow.itemSelectedDelegate(UpdateCurrentPieceInstance);
        }

        private void UnsubscribeEvents()
        {
            FV_EditorWindow.ItemSelectedEvent -= new FV_EditorWindow.itemSelectedDelegate(UpdateCurrentPieceInstance);
        }

        private void ResetTreeValues()
        {
            _newTotalBranches = _myTarget._totalBranches;

        }
        private void OnDisable()
        {
            Debug.Log("OnDisable was called...");
            UnsubscribeEvents();
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy was called...");
        }

        public override void OnInspectorGUI()
        {
            DrawLevelSizeGUI();
            DrawPieceSelectedGUI();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(_myTarget);
            }

        }
        private void DrawLevelSizeGUI()
        {
            EditorGUILayout.LabelField("Size", EditorStyles.boldLabel);
            _newTotalBranches = EditorGUILayout.IntField("Columns", Mathf.Max(1, _newTotalBranches));

            // with this variable we can enable or disable GUI
            bool oldEnabled = GUI.enabled;
            GUI.enabled = (_newTotalBranches != _myTarget.TotalBranches);
            bool buttonResize = GUILayout.Button("Collapse Tree", GUILayout.Height(2 * EditorGUIUtility.singleLineHeight));
            if (buttonResize)
            {

            }
            bool buttonReset = GUILayout.Button("Reset");
            if (buttonReset)
            {
                ResetTreeValues();
            }
            GUI.enabled = oldEnabled;
        }

        private void DrawLevelDataGUI()
        {
            EditorGUILayout.LabelField("Tree Statistics", EditorStyles.boldLabel);
            _myTarget._totalBranches = EditorGUILayout.IntField("Total Branches", Mathf.Max(0, _myTarget._totalBranches));
            _myTarget._baseTrunkMat = (Material)EditorGUILayout.ObjectField("Background", _myTarget._baseTrunkMat, typeof(Material), false);
            _myTarget._baseFoliageMat = (Material)EditorGUILayout.ObjectField("Foliage", _myTarget._baseFoliageMat, typeof(Material), false);
        }



        private void DrawPieceSelectedGUI()
        {
            EditorGUILayout.LabelField("Piece Selected", EditorStyles.boldLabel);
            if (_itemSelected == null)
            {
                EditorGUILayout.HelpBox("No piece selected!", MessageType.Info);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField(new GUIContent(_itemPreview), GUILayout.Height(40));
                EditorGUILayout.LabelField(_itemSelected.itemName);
                EditorGUILayout.EndVertical();
            }
        }


    }
}
#endif