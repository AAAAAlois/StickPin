using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    private Transform StartPoint;
    private bool isFly = false;
    private bool isReach = false;
    private Vector3 TargetCirclePos;
    public float speed = 5;
    public Transform Circle;

	// Use this for initialization
	void Start () {
        StartPoint = GameObject.Find("StartPoint").transform;
        Circle = GameObject.Find("Circle").transform;
        TargetCirclePos = Circle.position;
        TargetCirclePos.y -= 2f;
	}
	
	// Update is called once per frame
	void Update () {
        if(isFly == false){
            if(isReach == false){
                transform.position = Vector3.MoveTowards(transform.position, StartPoint.position, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position,StartPoint.position)<0.05f) {
                    isReach = true;
                }
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position,TargetCirclePos, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position,TargetCirclePos)<0.05f){
                transform.position = TargetCirclePos;
                transform.parent = Circle;
                isFly = false;
            }
        }
	}

    public void StartFly(){
        isFly = true;
        isReach = true;
    }
}
