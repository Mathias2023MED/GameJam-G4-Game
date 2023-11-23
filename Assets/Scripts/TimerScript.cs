using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] int maxTime;
    private TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        timerText = GetComponent<TMP_Text>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        maxTime = maxTime-1;
        Debug.Log(maxTime);

        timerText.text = maxTime.ToString();

        StartCoroutine(Timer());
    }
}
