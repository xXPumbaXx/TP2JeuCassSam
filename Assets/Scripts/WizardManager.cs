using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    //Serialized Field
    [SerializeField] private Sprite wizardSprite;//Do nothing for now
    [SerializeField] private int STARTING_HP = 100;
    [SerializeField] private GameObject bulletPrefab;//Do nothing for now - to initialize

    public enum WizardStateToSwitch { Normal, Intrepide, Fuite, Planquer, Sureté, LastStand }

    //Component
    private SpriteRenderer spriteRenderer;
    private WizardState wizardState;

    //WizardStats
    private int currentHp;

    void Awake()
    {
        wizardState = GetComponent<WizardState>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = wizardSprite;

        currentHp = STARTING_HP;
    }
    public void ChangeGhostState(WizardStateToSwitch nextState)
    {
        wizardState.enabled = false;

        switch (nextState)
        {
            case WizardStateToSwitch.Normal:
                wizardState = gameObject.GetComponent<WizardStateNormal>();
                break;
            case WizardStateToSwitch.Intrepide:
                wizardState = gameObject.GetComponent<WizardStateIntrepide>();
                break;
            case WizardStateToSwitch.Fuite:
                wizardState = gameObject.GetComponent<WizardStateFuite>();
                break;
            case WizardStateToSwitch.Planquer:
                wizardState = gameObject.GetComponent<WizardStatePlanquer>();
                break;
            case WizardStateToSwitch.Sureté:
                wizardState = gameObject.GetComponent<WizardStateSureté>();
                break;
            case WizardStateToSwitch.LastStand:
                break;
            default:
                break;
        }

        //wizardState.Init();//Quand on en a besoin c'est là qu'On l'appelle.
        wizardState.enabled = true;
    }
}
