using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class WizardState : MonoBehaviour
{
    protected GameManager gameManager;
    protected WizardManager wizardManager;
    protected Transform towerObjectivePosition;
    protected GameObject attackTarget;
    protected HpManager hpManager;

    protected float speed;
    protected float shootingDelay;
    protected float magicProjectileSpeed;
    protected float regenTime;
    protected const float INITIAL_REGEN_TIME = 5.0f;
    protected GameManager.Equipe team;
    private GameManager.Equipe otherTeam;

    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hpManager = GetComponent<HpManager>();
    }

    private void OnEnable()
    {
        otherTeam = (wizardManager.GetTeam() == GameManager.Equipe.BLEU) ? GameManager.Equipe.VERT : GameManager.Equipe.BLEU;
        towerObjectivePosition = gameManager.getTower(otherTeam).transform;
        team = wizardManager.GetTeam();
        regenTime = INITIAL_REGEN_TIME;
    }

    public void ChangeTower()
    {
        while (!towerObjectivePosition.gameObject.activeSelf)
        {
            towerObjectivePosition = gameManager.getTower(otherTeam).transform;
        }
    }

    protected void Regen()
    {
        regenTime -= Time.deltaTime;
        if (regenTime <= 0)
        {
            hpManager.RegenOneHp();
        }
    }

    public abstract void WizardBehavior();
    public abstract void ManageStateChange();
}
