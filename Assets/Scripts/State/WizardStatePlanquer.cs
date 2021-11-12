using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStatePlanquer : WizardState
{
    private bool inBattle;
    private bool canFleeAgain;

    // Start is called before the first frame update
    void Start()
    {
        SetRegenTime(2.5f);
        speed = 0f;
        shootingDelay = INITIAL_SHOOTING_DELAY;
    }

    // Update is called once per frame
    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {
        if (hpManager.GetHpPercent() == 100 || inBattle && hpManager.GetHpPercent() >= 50)
        {
            SetRegenTime(5.0f);
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Normal);
            Debug.Log("State change: Normal");
        }
        if (canFleeAgain && hpManager.GetHpPercent() <= 25)
        {
            SetRegenTime(5.0f);
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Fuite);
            Debug.Log("State change: Fuite");
        }
    }

    public override void WizardBehavior()
    {
        if (inBattle)
        {
            Attack();
        }
        else
        {
            Regen();
            if(!canFleeAgain && hpManager.GetHpPercent() >= 26)
            {
                canFleeAgain = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            inBattle = true;
            SwitchAttackTarget(collision.gameObject);
        }
        if (collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            inBattle = true;
            SwitchAttackTarget(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            inBattle = false;
        }
        if (collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            inBattle = false;
        }
    }
}
