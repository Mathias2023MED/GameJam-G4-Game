using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWinScript : MonoBehaviour
{
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        //text.text = $"Winner: !!!";
    }
    public void EndScreen(string name)
    {
        name = name.Replace("(Clone)", "");
        text.text = $"Winner: {name}!!!";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
