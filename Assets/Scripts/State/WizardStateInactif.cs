using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateInactif : WizardState
{
    // Start is called before the first frame update

    public override void ManageStateChange()
    {
    }

    public override void WizardBehavior()
    {
    }

    // Update is called once per frame
    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }
}
