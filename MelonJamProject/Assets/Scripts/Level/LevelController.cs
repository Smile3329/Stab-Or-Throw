using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Animator transitor;
    [SerializeField] private GameObject ladderPrefab;
    [SerializeField] private AudioSource musicSource;
    [Tooltip("1 - enemy\n2 - levels\n3 - score")]
    [SerializeField] private List<Text> texts;
    [SerializeField] private GameObject losePanel;

    public static LevelController instance {get; private set;}

    private int enemyCount;
    private int enemyKilled = 0;
    private float DifficultyMultiplier = 1;
    private bool levelCleared = false;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        DifficultyMultiplier = PlayerPrefs.GetFloat("DifficultyMultiplier", 1);

        List<Room> generatedRooms = RoomGenerator.instance.GenerateRooms(DifficultyMultiplier);
        RoomGenerator.instance.GenerateDoors();
        
        ObstacleGenerator.instance.GenerateObstacles(generatedRooms);
        DecorationGenerator.instance.GenerateDecorations(generatedRooms);
        ChestGenerator.instance.Generate(generatedRooms);
        enemyCount = EnemyGenerator.instance.GenerateEnemies(generatedRooms, DifficultyMultiplier);

        StartCoroutine(MusicVolume(false));
    }
    
    private void Update() {
        if (enemyCount <= 0 && !levelCleared) {
            PlayerPrefs.SetFloat("DifficultyMultiplier", DifficultyMultiplier + 0.2f);
            PlayerPrefs.SetInt("LevelsCleared", PlayerPrefs.GetInt("LevelsCleared", 1) + 1);
            PlayerPrefs.SetInt("EnemeyKilled", PlayerPrefs.GetInt("EnemeyKilled", 0) + enemyKilled);
            PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore", 0) + ScoreCounter.instance.GetScore());

            CreateLadder();
        }
    }

    private void CreateLadder() {
        Room room = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>().GetCurrentRoom();
        GameObject nextLevelLadder = Instantiate(ladderPrefab);
        nextLevelLadder.transform.position = new Vector3(room.transform.position.x + .5f, room.transform.position.y + .5f, 0);
        levelCleared = true;
    }

    public void PlayerDied() {
        StartCoroutine(MusicVolume(true));

        losePanel.SetActive(true);

        texts[0].text = "Enemy Killed: " + PlayerPrefs.GetInt("EnemeyKilled");
        texts[1].text = "Levels Passed: " + PlayerPrefs.GetInt("LevelsCleared", 0);
        texts[2].text = "Total Score: " + PlayerPrefs.GetInt("TotalScore");

        PlayerPrefs.SetFloat("RoomMultiplier", 1);
        PlayerPrefs.SetInt("LevelsCleared", 0);
        PlayerPrefs.SetInt("EnemeyKilled", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
    }

    private IEnumerator MusicVolume(bool backward) {
        if (!backward) {
            float time = 0;
            musicSource.volume = 0;
            while (time < 3) {
                musicSource.volume = Mathf.Lerp(musicSource.volume, 1, time*Time.deltaTime);

                yield return null;

                time += Time.deltaTime;
            }
        } else {
            float time = 0;
            musicSource.volume = 1;
            while (time < 3) {
                musicSource.volume = Mathf.Lerp(musicSource.volume, 0, time*Time.deltaTime);
                
                yield return null;

                time += Time.deltaTime;
            }
        }
    }

    public void EnemyDied() {
        enemyCount--;
        enemyKilled++;
    }

    public void Restart() {
        transitor.SetTrigger("NewLevel");
        StartCoroutine(Wait(2f));
    }

    private IEnumerator Wait(float time) {
        float timer = 0;
        musicSource.volume = 1;
        while (timer < 3) {
            musicSource.volume = Mathf.Lerp(musicSource.volume, 0, time*Time.deltaTime);
            
            yield return null;

            timer += Time.deltaTime;
        }

        SceneManager.LoadScene(1);
    }

    public void ToMenu() {
        transitor.SetTrigger("NewLevel");
        SceneManager.LoadScene(0);
    }
}
