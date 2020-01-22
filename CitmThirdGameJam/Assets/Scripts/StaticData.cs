using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData
{
    private static int win_player;

    public static int Win
    {
        get
        {
            return win_player;
        }
        set
        {
            win_player = value;
        }
    }
}