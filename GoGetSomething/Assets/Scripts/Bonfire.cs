/**
 * Bonfire.cs
 * Created by Akeru on 06/10/2019
 */

using System.Collections.Generic;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    #region Fields

    [Tooltip("Change it in its 'SafeZone' Script")] public int ID;

    [SerializeField] private GameObject _firedGo;
    [OnValueChanged("SetBonfire")] [SerializeField] private TryToSavePopup _popup;

    private bool _interacting;
    private bool _fired;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        CheckFired();
    }

    #endregion

    #region Other Functions

    private void SetBonfire()
    {
        _popup.Bonfire = this;
    }

    public void Interact()
    {
        if (_interacting || _fired) return;
        _interacting = true;
        _popup.Show();
//        EventManager.OnBonfireInteracted(this);
    }

    public void HidePopup()
    {
        _popup.Hide();
        //        EventManager.OnBonfireInteracted(this);
    }

    public void Save()
    {
        _interacting = false;

        EventManager.OnSaveProcess();
        User.SaveProcess();
        User.SetBonfireFired(ID, PlayerController.I.gameObject.transform.position);

        CheckFired();
    }

    public void StopInteraction()
    {
        Timing.RunCoroutine(_StopInteraction());
    }

    private IEnumerator<float> _StopInteraction()
    {
        yield return Timing.WaitForOneFrame;
        yield return Timing.WaitForSeconds(0.2f);

        _interacting = false;
    }

    private void CheckFired()
    {
        _fired = User.IsBonfireFired(ID);
        _firedGo.SetActive(_fired);
    }

    #endregion
}
