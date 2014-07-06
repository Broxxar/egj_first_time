using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeManager : MonoBehaviour {
	private static NodeManager _instance;

	
	public static NodeManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<NodeManager>();
			}
			return _instance;
		}
		
		private set { }
	}

	List<Clocks> _clocks = new List<Clocks>();
	public Vector2 MouseWorldPosition {get; private set;}
	
	public void AddNode(Clocks node)
	{
		if (!(_clocks.Contains (node) || _clocks.Contains (node.PartnerNode)))
		{
			_clocks.Add (node);
		}
	}
	
	
	public void RemoveNode(Clocks node)
	{
		if(_clocks.Contains(node))
		{
			_clocks.Remove(node);
		}else if(_clocks.Contains(node.PartnerNode))
		{
			_clocks.Remove (node.PartnerNode);
		}	
	}

	public Clocks DifferentClockHit(Clocks current)
	{
		MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D collider = Physics2D.OverlapPoint(MouseWorldPosition);
		if (collider !=null)
		if(Input.GetMouseButton(0))
		{
			foreach (Clocks node in _clocks) 
			{	
				if(node != current && collider.Equals(node.collider2D))
				{
					return node;
				}
			}
		}
		return null;
	}

	public void BreakAll(){
		foreach (Clocks node in _clocks) 
		{	

				node.BreakLine();

		}
	}


}
