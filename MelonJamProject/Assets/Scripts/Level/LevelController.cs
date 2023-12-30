using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject ladderPrefab;
    [Tooltip("1 - enemy\n2 - levels\n3 - score")]
    [SerializeField] private List<Text> texts;
    [SerializeField] private GameObject losePanel;

    public static LevelController instance {get; private set;}

    private int enemyCount;
    private int enemyKilled = 0;
    private float roomMultiplier = 1;
    private bool levelCleared = false;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        roomMultiplier = PlayerPrefs.GetFloat("RoomMultiplier", 1);

        List<Room> generatedRooms = RoomGenerator.instance.GenerateRooms(roomMultiplier);
        RoomGenerator.instance.GenerateDoors();
        
        ObstacleGenerator.instance.GenerateObstacles(generatedRooms);
        DecorationGenerator.instance.GenerateDecorations(generatedRooms);
        enemyCount = EnemyGenerator.instance.GenerateEnemies(generatedRooms);
    }
    
    private void Update() {
        if (enemyCount <= 0 && !levelCleared) {
            PlayerPrefs.SetFloat("RoomMultiplier", roomMultiplier + 0.2f);
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
        losePanel.SetActive(true);

        texts[0].text = "Enemy Killed " + PlayerPrefs.GetInt("EnemeyKilled");
        texts[1].text = "Levels Passed" + PlayerPrefs.GetInt("LevelsCleared", 1);
        texts[2].text = "Total Score" + PlayerPrefs.GetInt("TotalScore");

        PlayerPrefs.SetFloat("RoomMultiplier", 1);
        PlayerPrefs.SetInt("LevelsCleared", 1);
        PlayerPrefs.SetInt("EnemeyKilled", 0);
        PlayerPrefs.SetInt("TotalScore", 0);
    }

    public void EnemyDied() {
        enemyCount--;
        enemyKilled++;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu() {
        // Menu
    }
}
