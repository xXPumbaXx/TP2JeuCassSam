using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] int lives;
    [SerializeField] bool isBlue;
    string enemyProjectileName;
    GameManager.Equipe equipe;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyProjectileName = isBlue ? "ProjectileGreen" : "ProjectileBlue";
        equipe = isBlue ? GameManager.Equipe.BLEU : GameManager.Equipe.VERT;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.name == enemyProjectileName)
        {
            lives--;
        }
        if(lives <= 0)
        {
            gameManager.LoseATower(this.gameObject, equipe);
        }
    }
}
