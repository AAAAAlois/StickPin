using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Transform StartPoint;
    private Transform SpawnPoint;
    private Pin CurrentPin;    //当前准备发射的针
    private bool isGameOver = false;
    private int score = 0;
    private Camera MainCamera;

    public GameObject pinPrefab;
    public Text ScoreText;
    public float speed = 3;


	// Use this for initialization
	void Start () {
        StartPoint = GameObject.Find("StartPoint").transform;
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
        MainCamera = Camera.main;
        SpawnPin();
	}
	
	// Update is called once per frame
	void Update () {
        if (isGameOver ) return;
        if(Input.GetMouseButtonDown(0)){
            score++;
            ScoreText.text = score.ToString(); 
            CurrentPin.StartFly();
            SpawnPin();
        }
        
	}

    void SpawnPin(){
        CurrentPin = GameObject.Instantiate(pinPrefab, SpawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver(){
        if (isGameOver) return;
        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;   //游戏结束时圆圈不再旋转
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }

    IEnumerator GameOverAnimation(){
        while(true){
            MainCamera.backgroundColor = Color.Lerp(MainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 4, speed * Time.deltaTime);
            if (Mathf.Abs(MainCamera.orthographicSize-4) < 0.01f) break;
            yield return 0;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //重新开始游戏
    }
}
