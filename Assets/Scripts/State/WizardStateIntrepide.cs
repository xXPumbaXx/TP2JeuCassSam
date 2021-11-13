using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateIntrepide : WizardState
{
    private const float INITIAL_SHOOTING_DELAY = 1.0f;
    private bool isInBattle;
    private bool gotTarget;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        shootingDelay = INITIAL_SHOOTING_DELAY;
        magicProjectileSpeed = 2f;
        gotTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {
        throw new System.NotImplementedException();
    }

    public override void WizardBehavior()
    {
        if (isInBattle)
        {
            Attack();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, towerObjectivePosition.position, speed * Time.deltaTime);
        }
        // Regenerate all the time
        Regen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            isInBattle = true;
            SwitchAttackTarget(collision.gameObject);
        }
        if (collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            isInBattle = true;
            SwitchAttackTarget(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.GetComponent<WizardManager>().GetTeam() != team)
        {
            SwitchAttackTarget(collision.gameObject);
        }
        if (!isInBattle && collision.gameObject.tag == "Tower" && collision.gameObject.GetComponent<TowerManager>().GetTeam() != team)
        {
            isInBattle = true;
            SwitchAttackTarget(collision.gameObject);
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
}
