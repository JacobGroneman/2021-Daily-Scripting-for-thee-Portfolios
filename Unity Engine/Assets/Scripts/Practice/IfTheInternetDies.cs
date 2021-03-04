using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class IfTheInternetDies : MonoBehaviour
{
    void Start()
    {
        checked
        {//I don't this works well, but it is good parody
            MyComputer.IsInternetOn = false;
        }
    }
}

public static class MyComputer
{
    public static bool IsInternetOn;
    public static Image FullBars;

    static MyComputer()
    {
        if (Math.Abs(FullBars.fillAmount - float.MinValue) < 0)
        {
            IsInternetOn = false;
            MyIPhone.ConnectToHotspot();
        }
        else
        {
            IsInternetOn = true;
        }
    }
}

public static class MyIPhone
{
    public static void ConnectToHotspot()
    {
        if (MyComputer.IsInternetOn == false)
        {
            MyComputer.IsInternetOn = true;
            MyComputer.FullBars.fillAmount = Single.MaxValue;
            ;
        }
    }
}
