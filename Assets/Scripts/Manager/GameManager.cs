using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TMP_Text timeText;
    private float _time = 60;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;
        timeText.text = ((int)_time).ToString();
        print("die");
    }
}
