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

   
    #endregion

    #region Events
    public static event VoidDelegate ExampleEvent;
    public static void OnExampleEvent() { ExampleEvent?.Invoke(); }

    #endregion
}

