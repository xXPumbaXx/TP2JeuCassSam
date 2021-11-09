using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    protected GameManager gameManager;
    protected WizardManager wizardManager;
    protected Transform objectivePosition;

    protected float speed;
    protected float shootingDelay;
    protected float magicProjectileSpeed;
    protected GameManager.Equipe team;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        wizardManager = GetComponent<WizardManager>();
    }

    private void OnEnable()
    {
        GameManager.Equipe otherTeam = (wizardManager.GetTeam() == GameManager.Equipe.BLEU) ? GameManager.Equipe.VERT : GameManager.Equipe.BLEU;
        objectivePosition = gameManager.getTower(otherTeam).transform;
        team = wizardManager.GetTeam();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public abstract void WizardBehavior();
    public abstract void ManageStateChange();
}
