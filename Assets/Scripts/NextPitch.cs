using UnityEngine;
using System.Collections;

public class NextPitch : MonoBehaviour {

    public void LoadLevel(int level)
    {
        level = 1;
        Object.DontDestroyOnLoad
        Application.LoadLevel(level);
    }
}
