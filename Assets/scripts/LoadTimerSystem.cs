using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTimerSystem : MonoBehaviour
{
    public static LoadTimerSystem Instance; // 1

    //Player1
    [SerializeField] public Image uiFillP1;
    [SerializeField] public Text uiTextP1;
    [SerializeField] public RectTransform FxHolderP1;

    //Player2
    [SerializeField] public Image uiFillP2;
    [SerializeField] public Text uiTextP2;
    [SerializeField] public RectTransform FxHolderP2;


    public float time = 5;
    public float value = 0;


    private void Awake()
    {
        Instance = this;
    }
    public void InitTimers()
    {
         time = 5;
         value = 0;
    }

    public void LoadTimerP1()
    {
        time -= Time.deltaTime;
        value = time / 5;

        Debug.Log("P1: " + time);
        LoadTimerSystem.Instance.uiTextP1.text = $"{(int)time % 60:00}:{(int)((time - (int)time) * 100):00}";
        LoadTimerSystem.Instance.uiFillP1.fillAmount = value;
        LoadTimerSystem.Instance.FxHolderP1.rotation = Quaternion.Euler(90, 0, value * 360);
    }
    public void LoadTimerP2()
    {
        time -= Time.deltaTime;
        value = time / 5;

        Debug.Log("P2: " + time);
        LoadTimerSystem.Instance.uiTextP2.text = $"{(int)time % 60:00}:{(int)((time - (int)time) * 100):00}";
        LoadTimerSystem.Instance.uiFillP2.fillAmount = value;
        LoadTimerSystem.Instance.FxHolderP2.rotation = Quaternion.Euler(90, 0, value * 360);
    }
}

