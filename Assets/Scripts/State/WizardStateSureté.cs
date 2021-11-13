using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateSureté : WizardState
{
    private GameObject inTower;
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        SetRegenTime(1.5f);
        foreach(GameObject tower in gameManager.getAllActiveTeamTowers(team))
        {
            if(Vector2.Distance(transform.position, tower.transform.position) < 0.1)
            {
                inTower = tower;
                break;
            }
        }
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = !circleCollider.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {
        if(hpManager.GetHpPercent() == 100 || !inTower.activeSelf)
        {
            SetRegenTime(5.0f);
            circleCollider.enabled = !circleCollider.enabled;
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
            Debug.Log("State change: Normal");
        }
    }

    public override void WizardBehavior()
    {
        Regen();
    }
}
