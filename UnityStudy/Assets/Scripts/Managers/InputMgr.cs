using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : MonoBehaviour {

    private static bool isDPadRightPressed = false;    // D-Pad Right 键是否正被按住

    /// <summary>
    /// 手柄十字键区域右键是否按下
    /// </summary>
    /// <returns>按下那一刻返回true, 长按返回false</returns>
    public static bool GetDPadRightDown()
    {
        bool result = false;

        float right = Input.GetAxis("Weapon");
        if (right >= 0.5f)
        {
            if (!isDPadRightPressed)
            {
                result = true;
            }
            isDPadRightPressed = true;
        }
        else
        {
            isDPadRightPressed = false;
        }

        return result;
    }
}
