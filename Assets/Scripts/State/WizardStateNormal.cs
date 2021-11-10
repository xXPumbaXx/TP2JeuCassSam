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
        // TODO
    }

    public override void WizardBehavior()
    {
        if (isInBattle)
        {
            shootingDelay -= Time.deltaTime;

            if(shootingDelay < 0)
            {
                // wizardManager.Attack();
                attackTarget.GetComponent<LivesManager>().LoseALife();
                shootingDelay = INITIAL_SHOOTING_DELAY;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, towerObjectivePosition.position, speed * Time.deltaTime);
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
}
