using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Tile;

public class ButtonBehavior : MonoBehaviour
{
	public Button btn;

	void Start(){
		btn.onClick.AddListener(TaskOnCick);
	}

    public void TaskOnCick(){
    	if(latestClick.x == ghost.x && latestClick.y == ghost.y){
    		Debug.Log("Congrats! You ghost_busteded the ghost");
        }
        else{
            Debug.Log("Game over!");
        }
        UnityEditor.EditorApplication.isPlaying = false;
	}
}

