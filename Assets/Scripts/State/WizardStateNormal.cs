using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    private const float INITIAL_SHOOTING_DELAY = 1.0f;
    private bool isInBattle;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
        shootingDelay = INITIAL_SHOOTING_DELAY;
        magicProjectileSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {
        if(wizardManager.GetKills() >= 3)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Intrepide);
            Debug.Log("State change: Intrepide");
        }
        if(hpManager.GetHpPercent() <= 25)
        {
            wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Fuite);
            Debug.Log("State change: Fuite");
        }
    }

    public override void WizardBehavior()
    {
        if (isInBattle)
        {
            Attack();
        }
        else
        {
            // Move
            transform.position = Vector3.MoveTowards(transform.position, towerObjectivePosition.position, speed * Time.deltaTime);

            //Regen
            Regen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            isInBattle = true;
            attackTarget = collision.gameObject;
        }
        if (collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            isInBattle = true;
            attackTarget = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            attackTarget = collision.gameObject;
        }
        if (!isInBattle && collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            isInBattle = true;
            attackTarget = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            isInBattle = false;
        }
        if (collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            isInBattle = false;
        }
    }

    private void Attack()
    {
        shootingDelay -= Time.deltaTime;

        if (shootingDelay < 0)
        {
            attackTarget.GetComponent<HpManager>().LoseOneHp(this.gameObject);
            shootingDelay = INITIAL_SHOOTING_DELAY;
        }
    }
}
