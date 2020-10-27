using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;


namespace ForestVision.FV_TreeEditor
{
    public class FV_EditorWindow : EditorWindow
    {
        public static FV_EditorWindow instance;
        private List<FV_Items.Category> _categories;
        private List<string> _categoryLabels;
        private FV_Items.Category _categorySelected;

        private Transform[] selectedTransforms;


        static private string _path = "Assets/ForestVision";
        //static private string previewPath = "Assets/ForestVision/09 Scripts/Tools/TreeCreator/Editor/Resources/Assets/Previews";
        //static private string prefabAssetPath = "Assets/ForestVision/06 Specials/MyPrefabs/SourceMeshes/";
        static private string prefabPath = "Assets/ForestVision/06 Specials/MyPrefabs/";
        static private string optimizedPath = "Assets/ForestVision/08 Optimized/07 MyOptimized/";

        private List<FV_Items> _items;
        private Dictionary<FV_Items.Category, List<FV_Items>> _categorizedItems;
        private Dictionary<FV_Items, Texture2D> _previews;
        private Vector2 _scrollPosition;
        private const float ButtonWidth = 140;
        private const float ButtonHeight = 150;
        private int guiSpace = 20;

        public delegate void itemSelectedDelegate(FV_Items item, Texture2D preview);

        public static event itemSelectedDelegate ItemSelectedEvent;

        //private string CollapseMesh = "Collapse Tree Arrangement?";
        private bool collapse;
        public string myCustomName = " ";

        private void OnEnable()
        {
            if (_categories == null)
                InitCategories();


            if (_categorizedItems == null)
                InitContent();


        }

        private void InitCategories()
        {
            _categories = FV_edUtils.GetListFromEnum<FV_Items.Category>();
            _categoryLabels = new List<string>();
            foreach (FV_Items.Category category in _categories)
            {
                _categoryLabels.Add(category.ToString());
            }
        }

        private void InitContent()
        {
            // Set the ScrollList
            _items = FV_edUtils.GetAssetsWithScript<FV_Items>(_path);

            _categorizedItems = new Dictionary<FV_Items.Category, List<FV_Items>>();
            _previews = new Dictionary<FV_Items, Texture2D>();
            // Init the Dictionary
            foreach (FV_Items.Category category in _categories)
                _categorizedItems.Add(category, new List<FV_Items>());


            // Assign items to each category 
            foreach (FV_Items item in _items)
                _categorizedItems[item.category].Add(item);

            GeneratePreviews();
        }

        private void DrawScroll()
        {
            if (_categorizedItems[_categorySelected].Count == 0)
            {
                EditorGUILayout.HelpBox("This category is empty!", MessageType.Info);
                return;
            }
            int rowCapacity = Mathf.FloorToInt(position.width / (ButtonWidth));
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            int selectionGridIndex = -1;
            selectionGridIndex = GUILayout.SelectionGrid(
                selectionGridIndex,
                GetGUIContentsFromItems(),
                rowCapacity,
                GetGUIStyle());
            GetSelectedItem(selectionGridIndex);
            GUILayout.EndScrollView();
        }

        private void DrawTabs()
        {
            int index = (int)_categorySelected;
            index = GUILayout.Toolbar(index, _categoryLabels.ToArray());
            _categorySelected = _categories[index];
        }

        private void GeneratePreviews()
        {
            AssetPreview.SetPreviewTextureCacheSize(1024);
            foreach (FV_Items item in _items)
            {
                if (!_previews.ContainsKey(item))
                {
                    Texture2D preview = AssetPreview.GetAssetPreview(item.gameObject);
                    if (preview != null)
                        _previews.Add(item, preview);
                }
            }
        }

        public static void ShowEditor()
        {
            instance = (FV_EditorWindow)EditorWindow.GetWindow(typeof(FV_EditorWindow));
            instance.titleContent = new GUIContent("ForestVision Editor");
            instance.autoRepaintOnSceneChange = true;
            instance.titleContent = new GUIContent("FV Editor");

        }
        public static void ResetTransformOnSelected(GameObject selObj)
        {
            GameObject newParent = new GameObject();
            newParent.name = selObj.name;
            newParent.transform.position = new Vector3(0, 0, 0);
            selObj.transform.SetParent(newParent.transform);
        }



