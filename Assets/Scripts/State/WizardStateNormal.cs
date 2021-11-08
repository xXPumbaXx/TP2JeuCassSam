using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateNormal : WizardState
{
    GameObject[] projectiles;

    bool isInBattle;
    private Transform enemyPosition;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        shootingDelay = 2f;
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
        throw new System.NotImplementedException();
    }

    public override void WizardBehavior()
    {
        if (isInBattle)
        {
            shootingDelay -= Time.deltaTime;

            foreach(GameObject projectile in projectiles)
            {
                if (!projectile.activeSelf && shootingDelay < 0)
                {
                    projectile.SetActive(true);
                    shootingDelay = 2f;
                }
                projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, enemyPosition.position, magicProjectileSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, objectivePosition.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.name != gameObject.name)
        {
            isInBattle = true;
            enemyPosition = collision.gameObject.transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.name != gameObject.name)
        {
            enemyPosition = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wizard" && collision.gameObject.name != gameObject.name)
        {
            isInBattle = false;
        }
    }
}
