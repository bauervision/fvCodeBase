﻿using UnityEngine;

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

namespace ForestVision.FV_TreeEditor
{
    public static class FV_edUtils
    {


        public static void RandomAll()
        {
            Debug.Log("All Trees Randomized");
        }
        public static void NewTree()
        {

            GameObject FV_editor_Tree = new GameObject("FV_Tree");
            FV_editor_Tree.transform.position = Vector3.zero;
            FV_editor_Tree.AddComponent<FV_Tree>();
        }



        public static List<T> GetAssetsWithScript<T>(string path) where T : MonoBehaviour
        {
            T tmp;
            string assetPath;
            GameObject asset;
            List<T> assetList = new List<T>();
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
            for (int i = 0; i < guids.Length; i++)
            {
                assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
                tmp = asset.GetComponent<T>();
                if (tmp != null)
                {
                    assetList.Add(tmp);
                }
            }
            return assetList;
        }

        public static List<T> GetListFromEnum<T>()
        {
            List<T> enumList = new List<T>();
            System.Array enums = System.Enum.GetValues(typeof(T));
            foreach (T e in enums)
            {
                enumList.Add(e);
            }
            return enumList;
        }




    }
}

#endif