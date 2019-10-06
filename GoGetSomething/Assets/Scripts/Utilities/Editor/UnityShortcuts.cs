using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class UnityShortcuts : ScriptableObject
 {
     [MenuItem("Shortcuts/Play _&1")]
     static void PlayGame()
     {
         EditorApplication.ExecuteMenuItem("Edit/Play");
     }

     [MenuItem("Shortcuts/Pause _&2")]
     static void PauseGame()
     {
         EditorApplication.ExecuteMenuItem("Edit/Pause");
     }

     [MenuItem("Shortcuts/Step _&3")]
     static void Step()
     {
         EditorApplication.ExecuteMenuItem("Edit/Step");
     }

     [MenuItem("Shortcuts/Select Settings _&s")]
     static void SelectSettings()
     {
         Debug.Log("Settings Selected");
         var settings = Find.Settings;
         Selection.activeObject = settings;
     }
}