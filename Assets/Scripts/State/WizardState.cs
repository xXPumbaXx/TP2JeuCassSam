using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    protected WizardManager wizardManager;
    protected Transform objectivePosition;

    protected float speed;
    protected float shootingDelay;
    protected float magicProjectileSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
        objectivePosition = gameManager.getTower(wizardManager.GetTeam()).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public abstract void WizardBehavior();
    public abstract void ManageStateChange();
}
