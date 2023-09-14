using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    // Update is called once per frame
    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;
        if (KeyAction != null)
            // 키 액션이 존재하였다고 전파 
            KeyAction.Invoke();
    }
}
