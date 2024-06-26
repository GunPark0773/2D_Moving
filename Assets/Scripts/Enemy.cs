using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float upScaleSpeed = 0.003f;
    public bool isAbleHit = false;
    public bool readyCheck = false;
    public bool playerHitCheck = false;
    private Transform EnemyTransform;
    private SpriteRenderer EnemySpriteRenderer;
    public bool isCircle = false;
    private int sniffling;
    private float timeCheck = 0;
    // Start is called before the first frame update


    void Start()
    {
        sniffling = Random.Range(0, 2);
        EnemyTransform = GetComponent<Transform>();
        EnemySpriteRenderer = GetComponent<SpriteRenderer>();

        if (isCircle)
        {
            EnemyTransform.localScale = new Vector3(0.5f, 0.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCircle == true)
        {
            CircleEnemy();
        }
        else
        {
            timeCheck += Time.deltaTime;
            WallEnemy();
        }
    }
    void ReadyCheck()
    {
        if(readyCheck == false)
        {
            gameObject.tag = "EnemyReady";
            readyCheck = true;
        }
    }
    void CircleEnemy()
    {
        //EnemyTransform.RotateAround(Vector3.zero, Vector3.forward, rotationSpeed * Time.deltaTime);

        if ((EnemyTransform.localScale.x < 2) && (!isAbleHit))
        {
            EnemyTransform.localScale += new Vector3(upScaleSpeed, upScaleSpeed, 0);
        }
        else
        {
            EnemyTransform.localScale -= new Vector3(upScaleSpeed, upScaleSpeed, 0);
            EnemySpriteRenderer.color = Color.red;
            ReadyCheck();
            isAbleHit = true;
        }

        if (EnemyTransform.localScale.x < 0.5)
        {
            Destroy(gameObject);
        }
    }
    void WallEnemy()
    {
        if(timeCheck > 3)
        {
            EnemySpriteRenderer.color = Color.red;
            ReadyCheck();
        }
        if(timeCheck > 4)
        {
            Destroy(gameObject);
        }
    }
}
