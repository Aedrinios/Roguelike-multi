using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour {

    public DirectionX directionX = DirectionX.NONE;
    public DirectionY directionY = DirectionY.NONE;

    public Vector2 PositionRoom = new Vector2();
    //public Vector2 PositionADroite = new Vector2(1,0);
    public Vector2 PositionRoomPlayer;
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
                //Debug.Log(nameTrigger);
                if (roomtransition.GetPosRoom() == PositionRoomPlayer && players.Count == GameManager.instance.players.Count )
                {

                    
                    PositionRoomPlayer = this.GetPosRoom();

                    PositionRoomPlayer.x = this.GetPosRoom().x + (float)directionX;
                    PositionRoomPlayer.y = this.GetPosRoom().y + (float)directionY;


                    foreach (GameObject player in players) {
                        player.gameObject.transform.position = roomtransition.transform.position;
                    }
                    var kk = roomtransition.transform.parent.gameObject;
                    Camera.main.transform.position = new Vector3( kk.transform.position.x, kk.transform.position.y,- 1.0f);

                }

                if (roomtransition.name == "Left") Debug.Log("LOL cetait Left");
                if (roomtransition.name == "Right") Debug.Log("LOL cetait Right");
                if (roomtransition.name == "Down") Debug.Log("LOL cetait Down");
                if (roomtransition.name == "Up") Debug.Log("LOL cetait Up");
            }
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
