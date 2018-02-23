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

    void OntriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")

        PositionRoomPlayer = this.GetPosRoom();
        PositionRoomPlayer.x = this.GetPosRoom().x + (float)directionX;
        PositionRoomPlayer.y = this.GetPosRoom().y + (float)directionY;
        RoomTransition room = GameObject.FindObjectOfType<RoomTransition>();
        if (room.GetPosRoom() == PositionRoomPlayer)
        {
            other.gameObject.transform.position = room.transform.position;
            Debug.Log(room.transform.position);
            Debug.Log(this.GetPosRoom());
            Debug.Log("tu l'as trouvé frere");
            Debug.Log(room.GetPosRoom());
        }
        else
        {
            room = GameObject.FindObjectOfType<RoomTransition>();
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
