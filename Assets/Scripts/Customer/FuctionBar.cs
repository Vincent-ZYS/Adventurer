using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuctionBar : MonoBehaviour {

    public void OnBagButtonClick()
    {
        Inventory.instance.TransformState();
    }
    public void OnPlayerStatusClick()
    {
        Status.instance.TransformState();
    }
    public void OnSkillButtonClick()
    {
        SkillUI.instance.TransformState();
    }
   
}
