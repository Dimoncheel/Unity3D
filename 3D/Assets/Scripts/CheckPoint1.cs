using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image countdown;
    const float startTime = 5;   
    private float countdownTime;
    GameObject gate1;
    public static bool isActivated = false;

    void Start()
    {
        countdownTime = startTime;   
        gate1 = GameObject.Find("Gates1");
    }

    void Update()
    {
        countdownTime -= Time.deltaTime;

        if (countdownTime < 0)
        {
            GameStat.PassCheckpoint1(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            GameStat.Checkpoint1Fill =
            countdown.fillAmount =
                countdownTime / startTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        gate1.SetActive(false);
        GameStat.PassCheckpoint1(true);
        isActivated = true;
    }
}
