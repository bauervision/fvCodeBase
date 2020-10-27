using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
//============================================================================
//
//			Used for ForestVision Menu Items
//
//============================================================================
namespace ForestVision.FV_TreeEditor
{
    public static class MenuItems
    {

        [MenuItem("Tools/ForestVision/Deprecated/Browser", false, 30)]//access to everything
        private static void ShowEditor()
        {
            FV_EditorWindow.ShowEditor();
        }

        [MenuItem("Tools/ForestVision/FV Tools", false, 30)]//access to everything
        private static void ShowTools()
        {
            FV_Tools.ShowEditor();
        }

    }
}
#endif