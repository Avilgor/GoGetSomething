/**
 * CreateClass.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class CreateClass
{
    [MenuItem("Assets/Create/Add C# Class")]
    static void Create()
    {
        GameObject selected = Selection.activeObject as GameObject;
        if (selected == null || selected.name.Length >= 0 )
        {
            Debug.Log("Selected object not Valid");
            return;
        }

        // remove whitespace and minus
        string name = selected.name.Replace(" ", "_");
        name = name.Replace("-", "_");
        string copyPath = "Assets/" + name + ".cs";
        Debug.Log("Creating Classfile: " + copyPath);
        if (File.Exists(copyPath) == false)
        { // do not overwrite
            using (StreamWriter outfile =
                new StreamWriter(copyPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + name + " : MonoBehaviour {");
                outfile.WriteLine(" ");
                outfile.WriteLine(" ");
                outfile.WriteLine(" // Use this for initialization");
                outfile.WriteLine(" void Start () {");
                outfile.WriteLine(" ");
                outfile.WriteLine(" }");
                outfile.WriteLine(" ");
                outfile.WriteLine(" ");
                outfile.WriteLine(" // Update is called once per frame");
                outfile.WriteLine(" void Update () {");
                outfile.WriteLine(" ");
                outfile.WriteLine(" }");
                outfile.WriteLine("}");
            }
        }
        AssetDatabase.Refresh();
        selected.AddComponent(Type.GetType(name));
    }
}