        private void OnGUI()
        {

            EditorGUILayout.LabelField("ForestVision Tree Creator and Asset Collection Browser", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawTabs();
            EditorGUILayout.HelpBox("Select an asset to add it to the scene. Newly created objects will by default be the child of anything selected. New objects will be placed at (0,0,0) of its parent object.", MessageType.None);
            DrawScroll();

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal("box");


            #region Tree Tools
            if (GUILayout.Button("Tree Tools", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {
                FV_TreeTools.ShowWindow();

            }
            GUILayout.Space(guiSpace);
            #endregion

            #region Reset Transform
            if (GUILayout.Button("Reset Transform", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {
                var go = Selection.activeGameObject;
                if (go == null)
                {
                    if (EditorUtility.DisplayDialog("Heads Up", "Can't reset the transforms without something selected", "OK"))
                        return;
                }
                ResetTransformOnSelected(go);
            }
            GUILayout.Space(guiSpace);

            #endregion

            #region Add FV Items Script
            if (GUILayout.Button("Add FV_Items Script", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {
                var go = Selection.activeGameObject;
                if (go == null)
                {
                    if (EditorUtility.DisplayDialog("Heads Up", "Gotta select something before we put a script on it", "OK"))
                        return;
                }

                go.AddComponent<FV_Items>();

            }
            GUILayout.Space(guiSpace);
            #endregion

            #region Collapse Mesh


            if (GUILayout.Button("New Optimized", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {
                var go = Selection.activeGameObject;
                if (go == null)
                {
                    if (EditorUtility.DisplayDialog("Heads Up", "Can't collapse without something selected", "OK"))
                        return;
                }

                FV_Collapse.CombineMeshes(go);

                foreach (GameObject tree in FV_Collapse.childrenOfTree)
                {
                    DestroyImmediate(tree.gameObject);
                }

                go.GetComponent<FV_Items>().category = FV_Items.Category.Optimized;
                go.GetComponent<FV_Items>().itemName = go.name;
                //string nameStamp = string.Format ("{0}_FV_{1}x", "My", System.DateTime.Now.ToString ("MM-dd-yyyy_mm-ss"));
                PrefabUtility.SaveAsPrefabAsset(Selection.activeGameObject, optimizedPath + go.name + ".prefab");

                InitContent();
                GeneratePreviews();

            }
            GUILayout.Space(guiSpace);
            #endregion

            #region Save Prefab
            if (GUILayout.Button("New Prefab", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {

                var go = Selection.activeGameObject;
                if (go == null)
                {
                    if (EditorUtility.DisplayDialog("Heads Up", "Gotta select something to prefab", "OK"))
                        return;
                }

                go.GetComponent<FV_Items>().category = FV_Items.Category.Trees;
                //string nameStamp = string.Format ("{0}_FV_{1}x", "My", System.DateTime.Now.ToString ("MM-dd-yyyy_mm-ss"));
                //var prefab = PrefabUtility.CreatePrefab (prefabPath + go.name + ".prefab", Selection.activeGameObject);
                PrefabUtility.SaveAsPrefabAsset(Selection.activeGameObject, prefabPath + go.name + ".prefab");
                InitContent();
                GeneratePreviews();


            }
            GUILayout.Space(guiSpace);
            #endregion

            #region Screenshotter
            if (GUILayout.Button("Screen Shot", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {
                FV_Screenshots.ShowWindow();
            }
            GUILayout.Space(guiSpace);

            #endregion

            #region Refresh Database
            if (GUILayout.Button("Refresh Database", GUILayout.Width(160), GUILayout.MinHeight(60)))
            {
                InitContent();
                GeneratePreviews();
            }
            #endregion
            /*
			if (GUILayout.Button ("Show Help", GUILayout.Width (160), GUILayout.MinHeight (60))) {
				if (EditorUtility.DisplayDialog ("Editor Help", "All assets are located within: Assets/ForestVision/" +
				                                 "\n" +
				                                 "\nTo add your own models to this collection, simply:" +
				                                 "\n" +
				                                 "\n1) Place them anywhere inside the: Assets/ForestVision/ folder" +
				                                 "\n2) Assign the FV_Item script to your model" +
				                                 "\n3) Set the Category in the FV_Script, ie = Prefabs, Optimized, Flora, etc., as appropriate." +
				                                 "\n4) Set the Item Name in the FV_Scripts to something meaningful to you." +
				                                 "\n" +
				                                 "\n[ Refresh Database ] will re-generate thumbnails if you add new assets and they don't appear in the browser right away." +
				                                 "\n" +
				                                 "[ New Optimized ] will collapse your selected tree creations down into a single mesh, while preserving materials.   It will save the tree into the:  ForestVision/08 Optimized/07 MyOptimized folder, and assign it's Editor Category so it will show up here under the Optimized tab." +
				                                 "\n" +
				                                 "[ New Prefab ] will simply save your selected tree arrangement as a new prefab inside: ForestVision/ 06 Specials/ MyPrefabs. Nothing will be collapsed. Everything will be preserved.", "OK"))
					return;
			}
			*/

            GUILayout.EndHorizontal();


        }

        private GUIContent[] GetGUIContentsFromItems()
        {
            List<GUIContent> guiContents = new List<GUIContent>();

            int totalItems = _categorizedItems[_categorySelected].Count;
            for (int i = 0; i < totalItems; i++)
            {
                GUIContent guiContent = new GUIContent();
                guiContent.text = _categorizedItems[_categorySelected][i].itemName;

                if (_previews[_categorizedItems[_categorySelected][i]] != null)
                {
                    guiContent.image = _previews[_categorizedItems[_categorySelected][i]];
                }


                guiContents.Add(guiContent);
            }

            return guiContents.ToArray();
        }

        private GUIStyle GetGUIStyle()
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.button);
            guiStyle.alignment = TextAnchor.LowerCenter;
            guiStyle.imagePosition = ImagePosition.ImageAbove;
            guiStyle.fixedWidth = ButtonWidth;
            guiStyle.fixedHeight = ButtonHeight;
            return guiStyle;
        }

        private void GetSelectedItem(int index)
        {
            if (index != -1)
            {
                FV_Items selectedItem = _categorizedItems[_categorySelected][index];
                Debug.Log("GetSelectedItem: " + _categorizedItems[_categorySelected].Count);
                GameObject obj = PrefabUtility.InstantiatePrefab(selectedItem.gameObject) as GameObject;
                obj.name = "FV_" + selectedItem.itemName;
                if (Selection.activeTransform != null)
                {
                    obj.transform.parent = Selection.activeTransform;
                    obj.transform.localPosition = Vector3.zero;


                }

                if (ItemSelectedEvent != null)
                {
                    ItemSelectedEvent(selectedItem, _previews[selectedItem]);
                }
            }
        }

        private void Update()
        {

            //     if (_previews.Count == 0)
            //     {
            //         GeneratePreviews();
            //     }
        }



    }
}
#endif