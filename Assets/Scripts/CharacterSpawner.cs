using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterPrefabs = new List<GameObject>();
    //private List<GameObject> chosenCharacters = new List<GameObject>();
    public GameObject[] chosenCharacters = new GameObject[2];

    [SerializeField] public TMP_Text player1HP;
    [SerializeField] public TMP_Text player2HP;

    [SerializeField] private Vector3 spawnPoint1 = new Vector3(-6,0,0);
    [SerializeField] private Vector3 spawnPoint2 = new Vector3(6, 0, 0);

    [SerializeField] private GameObject startGameButton;

    [Header("Player stats")]
    [SerializeField] private float moveSpeed = 700;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float health = 100;
    [SerializeField] private float punchDamage = 15;
    [SerializeField] private float kickDamage = 25;

    private bool player1Chosen = false;
    private GameObject toBeConfirmed;

    public void SelectCharacter(GameObject chosenChar, int slot)
    {
        if (slot == 1)
        {
            chosenCharacters[0] = chosenChar;
        }
        else if (slot == 2) {
            chosenCharacters[1] = chosenChar; 
            startGameButton.SetActive(true);
        }

    }

    public void ConfirmChar()
    {
        if (!player1Chosen)
        {
            SelectCharacter(toBeConfirmed, 1);
            player1Chosen = true;
        }
        else
        {
            SelectCharacter(toBeConfirmed, 2);
        }
        toBeConfirmed = null;
    }

    public void ChooseChar(GameObject chosenChar) 
    {
        toBeConfirmed = chosenChar;
    }
    public void StartGame()
    {

        GameObject player1 = Instantiate(chosenCharacters[0]);
        GameObject player2 = Instantiate(chosenCharacters[1]);
        GameObject[] chosenCharactersSpawned = new GameObject[2];
        chosenCharactersSpawned[0]= player1;
        chosenCharactersSpawned[1]= player2;

        PlayerMovement pm1 = player1.GetComponent<PlayerMovement>();
        player1.tag = "Player";
        player1.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        pm1.enemyPlayerScript = player2.GetComponent<PlayerMovement>();
        pm1.enemyHP = player2HP;
        player1.transform.position = spawnPoint1;


        PlayerMovement pm2 = player2.GetComponent<PlayerMovement>();
        player2.tag = "Player2";
        player2.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player2");
        pm2.enemyPlayerScript = player1.GetComponent<PlayerMovement>();
        pm2.enemyHP = player1HP;
        player2.transform.position = spawnPoint2;
        player2.transform.localScale = new Vector3(-1,1,1);

        foreach (GameObject player in chosenCharacters)//sync stats
        {
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.moveSpeed = moveSpeed;
            pm.jumpHeight = jumpHeight;
            pm.health = health;
            pm.punchDamage = punchDamage;
            pm.kickDamage = kickDamage;
        }


        player1.GetComponent<PlayerMovement>().EnableChildCollision();
        player2.GetComponent<PlayerMovement>().EnableChildCollision();
    }

}
