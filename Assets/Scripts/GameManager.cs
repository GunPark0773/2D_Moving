using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    private int score = 0;
    private float scoreCheckTime = 0;
    private bool scoreCheck = false;
    private float degreePerSecond = 10;
    private float checkF = 0f;
    private bool check = true;
    private Vector3 SetPosition = Vector3.zero;
    private int health = 3;
    public float noHitTime = 0;
    private bool HitCheckCheck = false;
    public GameObject enemyObject1;
    public GameObject enemyObject2;
    public GameObject enemyObject3;
    public GameObject enemyObject4;

    public Text wallMoveCheckText;
    public Text wallRollCheckText;
    public Text enemySecondCheckText;
    public Text cameraPositionCheckText;
    public Text gameOver;
    public Text LifePoint;
    public Text scoreText;
    public GameObject GameOverBack;
    public GameObject GameStartBack;
    public Text GameStartBackText;
    public Text GameStartBackTimer;

    public float maxSpawnDelay;
    public float curSpawnDelay;
    public float maxSpawnDelay1;
    public float curSpawnDelay1;
    public GameObject player;
    private float time = 0;
    public bool wallMoveCheck = false;
    public bool wallRollCheck = false;
    public bool enemyFirst = false;
    public bool enemySecond = false;
    public bool cameraPositionCheck = false;
    public float rotationSpeed = 20f;
    public Transform rotateAroundTarget;
    private int remainTime = 60;
    private bool gameProceeding = true;
    private bool gameStart= false;
    private bool gameStartTimerOn = false;
    private float gameStartTimer = 3;
    private float gameStartTimerSetting = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space)) && (!gameStart))
        {
            GameStartBackText.gameObject.SetActive(false);
            GameStartBackTimer.gameObject.SetActive(true);
            gameStartTimerOn = true;
        }
        GameStartBackTimer.text = string.Format("{0}", gameStartTimer);
        if (gameStartTimerOn)
        {
            if (gameStartTimerSetting < 1)
            {
                gameStartTimerSetting += Time.deltaTime;
            }
            else
            {
                gameStartTimerSetting = 0;
                gameStartTimer--;
            }
        }
        if(gameStartTimer == 0)
        {
            gameStart = true;
            GameStartBackText.gameObject.SetActive(false);
            GameStartBackTimer.gameObject.SetActive(false);
            GameStartBack.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            health = 3;
        }

        if(gameStart)
        { 
            if ((Input.GetKeyUp(KeyCode.Space)) && (!gameProceeding))
            {
                RestartGame();
            }
            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                wallMoveCheck = !wallMoveCheck;
            }
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                wallRollCheck = !wallRollCheck;
            }
            //if (Input.GetKeyUp(KeyCode.Alpha3))
            //{
            //    enemyFirst = !enemyFirst;
            //}
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                enemySecond = !enemySecond;
            }
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                cameraPositionCheck = !cameraPositionCheck;
            }

            wallMoveCheckText.text = string.Format("Wall Move : {0}", wallMoveCheck);
            wallRollCheckText.text = string.Format("Wall Roll : {0}", wallRollCheck);
            enemySecondCheckText.text = string.Format("Box Spawn : {0}", enemySecond);
            cameraPositionCheckText.text = string.Format("Camera Stick : {0}", cameraPositionCheck);



            LifePoint.text = string.Format("Life : {0}", health);

            scoreText.text = string.Format("Time Remaining : {0}\nScore : {1}", remainTime, score);

            time += Time.deltaTime;
            checkF += Time.deltaTime;
            curSpawnDelay += Time.deltaTime;
            curSpawnDelay1 += Time.deltaTime;
            PlayerController playerLogic = player.GetComponent<PlayerController>();

            scoreCheckTime += Time.deltaTime;

            if (gameProceeding)
            {
                if (scoreCheckTime > 1)
                {
                    scoreCheckTime = 0;
                    scoreCheck = true;
                    remainTime--;
                }
                else
                {
                    scoreCheck = false;
                }
            }
            if (checkF > 1)
            {
                checkF = -1f;
                check = !check;
            }

            if (curSpawnDelay > maxSpawnDelay)
            {
                SpawnEnemy();

                maxSpawnDelay = UnityEngine.Random.Range(1f, 3f);
                curSpawnDelay = 0;
            }


            if (scoreCheck)
            {
                score += 100;
            }


            if (wallMoveCheck)
            {
                WallMove();

                if (scoreCheck)
                {
                    score += 100;
                }
            }

            if (wallRollCheck)
            {
                WallRoll();

                if (scoreCheck)
                {
                    score += 100;
                }
            }

            if (enemySecond)
            {
                if (curSpawnDelay1 > maxSpawnDelay1)
                {
                    SpawnEnemy1();
                    curSpawnDelay1 = 0;
                }

                if (scoreCheck)
                {
                    score += 100;
                }
            }

            if (cameraPositionCheck)
            {
                CameraPosition();

                if (scoreCheck)
                {
                    score += 100;
                }
            }
            if (HitCheckCheck != playerLogic.HitCheck)
            {
                health--;
                playerLogic.HitCheck = !playerLogic.HitCheck;
                playerLogic.transform.position = Vector3.zero;

                wallMoveCheck = false;
                wallRollCheck = false;
                enemyFirst = false;
                enemySecond = false;
                cameraPositionCheck = false;
            }
            if ((health == 0) || (remainTime < 0))
            {
                health = 0;
                gameOver.gameObject.SetActive(true);
                playerLogic.isLive = false;
                GameOverBack.gameObject.SetActive(true);
                gameProceeding = false;
            }

        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyObject1,
                               new Vector3(UnityEngine.Random.Range(-8, 9), UnityEngine.Random.Range(-8, 9), 0), Quaternion.Euler(0, 0, 0));

        //스폰될 적 실제 스폰 세팅

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;

        //if (enemyFirst)
        //{
        //    Debug.Log(enemy.transform.position);
        //    enemy.transform.RotateAround(Vector3.zero, Vector3.forward, rotationSpeed * Time.deltaTime);
        //}
    }
    private void SpawnEnemy1()
    {
        GameObject enemy;
        int checkTimeWall = (int)time / 2;
        if (checkTimeWall / 2 == 1)
        {
            enemy = Instantiate(enemyObject2,
                   new Vector3(UnityEngine.Random.Range(-12, 13), 0, 0), Quaternion.Euler(0, 0, 0));
            enemy.transform.Rotate(0, 0, 90);
        }
        else
        {
            enemy = Instantiate(enemyObject2,
                   new Vector3(0, UnityEngine.Random.Range(-12, 13), 0), Quaternion.Euler(0, 0, 0));
        }
        
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
    }

    private void WallMove()
    {
        enemyObject3.transform.position = SetPosition;
        if (check)
        {
            SetPosition.x += 0.01f;
        }
        else
        {
            SetPosition.x -= 0.01f;
        }
    }
    private void WallRoll()
    {
        enemyObject3.transform.Rotate(Vector3.forward * Time.deltaTime * degreePerSecond);

        if (check)
        {
            enemyObject3.transform.Rotate(Vector3.up * Time.deltaTime * degreePerSecond);
        }
        else
        {
            enemyObject3.transform.Rotate(Vector3.down * Time.deltaTime * degreePerSecond);
        }
    }

    private void CameraPosition()
    {
        enemyObject4.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -20);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}