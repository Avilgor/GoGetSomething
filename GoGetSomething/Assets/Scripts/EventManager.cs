/**
 * EventManager.cs
 * Created by Akeru on 13/03/2019
 * Copyright © iBoo Mobile. All rights reserved.
 */

using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Delegates
    public delegate void VoidDelegate();
    public delegate void IntDelegate(int value);
    public delegate void FloatDelegate(float value);
    public delegate void BoolDelegate(bool value);
    public delegate void StringDelegate(string value);

    public delegate void ZoneDelegate(Zone zone);
    public delegate void BonfireDelegate(Bonfire bonfire);

    #endregion

    #region Events
    public static event VoidDelegate ExampleEvent;
    public static void OnExampleEvent() { ExampleEvent?.Invoke(); }

    #region Enemies

    public static event VoidDelegate KillAllEnemies;

    public static void OnKillAllEnemies() { KillAllEnemies?.Invoke(); }

    #endregion

    #region Zones
    public static event VoidDelegate SaveProcess, CleanPlayer, ZoneReady;
    public static event IntDelegate StartSurvivalTiming;
    public static event ZoneDelegate ZoneEntered, ZoneCompleted, ZoneExit;
    public static event BonfireDelegate BonfireInteracted;

    public static void OnBonfireInteracted(Bonfire bonfire) { BonfireInteracted?.Invoke(bonfire); }
    public static void OnZoneReady() { ZoneReady?.Invoke(); }
    public static void OnCleanPlayer() { CleanPlayer?.Invoke(); }
    public static void OnSaveProcess() { SaveProcess?.Invoke(); }

    public static void OnZoneEntered(Zone zone) { ZoneEntered?.Invoke(zone); }
    public static void OnStartSurvivalTiming(int time) { StartSurvivalTiming?.Invoke(time); }
    public static void OnZoneCompleted(Zone zone) { ZoneCompleted?.Invoke(zone); }
    public static void OnZoneExit(Zone zone) { ZoneExit?.Invoke(zone); }

    #endregion

    #region Popups

    public static event VoidDelegate PopupOpened, PopupsClosed;
    public static void OnPopupOpened() { PopupOpened?.Invoke(); }
    public static void OnPopupsClosed() { PopupsClosed?.Invoke(); }

    #endregion
    #endregion
}

