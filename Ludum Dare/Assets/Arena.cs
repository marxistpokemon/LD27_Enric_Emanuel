using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Arena : MonoBehaviour {
	
	public Transform myTransform;
	
	public Tile tilePrefab;
	
	public Tile[,] tiles;
	
	public float tileSize = 5f;
	public int numColumns = 6;
	public int numRows = 3;
	
	public Material grid1;
	public Material grid2;
	
	public Player player;
	
	// Use this for initialization
	void Start () {
		
		myTransform = transform;
		
		tiles = new Tile[numRows, numColumns];
		
		Vector3 startPos = new Vector3(
			myTransform.position.x -numRows/2*tileSize,
			myTransform.position.y,
			myTransform.position.z -numColumns*tileSize/2);
		
		bool isGray = true;
		
		for (int row = 0; row < numRows; row++) {
			
			bool isGrayCopy = !isGray;
			isGray = isGrayCopy;
			
			for (int col = 0; col < numColumns; col++) {
				startPos.z += tileSize;
				
				tiles[row, col] = Instantiate(tilePrefab, startPos, 
					tilePrefab.transform.rotation) as Tile;
				tiles[row, col].transform.parent = myTransform;
				tiles[row, col].transform.localScale = new Vector3(tileSize, tileSize, 1);
				tiles[row, col].gridPos = new Coord(row, col);
				
				if(isGrayCopy){
					tiles[row, col].renderer.sharedMaterial = grid1;
				}
				else {
					tiles[row, col].renderer.sharedMaterial = grid2;
				}
				isGrayCopy = !isGrayCopy;
			}
			
			startPos.x += tileSize;
			startPos.z = myTransform.position.z -numColumns*tileSize/2;
		}
		
		// player comeca no 1,1
		player.gridPos = new Coord(1,1);
		player.transform.position = new Vector3(
			tiles[1,1].transform.position.x,
			player.transform.position.y,
			tiles[1,1].transform.position.z);
		tiles[1,1].isOccuppied = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		HandleMovement();
	
	}
	
	void HandleMovement(){

		if(!player.isMoving){
			
			player.input = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
			player.input.Normalize();
			
			// se nao tem input do jogador
			if(player.input == Vector3.zero){
				return;
			}
			
			Coord newPos = new Coord(player.gridPos.r, player.gridPos.c);
			
			if(Mathf.Abs(player.input.x) > Mathf.Abs(player.input.z)){
				// anda nas linhas
				
				if(player.input.x > 0){ // ir para baixo
					newPos.r = Mathf.Clamp(player.gridPos.r+1, 0, numRows);
					
				}
				else { // ir para cima
					newPos.r = Mathf.Clamp(player.gridPos.r-1, 0, numRows);
				}
			}
			else {
				// anda nas colunas
				if(player.input.z > 0){ // ir para direita
					newPos.c = Mathf.Clamp(player.gridPos.c+1, 0, numColumns);
				}
				else { // ir para esquerda
					newPos.c = Mathf.Clamp(player.gridPos.c-1, 0, numColumns);
				}
			}
			
			// checar se precisa de fato mover
			if(newPos.Equals(player.gridPos) || 
				tiles[newPos.r, newPos.c].isOccuppied){
				return;
			}
			
			// move o player
			player.isMoving = true;
			tiles[player.gridPos.r,player.gridPos.c].isOccuppied = false;
			
			Vector3 tweenEnd = tiles[newPos.r, newPos.c].transform.position;
			tweenEnd.y = player.transform.position.y;
			
			Go.to (player.transform, player.moveDuration, new GoTweenConfig()
				.position(tweenEnd)
				.setEaseType(GoEaseType.QuadInOut))
				.setOnCompleteHandler(move => {
					player.isMoving = false;
					player.gridPos = newPos;
					tiles[newPos.r, newPos.c].isOccuppied = true;
				});
		}
	}
}


