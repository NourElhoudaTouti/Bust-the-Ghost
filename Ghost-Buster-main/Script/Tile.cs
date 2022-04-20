using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using static Game.initiate_game;

public class Tile : MonoBehaviour{
	[SerializeField] private Color _baseColor, _offgive_color;
    [SerializeField] private SpriteRenderer renderer;
    public static Vector3 latestClick;
    private int clicks = 0;
    [SerializeField] public TextMesh proba = null;

    public void Init(bool isOffset) {
        renderer.color = isOffset ? _offgive_color : _baseColor;
    }

    public void Mouse(){
        update_proba(renderer.transform.position, this);
        latestClick = renderer.transform.position;
    }

    public void give_color(color c){
        if(c == color.R)renderer.color = Color.R;
        else if (c == color.G)renderer.color = Color.G;
        else if (c == color.Y)renderer.color = new Color(246.0f,190.0f,0.0f);
        else if (c == color.O)renderer.color = new Color(0.95f,0.32f,0.09f,0.99f);

    }

    public void give_proba(double p){
        double percentage = p*100;
        string probability = percentage.ToString("0.000") + "%";
        if(this.p != null){
            this.p.text = probability;
        }
        else{
            Debug.Log(renderer.transform.position);
            this.p = UtilsClass.CreateWorldText(probability, null, renderer.transform.position, 25, Color.white, TextAnchor.MiddleCenter);
        }
    }

}
