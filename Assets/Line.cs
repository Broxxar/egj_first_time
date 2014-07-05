using UnityEngine;
using System.Collections;

public class Line {

	Vector2 _startPos;
	Vector2 _endPos; 

	public Vector2 StartPos{
		get{ return _startPos;}
		set{_startPos = value;}
	}

	public Vector2 EndPos{
		get{ return _endPos;}
		set{_endPos = value;}
	}

	public Line (Vector2 startP, Vector2 endP){
		StartPos = startP;
		EndPos = endP;
	}
	
}
