/**
 * LoadingController.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : Singleton<LoadingController>
{
    #region Fields

    [SerializeField] private float _timeToWait = 0;
    [SerializeField] private GameObject _loadingGo;

//    private bool _first = true;
    private bool _loading;
    #endregion

    #region Unity Functions

    #endregion

    #region Other Functions
    public void LoadScene(bool special = false)
    {
        Debug.Log("LoadScene [" + LoadLevel.SceneName + "] || _loading status: "+ _loading + " (it should be false)");

        if (_loading) return;
        _loading = true;

//        if (SceneManager.GetActiveScene().name == Settings.I.Get.InitSceneName)
//        {
//            Timing.RunCoroutine(_Load());
//        }
//        else
//        {
            _loadingGo.SetActive(true);
            Timing.RunCoroutine(_Load());
//        }
    }

    private IEnumerator<float> _Load()
    {
        Debug.Log("Loading ["+LoadLevel.SceneName+"]");

        if (_timeToWait == 0) yield return Timing.WaitForOneFrame;
        else yield return Timing.WaitForSeconds(_timeToWait);

        var async = new AsyncOperation();

        async = SceneManager.LoadSceneAsync(LoadLevel.SceneName, LoadSceneMode.Single);

        yield return Timing.WaitUntilDone(async);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(LoadLevel.SceneName));

        _loading = false;
        _loadingGo.SetActive(false);
    }
    #endregion
}
