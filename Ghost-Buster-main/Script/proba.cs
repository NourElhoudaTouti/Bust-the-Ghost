using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Game {
		public static Vector2 ghost;
		public static Dictionary<Vector2, Tile> tiles;
		public static int w;
		public static int l;
		public static double [,] proba;
		public enum color {R, Y, G, O, Null};
		public static Dictionary<color, double> R_Dict = new Dictionary<color, double>() {
		{ color.R, 0.87 }, { color.O, 0.10 }, { color.Y, 0.02 }, { color.G, 0.01 },};
		public static Dictionary<color, double> O_Dict = new Dictionary<color, double>() {
		{ color.R, 0.08 }, { color.O, 0.88 }, { color.Y, 0.03 }, { color.G, 0.01 },};
		public static Dictionary<color, double> Y_Dict = new Dictionary<color, double>() {
		{ color.R, 0.01 }, { color.O, 0.04 }, { color.Y, 0.86 }, { color.G, 0.08 },};
		public static Dictionary<color, double> G_Dict = new Dictionary<color, double>() {
		{ color.R, 0.04 }, { color.O, 0.06 }, { color.Y, 0.10 }, { color.G, 0.80 },};            
		
	}
	
	public class initiate_game {
		public static void init(Dictionary<Vector2, Tile> tiles, int w, int height) {
			this.tiles = tiles;
			this.w = w;
			l = height;
			Random_Ghost_Position();
		 	proba = new double [w, l];
		 	init_proba();
	    }
	    
	    public static void init_proba() {
	        double InitProba = (double) 1 / ( w * l );
	        for(int x = 0; x < tiles.Count; x++) {
	        	Tile tile = tiles.ElementAt(x).Value;
	            tile.give_proba(InitProba);
	        }
	        for (int x = 0; x < w; x++) {
	            for (int y = 0; y < l; y++) probabilities[i,j] = init_proba;
	        }
	    }

	    public static void Random_Ghost_Position() {
	    	var ghost = new Vector2(Random.Range(0, w), Random.Range(0, l));
	        this.ghost = ghost;
	        Debug.Log("The ghost is at position : " + ghost);
	    }
	    

	    // this should be called by the onMouseDown() and should take the position of the tile
	    // Change the proba of the clicked tile and normalization with other cells
	    public static void update_proba(Vector3 tile_clicked, Tile tile) {
	        // Run on each click.
	        int x = (int)tile_clicked.x;
	        int y = (int)tile_clicked.y;
	        double prev_prob = probabilities[x, y];
	        int distance = ghost_distance(x, y);
	        color color_at_current_cell = color(distance);
	        double color_prob = conditional_proba(color_at_current_cell, distance);
	        double updated_proba = prev_prob * color_prob;
	        probabilities[x, y] = updated_proba;
	        tile.give_color(color_at_current_cell);
	        tile.give_proba(updated_proba);
	        normalization();
	    }

	    public static void normalization() {
	        double total_sum = 0.0;
	        for (int x = 0; x < w; x++) {
	            for (int y = 0; y < l; y++) total_sum += probabilities[x, y];
	        }
	        for (int x = 0; x < w; x++) {
	            for (int y = 0; y < l; y++) { 
	            	probabilities[x,y] = probabilities[x,y] / total_sum;
	                Vector2 _vector = new Vector2((float)x, (float)y);
	                Tile tile = GetTileAtPosition(_vector);
	                tile.give_proba(probabilities[x, y]);
	            }
	        }
	    }
	    
	    public static int ghost_distance(int x, int y) {return (int)(ghost.x - x + ghost.y - y);}

	    public static double conditional_proba(color color, int distance) {
			double probability;
	        distribution(distance).TryGetValue(color, out probability);
			return probability;
	    }
	    
	    public static bool ghost_busted(Vector2 busted) {
	    	return busted.x == ghost.x && busted.y == ghost.y;
	    }
	    
	    public static Dictionary<color, double> distribution(int distance_from_ghost) {
	        if (distance_from_ghost == 0) return R_Dict;
	        if (distance_from_ghost == 1 || distance_from_ghost == 2) return O_Dict;
	        if (distance_from_ghost == 3 || distance_from_ghost == 4) return Y_Dict;
	        if (distance_from_ghost >= 5) return G_Dict;
			return G_Dict;
	    }
	    
	    public static color color(int distance) {
	        var random_value = new System.Random();
	        double random = random_value.NextDouble();
	        Dictionary<color, double> distribution = distribution(distance);
	        List<double> p = System.Linq.Enumerable.ToList(distribution.Values); p.Sort();
	        double match_proba = p[rand.Next(0, 4)];
			if (random <= p[0])match_proba = p[0];
			if (random > p[0] && random <= p[0] + p[1])match_proba = p[1];
			if (random > p[0] + p[1] && random <= p[0] + p[1] + p[2])match_proba = p[2];
			if (random > p[0] + p[1] + p[2] && random <= p[0] + p[1] + p[2] + p[3])match_proba = p[3];

			foreach(KeyValuePair<color, double> e in dist) {
				if (e.Value == match_proba) {
					return e.Key;
				}
			}
			return color.O;
	    }
	    
	    
	}