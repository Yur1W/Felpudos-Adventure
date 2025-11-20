using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoTitleScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
