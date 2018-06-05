using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class custom_FPInput : vp_FPInput {

    protected override void InputAttack()
    {
        base.InputAttack();
        if (vp_Input.GetButton("Attack"))
        {
            FPPlayer.Attack.TryStart();
        }
        else if(vp_Input.GetAxisRaw("RightTrigger") > 0.5f)
        {
            FPPlayer.Attack.TryStart();
        }
        else
        {
            FPPlayer.Attack.TryStop();
        }
    }
    
    protected override void InputCamera()
    {
        base.InputCamera();

        //if (vp_Input.GetButton("Zoom"))
        //{
        //    FPPlayer.Attack.TryStart();
        //}
        if (vp_Input.GetAxisRaw("LeftTrigger") > 0.5f)
        {
            FPPlayer.Zoom.TryStart();
        }
        else
        {
            FPPlayer.Zoom.TryStop();
        }
    }
}
