/**
 * ResetAll.cs
 * Created by Akeru on 07/10/2019
 */

using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetAll : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] _allScene;

    #endregion

    #region MonoBehaviour Functions

    private void OnEnable()
    {
        EventManager.ResetAll += ResetScene;
    }

    private void OnDisable()
    {
        EventManager.ResetAll -= ResetScene;
    }
    #endregion

    #region Other Functions

    private void ResetScene()
    {
        var gos = GameObject.FindObjectsOfType<GameObject>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i] != gameObject) Destroy(gos[i]);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }

    #endregion
}
