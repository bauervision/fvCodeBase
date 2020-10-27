using UnityEngine;
using ForestVision.FV_TreeEditor;

#if UNITY_EDITOR
using UnityEditor;

public class FV_Tools : EditorWindow
{

    public static FV_Tools instance;
    private int guiSpace = 20;
    static private string prefabPath = "Assets/ForestVision/06 Specials/MyPrefabs/";
    static private string optimizedPath = "Assets/ForestVision/08 Optimized/07 MyOptimized/";


    public static void ShowEditor()
    {
        instance = (FV_Tools)EditorWindow.GetWindow(typeof(FV_Tools));
        instance.titleContent = new GUIContent("ForestVision Tools");
        instance.autoRepaintOnSceneChange = true;
        instance.titleContent = new GUIContent("FV Tools");
        //instance.maxSize = new Vector2(846f, 600f);
        //instance.minSize = instance.maxSize;
    }

    private void OnGUI()
    {

        GUILayout.BeginHorizontal("box");



        if (GUILayout.Button("Tree Tools", GUILayout.Width(160), GUILayout.MinHeight(60)))
        {
            FV_TreeTools.ShowWindow();

        }
        GUILayout.Space(guiSpace);
        if (GUILayout.Button("Reset Transform", GUILayout.Width(160), GUILayout.MinHeight(60)))
        {
            var go = Selection.activeGameObject;
            if (go == null)
            {
                if (EditorUtility.DisplayDialog("Heads Up", "Can't reset the transforms without something selected", "OK"))
                    return;
            }
            FV_EditorWindow.ResetTransformOnSelected(go);
        }
        GUILayout.Space(guiSpace);
        // ----------------- Collapse Mesh ----------------------

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

            //			InitContent ();
            //			GeneratePreviews ();

        }

        GUILayout.Space(guiSpace);
        //-------------------- Save Prefab -----------------------


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
            //			InitContent ();
            //			GeneratePreviews ();


        }

        GUILayout.Space(guiSpace);

        if (GUILayout.Button("Add\nFV_BranchRotation\nScript", GUILayout.Width(160), GUILayout.MinHeight(60)))
        {
            var go = Selection.activeGameObject;
            if (go == null)
            {
                if (EditorUtility.DisplayDialog("Heads Up", "Gotta select something before we put a script on it", "OK"))
                    return;
            }

            go.AddComponent<FV_BranchRotation>();

        }
        GUILayout.Space(guiSpace);

        if (GUILayout.Button("Add\nFV_Items\nScript", GUILayout.Width(160), GUILayout.MinHeight(60)))
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

        if (GUILayout.Button("Screen Shot", GUILayout.Width(160), GUILayout.MinHeight(60)))
        {
            FV_Screenshots.ShowWindow();
        }


    }


}
#endif
