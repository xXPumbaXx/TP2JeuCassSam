using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardStateFuite : WizardState
{
    private GameObject[] safeZones;
    private GameObject safeZoneObjective;

    private void Start()
    {
        speed = 2.5f;

        // Get all safe zones
        GameObject[] allyTowersPositions = gameManager.getAllActiveTeamTowers(wizardManager.GetTeam());
        GameObject[] forestsPositions = gameManager.getAllForests();

        safeZones = new GameObject[allyTowersPositions.Length + forestsPositions.Length];
        allyTowersPositions.CopyTo(safeZones, 0);
        forestsPositions.CopyTo(safeZones, allyTowersPositions.Length);

        getNearestSafeZone();
    }

    void Update()
    {
        WizardBehavior();
        ManageStateChange();
    }

    public override void ManageStateChange()
    {
        if(Vector2.Distance(transform.position, safeZoneObjective.transform.position) < 0.01)
        {
            if(safeZoneObjective.tag == "Tower")
            {
                wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Sureté);
                Debug.Log("State change: Sureté");
            }
            if(safeZoneObjective.tag == "Forest")
            {
                wizardManager.ChangeWizardState(WizardManager.WizardStateToSwitch.Planquer);
                Debug.Log("State change: Planquer");
            }
        }
    }

    public override void WizardBehavior()
    {
        if (!safeZoneObjective.activeSelf)
        {
            getNearestSafeZone();
        }
        // Move
        transform.position = Vector3.MoveTowards(transform.position, safeZoneObjective.transform.position, speed * Time.deltaTime);

        //Regen
        Regen();
    }

    private void getNearestSafeZone()
    {
        safeZoneObjective = safeZones[0];
        Vector2 wizardPosition = transform.position;
        float distance = Vector2.Distance(wizardPosition, safeZoneObjective.transform.position);
        foreach (GameObject zone in safeZones)
        {
            float newDistance = Vector2.Distance(wizardPosition, zone.transform.position);
            if (distance > newDistance && distance > 1) // Un sorcier ne doit pas fuire dans une foret/tour qu'il est déjà dedans
            {
                safeZoneObjective = zone;
                distance = newDistance;
            }
        }
    }
}
