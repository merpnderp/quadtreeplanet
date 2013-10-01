using UnityEngine;
using System.Collections;

public class QuadTree
{
	
	public int maxLevel;
	public int patchSize;
	public int radius;
	public QuadTreeSphere sphere;
	public Vector3 widthDir;
	public Vector3 heightDir;
	public Plane plane;
	public Vector3 cameraPoint;
	public bool containsCamera = false;
	
	private Vector3 startPosition;
	private Node rootNode;
	public string name;
	
	public QuadTree (int _maxLevel, int _patchSize, int _radius, Vector3 _startPosition, 
		Vector3 _widthDir, Vector3 _heightDir, QuadTreeSphere _sphere, string _name)
	{
		name = _name;
		maxLevel = _maxLevel;
		patchSize = _patchSize;
		radius = _radius;
		startPosition = _startPosition;
		widthDir = _widthDir;
		heightDir = _heightDir;
		sphere = _sphere;
		rootNode = new Node (null, 0, this, startPosition);
		plane = new Plane(Vector3.Cross(widthDir,heightDir), radius);
	}

	public void Draw ()
	{
		Ray cameraRay = new Ray(Vector3.zero, sphere.camera.transform.position);
		float distance = 0;
		containsCamera = plane.Raycast(cameraRay, out distance);
		if(containsCamera){
			cameraPoint = Vector3.Normalize(sphere.camera.transform.position) * distance;
//			Debug.Log(name + " had an intersection at: " + cameraPoint.ToString());
		}
		rootNode.Draw ();
	}

	
	public void AssignNeighbors (Node left, Node top, Node right, Node bottom)
	{
		rootNode.leftNeighbor = left;
		rootNode.topNeighbor = top;
		rootNode.rightNeighbor = right;
		rootNode.bottomBeighbor = bottom;
	}
	
	public Node GetRootNode ()
	{
		return rootNode;
	}
}
