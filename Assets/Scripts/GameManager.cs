using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> blueTowers;
    [SerializeField] List<GameObject> greenTowers;
    [SerializeField] GameObject wizardPrefab;

    [SerializeField] int wizardPoolSize = 10;
    [SerializeField] float wizardSpawnInterval = 3;
    [SerializeField] int maxWizardAliveInTeam = 5;

    //Private variable
    private GameObject[] wizardArray;
    private float wizardSpawnTimer;
    private int blueWizardCount;
    private int greenWizardCount;

    public enum Equipe
    {
        BLEU,
        VERT
    }

    // Start is called before the first frame update
    void Start()
    {
        wizardArray = new GameObject[wizardPoolSize];

        for (int i = 0; i < wizardPoolSize; i++)
        {
            wizardArray[i] = Instantiate(wizardPrefab);
            wizardArray[i].SetActive(false);
        }

        //Variable Init
        wizardSpawnTimer = 0;

        //Spawn all wizard
        foreach (GameObject tower in blueTowers)
        {
            SpawnWizard(tower.transform.position, Equipe.BLEU);
        }
        foreach (GameObject tower in greenTowers)
        {
            SpawnWizard(tower.transform.position, Equipe.VERT);
        }
    }

    // Update is called once per frame
    void Update()
    {
        blueWizardCount = 0;
        greenWizardCount = 0;

        //Count wizard in each team
        for (int i = 0; i < wizardArray.Length; i++)
        {
            if (wizardArray[i].activeSelf)
            {
                if (wizardArray[i].GetComponent<WizardManager>().GetTeam() == Equipe.BLEU)
                {
                    blueWizardCount++;
                }
                else
                {
                    greenWizardCount++;
                }
            }
        }

        wizardSpawnTimer += Time.deltaTime * 1;
        // Debug.Log(wizardSpawnTimer);
        if (wizardSpawnTimer >= wizardSpawnInterval)
        {
            if (blueWizardCount <= maxWizardAliveInTeam)
            {
                SpawnWizard(getTower(Equipe.BLEU).transform.position, Equipe.BLEU);
            }
            if (greenWizardCount <= maxWizardAliveInTeam)
            {
                SpawnWizard(getTower(Equipe.VERT).transform.position, Equipe.VERT);
            }
            wizardSpawnTimer = 0;
        }
    }

    public void LoseATower(GameObject tower, Equipe equipe)
    {
        if (equipe == Equipe.BLEU)
        {
            blueTowers.Remove(tower);
            tower.SetActive(false);
        }
        else if (equipe == Equipe.VERT)
        {
            greenTowers.Remove(tower);
            tower.SetActive(false);
        }

        if (blueTowers.Count <= 0 || greenTowers.Count <= 0)
        {
            EndGame();
        }
        else
        {
            for (int i = 0; i < wizardArray.Length; i++)
            {
                if (wizardArray[i].activeSelf)
                {
                    wizardArray[i].GetComponent<WizardState>().ChangeTower();
                }
            }
        }
    }

    public GameObject getTower(Equipe couleur)
    {
        if(couleur == Equipe.VERT)
        {
            return greenTowers[Random.Range(0, greenTowers.Count)];
        }
        else
        {
            return blueTowers[Random.Range(0, blueTowers.Count)];
        }
    }

    private void EndGame()
    {
        string winners = (blueTowers.Count <= 0) ? "green" : "blue";
        Debug.Log("The " + winners + " team won!");

        for (int i = 0; i < wizardArray.Length; i++)
        {
            wizardArray[i].GetComponent<WizardManager>().ChangeWizardState(WizardManager.WizardStateToSwitch.Inactif);
        }
    }

    private void SpawnWizard(Vector2 spawnPos, Equipe team)//Dont call it if all player are alive
    {
        for (int i = 0; i < wizardPoolSize; i++)
        {
            if (!wizardArray[i].activeSelf)
            {
                //Spawn
                wizardArray[i].GetComponent<WizardManager>().ChangeTeam(team);
                wizardArray[i].SetActive(true);
                wizardArray[i].transform.position = spawnPos;
                return;
            }
        }

        //Spawn the first one(but you aint supposed to get here)
        wizardArray[0].SetActive(true);
        wizardArray[0].transform.position = spawnPos;
        wizardArray[0].GetComponent<WizardManager>().ChangeTeam(team);
    }
}
