using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Increment the balls hit and the misses
/// </summary>
public class GameClock : MonoBehaviour
{
    private float timer = Time.deltaTime;
    private string clock;
    private Text clockText;
    private GameController gc;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject); 

    }

    void Start()
    {
        clockText = GameObject.Find("GameClock").GetComponent<Text>();
    }

    void Update()
    {
        clockText.text = clock;
    }

    void OnGUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        clock = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}


