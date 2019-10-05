/**
 * SwitchZone.cs
 * Created by Akeru on 05/10/2019
 */

using UnityEngine;

public class SwitchZone : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _distanceToMove = 5;

    [SerializeField] private bool _isHorizontal;
    [SerializeField] private Transform _redTrigger, _blueTrigger;
    [SerializeField] private BoxCollider2D _redCollision, _blueCollision;

    private bool _switching;

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Other Functions

    public void Switch(PlayerController player, bool isRed)
    {
        player.MoveTo(GetFinalPosition(isRed), 1);
    }

    private Vector2 GetFinalPosition(bool isRed)
    {
        Vector2 pos = isRed ? _redTrigger.position : _blueTrigger.position;
        if (_isHorizontal) pos += Vector2.left * (isRed ? -_distanceToMove : _distanceToMove);
        else pos += Vector2.up * (isRed ? -_distanceToMove : _distanceToMove);

        return pos;
    }

    public void Close()
    {
        Debug.Log("Close Switches");

        _redCollision.enabled = true;
        _blueCollision.enabled = true;
    }

    public void Open()
    {
        Debug.Log("Open Switches");

        _redCollision.enabled = false;
        _blueCollision.enabled = false;
    }

    #endregion
}