using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> blueTowers;
    [SerializeField] List<GameObject> greenTowers;
    [SerializeField] Sprite blueWizard;
    [SerializeField] Sprite greenWizard;

    public enum Equipe
    {
        BLEU,
        VERT
    }

    // Start is called before the first frame update
    void Start()
    {
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

        // TODO: Change tower Event
    }

    public GameObject getTower(Equipe couleur)
    {
        if(couleur == Equipe.BLEU)
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
}
