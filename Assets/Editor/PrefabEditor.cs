using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PrefabEditor : EditorWindow {
    public PrefabList list;
    private int viewIndex = 1;
    private string listName;
    bool createNewFlag = false;

    [MenuItem("Hopmon Tools/Prefab Editor %#e")]
    static void Init() {
        GetWindow(typeof(PrefabEditor));
    }

    void OnEnable() {
        listName = "Enter name";
        if (EditorPrefs.HasKey("ObjectPath")) {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            list = AssetDatabase.LoadAssetAtPath(objectPath, typeof(PrefabList)) as PrefabList;
        }
    }

    // Форма для создания нового списка
    private void CreateListLayout() {
        GUILayout.Space(10);
        GUILayout.BeginVertical();
        if (createNewFlag) {
            listName = EditorGUILayout.TextField(listName, GUILayout.MaxWidth(150));
            if (GUILayout.Button("Create " + listName, GUILayout.ExpandWidth(false))) {
                CreateNewList(listName);
            }
        }
        else {
            if (GUILayout.Button("Create List", GUILayout.ExpandWidth(false))) {
                createNewFlag = true;
            }
        }
        GUILayout.EndVertical();
        GUILayout.Space(10);
    }

    // Создать список префабов
    private void CreateNewList(string name) {
        viewIndex = 1;
        list = Create(name);
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = list;
    }

    private static PrefabList Create(string name)
    {
        var asset = CreateInstance<PrefabList>();
        AssetDatabase.CreateAsset(asset, "Assets/GameData/" + name + ".asset");
        AssetDatabase.SaveAssets();
        return asset;
    }

    // Форма для открытия списки
    private void OpenListLayout() {
        GUILayout.Space(10);
        if (GUILayout.Button("Open Prefab List", GUILayout.ExpandWidth(false))) {
            OpenList();
        }
        GUILayout.Space(10);
    }

    // Открыть список префабов
    private void OpenList() {
        string absPath = EditorUtility.OpenFilePanel("Select Prefab List", "Assets/GameData/", "asset");
        if (absPath.StartsWith(Application.dataPath)) {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            list = AssetDatabase.LoadAssetAtPath(relPath, typeof(PrefabList)) as PrefabList;
            if (list) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }


    void OnGUI() {
        GUILayout.BeginVertical();
        // Заголовок
        GUILayout.Label("Prefab Editor - " + listName, EditorStyles.boldLabel);
        if (list == null) {
            GUILayout.BeginHorizontal();
            CreateListLayout();
            OpenListLayout();
            GUILayout.EndHorizontal();
        }
        else {
            if (GUILayout.Button("Show List")) {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = list;
            }
        }
        GUILayout.EndVertical();

        GUILayout.Space(20);

        if (list != null) {
            GUILayout.BeginHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false))) {
                if (viewIndex > 1)
                    viewIndex--;
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Next", GUILayout.ExpandWidth(false))) {
                if (viewIndex < list.itemList.Count) {
                    viewIndex++;
                }
            }

            GUILayout.Space(60);

            if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false))) {
                AddItem();
            }
            if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false))) {
                if (list.itemList.Count > 0)
                    DeleteItem(viewIndex - 1);
            }

            GUILayout.EndHorizontal();
            if (list.itemList != null) {
                if (list.itemList.Count > 0) {
                    GUILayout.BeginHorizontal();
                    viewIndex = Mathf.Clamp(
                        EditorGUILayout.IntField("Current Prefab", viewIndex, GUILayout.ExpandWidth(false)), 1,
                        list.itemList.Count);
                    var index = viewIndex - 1;
                    //Mathf.Clamp (viewIndex, 1, list.itemList.Count);
                    EditorGUILayout.LabelField("of   " + list.itemList.Count.ToString() + "  items", "",
                        GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                    var currentItem = list.itemList[index];

            /*        list.world = (World) EditorGUILayout.EnumPopup("World", list.world, GUIStyle.none);
                    foreach (var item in list.itemList) {
                        item.world = list.world;
                    }*/
                    list.name = EditorGUILayout.TextField("Prefab List Name", list.name);

                    currentItem.name = EditorGUILayout.TextField("Prefab Name", currentItem.name);
                    currentItem.prefab =
                        EditorGUILayout.ObjectField("Transfrom", currentItem.prefab, typeof(GameObject),
                            false) as GameObject;
                    //currentItem.kind = EditorGUILayout.TextField("Kind", currentItem.kind);

                    GUILayout.Space(10);
                }
                else {
                    GUILayout.Label("This Inventory List is Empty.");
                }
            }
        }
        if (GUI.changed) {
            // EditorUtility.SetDirty(list);
        }
    }


    void AddItem() {
        PrefabItem newItem = new PrefabItem();
        newItem.name = "New Item";
        list.itemList.Add(newItem);
        viewIndex = list.itemList.Count;
    }

    void DeleteItem(int index) {
        list.itemList.RemoveAt(index);
    }
}