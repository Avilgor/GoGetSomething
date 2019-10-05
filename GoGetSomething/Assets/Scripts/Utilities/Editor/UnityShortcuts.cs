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

     [MenuItem("Shortcuts/Select Editor Helper _&e")]
     static void SelectEditorHelper()
     {
         Debug.Log("Editor Helper Selected");
         var settings = Find.EditorHelper;
         Selection.activeObject = settings;
     }

    [MenuItem("Shortcuts/Play-Stop, But From Prelaunch Scene _&w")]
     public static void PlayFromPrelaunchScene()
     {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            return;
        }

         var editorHelper = Find.EditorHelper;

        var sceneName = SceneManager.GetActiveScene().name;
        if (editorHelper.PrelaunchScene.name != sceneName)
        {
            Debug.Log("Find.SettingsAsset.PreviousScene: "+ editorHelper.PreviousScene);
            Debug.Log("SceneManager.GetActiveScene().name: "+ SceneManager.GetActiveScene().name);
            editorHelper.PreviousScene = SceneManager.GetActiveScene().name;
        }

//         editorHelper.CheckRequiredModules();

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/"+ Find.EditorHelper.PrelaunchScene.name+".unity");
        EditorApplication.isPlaying = true;
     }

     [MenuItem("Shortcuts/LoadPreviousScene _&q")]
     public static void LoadPreviousScene()
     {
         EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
         EditorSceneManager.OpenScene("Assets/Scenes/"+ Find.EditorHelper.PreviousScene + ".unity");
//         EditorApplication.isPlaying = true;
     }
}