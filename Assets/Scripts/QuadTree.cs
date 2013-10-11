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
	public Matrix4x4 localMatrix;
	
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
		rootNode = new Node (null, 0, this, startPosition, name + "RootNode");
		plane = new Plane (Vector3.Cross (heightDir, widthDir), startPosition);
	}

	public void Draw ()
	{
		localMatrix = sphere.transform.worldToLocalMatrix;
		Ray cameraRay = new Ray (Vector3.zero, localMatrix.MultiplyPoint (sphere.playerObject.transform.position));
		float distance = 0;
		containsCamera = plane.Raycast (cameraRay, out distance);
		
//		if(containsCamera && distance >= 0){
		if (containsCamera) {
			cameraPoint = Vector3.Normalize (sphere.playerObject.transform.position) * distance;
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
