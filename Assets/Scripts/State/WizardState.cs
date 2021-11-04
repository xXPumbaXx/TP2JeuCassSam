using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardState : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    protected WizardManager wizardManager;
    protected Transform objectivePosition;

    protected float speed;

    // Start is called before the first frame update
    void Awake()
    {
        wizardManager = GetComponent<WizardManager>();
        // TODO: Gérer les couleurs (wizard & tower)
        objectivePosition = gameManager.getTower().transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public abstract void WizardBehavior();
    public abstract void ManageStateChange();
}
