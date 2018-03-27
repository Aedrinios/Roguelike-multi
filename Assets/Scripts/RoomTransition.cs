﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour {

    public DirectionX directionX = DirectionX.NONE;
    public DirectionY directionY = DirectionY.NONE;

    public Vector2 PositionRoom = new Vector2();
    //public Vector2 PositionADroite = new Vector2(1,0);
    public Vector2 PositionRoomConnected;
    public Vector2 roomLol;
    private List<GameObject> player;
    private string nameTrigger;
    private ArrayList players = new ArrayList();

    //public GameObject TestPositionInstance;

    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosRoom(Vector2 pos)
    {
        PositionRoom = pos;
    }

    public Vector2 GetPosRoom()
    {
        return PositionRoom;
    }


    void FindGoodTrigger()
    {
        if ((float)directionX == -1.0f)
        {
            nameTrigger = "Right";
        }
        if ((float)directionX == 1.0f)
        {
            nameTrigger = "Left";
        }
        if ((float)directionY == -1.0f)
        {
            nameTrigger = "Up";
        }
        if ((float)directionY == 1.0f)
        {
            nameTrigger = "Down";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindGoodTrigger();
            players.Add(other.gameObject);

            RoomTransition [] room = GameObject.FindObjectsOfType<RoomTransition>();

            foreach (RoomTransition roomtransition in room)
            {


                roomLol.x = roomtransition.GetPosRoom().x;// + (float)directionX;
                roomLol.y = roomtransition.GetPosRoom().y;// + (float)directionY;

                PositionRoomConnected.x = this.GetPosRoom().x + (float)directionX;
                PositionRoomConnected.y = this.GetPosRoom().y + (float)directionY;

                //Debug.Log(nameTrigger);
                if (roomLol == PositionRoomConnected && players.Count == GameManager.instance.players.Count && roomtransition.name == nameTrigger )
                {
                    
                    foreach (GameObject player in players) {
                        player.gameObject.transform.position = roomtransition.transform.position;
                    }
                    var kk = roomtransition.transform.parent.gameObject;
                    Camera.main.transform.position = new Vector3( kk.transform.position.x, kk.transform.position.y,- 1.0f);

                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Remove(collision.gameObject);
        }
    }

    public enum DirectionX
    {
        RIGHT = 1,
        LEFT = -1,
        NONE = 0
    }

    public enum DirectionY
    {
        UP = 1,
        DOWN = -1,
        NONE = 0
    }
}
