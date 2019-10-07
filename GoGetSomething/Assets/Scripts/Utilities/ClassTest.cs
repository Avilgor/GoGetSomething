/**
 * ClassTest.cs
 * Created by Akeru on 21/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassTest : MonoBehaviour
{
    #region Fields

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        Debug.Log("Start");
    }

    private void Awake()
    {
        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("changed");
            SceneManager.LoadScene("ZonesTest", LoadSceneMode.Single);
        }
    }

    #endregion

    #region Other Functions

    #endregion
}
