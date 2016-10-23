using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextPitch : MonoBehaviour {

    public void LoadLevel(int level)
    {
        level = 1;
// Object.DontDestroyOnLoad
        SceneManager.LoadScene(level);
    }
}
