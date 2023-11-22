using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSpawner : MonoBehaviour
{
    //[SerializeField] private List<GameObject> characterPrefabs = new List<GameObject>();
    //private List<GameObject> chosenCharacters = new List<GameObject>();
    public GameObject[] chosenCharacters = new GameObject[2];

    [SerializeField] public TMP_Text player1HP;
    [SerializeField] public TMP_Text player2HP;

    [SerializeField] private Vector3 spawnPoint1 = new Vector3(-6,0,0);
    [SerializeField] private Vector3 spawnPoint2 = new Vector3(6, 0, 0);

    public void SelectCharacter(GameObject chosenChar, int slot)
    {
        if (slot == 1)
        {
            chosenCharacters[0] = chosenChar;
        }
        else if (slot == 2) { chosenCharacters[0] = chosenChar; }

    }
    public void StartGame()
    {

        GameObject player1 = Instantiate(chosenCharacters[0]);
        GameObject player2 = Instantiate(chosenCharacters[1]);

        player1.tag = "Player";
        player1.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        player1.GetComponent<PlayerMovement>().enemyPlayerScript = player2.GetComponent<PlayerMovement>();
        player1.GetComponent<PlayerMovement>().enemyHP = player2HP;
        player1.transform.position = spawnPoint1;

        player2.tag = "Player2";
        player2.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player2");
        player2.GetComponent<PlayerMovement>().enemyPlayerScript = player1.GetComponent<PlayerMovement>();
        player2.GetComponent<PlayerMovement>().enemyHP = player1HP;
        player2.transform.position = spawnPoint2;
        player2.transform.localScale = new Vector3(-1,1,1);


        player1.GetComponent<PlayerMovement>().EnableChildCollision();
        player2.GetComponent<PlayerMovement>().EnableChildCollision();
    }

}
