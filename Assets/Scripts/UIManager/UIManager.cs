using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player1UI;
    public GameObject player2;
    public GameObject player2UI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1UI.transform.position = player1.transform.position + Vector3.up * 0.5f;
        player2UI.transform.position = player2.transform.position + Vector3.up * 0.5f;
    }
}
