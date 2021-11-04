using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateInactif : WizardState
{
    // Start is called before the first frame update

    public override void ManageStateChange()
    {
        throw new System.NotImplementedException();
    }

    public override void WizardBehavior()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }
}
