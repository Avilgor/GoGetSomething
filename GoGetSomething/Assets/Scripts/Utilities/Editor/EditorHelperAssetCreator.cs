/**
 * EditorHelperAssetCreator.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEditor;

public class EditorHelperAssetCreator
{
    [MenuItem("Assets/Create/EditorHelper Asset")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<EditorHelperAsset>();
    }
}
