using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> blueTowers;
    [SerializeField] List<GameObject> greenTowers;
    [SerializeField] GameObject wizardPrefab;

    [SerializeField] int wizardPoolSize = 10;

    //Private variable
    private GameObject[] wizardArray;

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
            wizardArray[wizardPoolSize] = Instantiate(wizardPrefab);
            wizardArray[wizardPoolSize].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public GameObject getTower()
    {
        // TODO: Ajouter gestion de modification bleu/vert
        return blueTowers[Random.Range(0, blueTowers.Count)];
    }

    private void EndGame()
    {
        string winners = "";
        if(blueTowers.Count <= 0)
        {
            winners = "green";
        }
        else
        {
            winners = "bleu";
        }
        Debug.Log("The " + winners + " team won!");
    }

    private void SpawnWizard(Vector2 spawnPos, Equipe team)//Dont call it if all player are alive
    {
        for (int i = 0; i < wizardPoolSize; i++)
        {
            WizardManager wizardManager = wizardArray[i].GetComponent<WizardManager>();
            if (!wizardManager.IsAlive())
            {
                //Spawn
                wizardArray[i].SetActive(true);
                wizardArray[i].transform.position = spawnPos;
                wizardManager.ChangeTeam(team);
                return;
            }
        }

        //Spawn the first one(but you aint supposed to get here)
        wizardArray[0].SetActive(true);
        wizardArray[0].transform.position = spawnPos;
        wizardArray[0].GetComponent<WizardManager>().ChangeTeam(team);
    }
}
