using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    public UiManager ui;
    public PlayerMovement playerCode;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject spawners;
    [SerializeField]
    GameObject audioManager;
    [SerializeField]
    GameObject movingBackgrounds;
    [SerializeField]
    GameObject Background;

    public static bool GameStarted = false;
    public static int lives = 3;
    public static int score = 0;
    public float distance = 0f;
    public static float timer = 0f;
    [SerializeField]
    float winTime = 60f;
    public float runSpeed = 5f;
    public bool isGameOver {get; private set; } = false;
    public bool isVictory { get; private set; } = false;
    [SerializeField]
    GameObject fofura;
    [SerializeField]
    GameObject uruca;
    // Start is called before the first frame update
    void Start()
    {   lives = 3;
        score = 0;
        timer = 0f;
        ui.UpdateLives(lives);
        ui.UpdateScore(score);
        ui.UpdateDistance(distance);

    }

    // Update is called once per frame
    void Update()
    {   
        timer += Time.deltaTime;
        if (isGameOver || isVictory)
        {
            return;

        }
        distance += runSpeed * Time.deltaTime;
        ui.UpdateDistance(distance);
        if (GameStarted)
        {
            spawners.SetActive(true);
            audioManager.SetActive(true);
            movingBackgrounds.SetActive(true);
            Background.SetActive(false);
            GameStarted = false;
        }
        if (timer >= winTime)
        {
            Victory();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        playerCode.enabled = false;
        Destroy(spawners);
        Destroy(audioManager);
        playerCode.animator.StopPlayback();
        Background.SetActive(true);
        movingBackgrounds.SetActive(false);
        ui.ShowGameOver(true);

    }
    public void AddToken()
    {
        if (isGameOver) return;

        score++;
        lives++;
        ui.UpdateLives(lives);
        ui.UpdateScore(score);

    }
    void Victory()
    {
        isVictory = true;
        playerCode.enabled = false;
        ui.ShowVictory(true);
        Destroy(spawners);
        Destroy(audioManager);
        movingBackgrounds.SetActive(false);
        Background.SetActive(true);
       StartCoroutine(WaitCoroutine());
    }
    IEnumerator FofuraCoroutine()
    {
        yield return new WaitForSeconds(2f);
        fofura.SetActive(true);
        StartCoroutine(WaitWaitCoroutine());
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        uruca.SetActive(true);
        StartCoroutine(FofuraCoroutine());
    }
    IEnumerator WaitWaitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        playerCode.animator.Play("OnFoot");
    }
}

