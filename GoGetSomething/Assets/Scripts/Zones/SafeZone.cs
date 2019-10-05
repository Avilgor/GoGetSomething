/**
 * SafeZone.cs
 * Created by Akeru on 05/10/2019
 */

using UnityEngine;

public class SafeZone : Zone
{
    #region Fields

    #endregion

    #region MonoBehaviour Functions

    private void OnEnable()
    {
        EventManager.SaveProcess += SaveProcess;
    }

    private void OnDisable()
    {
        EventManager.SaveProcess -= SaveProcess;
    }

    #endregion

    #region Other Functions

    protected override void Entered()
    {
        base.Entered();

        EventManager.OnCleanPlayer();
    }

    //TODO Maybe this shouldn't be here
    private void SaveProcess()
    {
        User.SaveProcess();
    }
    #endregion
}
