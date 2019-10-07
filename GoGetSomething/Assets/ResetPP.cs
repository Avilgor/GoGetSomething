/**
 * ResetPP.cs
 * Created by Akeru on 08/10/2019
 */

using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ResetPP : MonoBehaviour
{
    #region Fields

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    public void ButtonReset()
    {
        PlayerPrefs.DeleteAll();
        ObscuredPrefs.DeleteAll();

        EventManager.OnResetAll();
    }
    #endregion
}
