/**
 * EditorHelperAsset.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public enum Module
{
    Settings, Popups, Loading, Music, Ambient, SFX, Awards, Bridge
}

public class EditorHelperAsset : ScriptableObject
{
    public string Value;

    [TabGroup("Modules")] public List<CModules> Modules;

    [TabGroup("Scenes")] public Object PrelaunchScene;
    [TabGroup("Scenes")] public string PreviousScene;
    [TabGroup("Scenes")] public List<CProjectScenes> Scenes;

    [SerializeField] private List<GameObject> _temporallyModulesInstantiated = new List<GameObject>();

    private Object _previousSelectedObject;

    [Serializable]
    public class CProjectScenes
    {
        public Object Scene;
        public Module[] RequieredModules;

        [Button]
        public void LoadScene()
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/"+ Scene.name + ".unity");
        }
    }
    
    [Serializable]
    public class CModules
    {
        public Module Module;
        public GameObject Prefab;
    }

    public void CheckRequiredModules()
    {
        var scene = Scenes.Find(scenes => scenes.Scene.name == SceneManager.GetActiveScene().name);
        if (scene != null)
        {
            for (int i = 0; i < scene.RequieredModules.Length; i++)
            {
                if (GameObject.FindGameObjectWithTag(scene.RequieredModules[i] + "Module") == null)
                {
                    var module = Modules.Find(modules => modules.Module == scene.RequieredModules[i]);
                    var moduleGameobject = Instantiate(module.Prefab);
                    _temporallyModulesInstantiated.Add(moduleGameobject);

                    Debug.Log("Requiered <color=white>"+moduleGameobject+"</color> added!");
                }
            }
        }
    }

    public void DeleteTemporalRequieredModules()
    {
        Selection.activeObject = Find.EditorHelper;

        for (int i = 0; i < _temporallyModulesInstantiated.Count; i++)
            DestroyImmediate(_temporallyModulesInstantiated[i]);

        _temporallyModulesInstantiated.Clear();

        ReturnSelection();
    }

    //Bug Workaround
    public void SelectToRefresh()
    {
        RefreshSelection();
        Selection.activeObject = Find.EditorHelper;
    }

    public void RefreshSelection()
    {
        if(Selection.activeObject != null) _previousSelectedObject = Selection.activeObject;
    }

    public void ReturnSelection()
    {
        if(_previousSelectedObject != null)  Selection.activeObject = _previousSelectedObject;
    }
}

[InitializeOnLoad]
public static class PlayModeStateChanged
{
    static PlayModeStateChanged()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }
    
    private static void LogPlayModeState(PlayModeStateChange state)
    {
        Debug.Log(state);

        if (state == PlayModeStateChange.EnteredEditMode)
        {
            Find.EditorHelper.DeleteTemporalRequieredModules();
        }
        else if (state == PlayModeStateChange.ExitingEditMode)
        {
            Find.EditorHelper.CheckRequiredModules();
            Find.EditorHelper.SelectToRefresh();
        }
        else if (state == PlayModeStateChange.ExitingPlayMode)
        {
            Find.EditorHelper.RefreshSelection();
        }
        else if (state == PlayModeStateChange.EnteredPlayMode)
        {
            Find.EditorHelper.ReturnSelection();
        }
    }
}
