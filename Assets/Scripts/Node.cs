using UnityEngine;
using System.Collections;

public class Node
{

	private int level;
	private float width;
	private float halfWidth;
	private Node parent;
	private QuadTree tree;
	private Vector3 position;
	public string name;
	
	// Children	
	private Node topLeftChild;
	private Node topRightChild;
	private Node bottomLeftChild;
	private Node bottomRightChild;
	
	// Neighbors
	public Node leftNeighbor;
	public Node rightNeighbor;
	public Node topNeighbor;
	public Node bottomBeighbor;
	public float c = 1;
	public bool isSplit = false;
	public bool isDrawn = false;
	private GameObject prefab;
	
	public Node (Node _parent, int _level, QuadTree _tree, Vector3 _position, string _name)
	{
		name = _name;
		parent = _parent;
		//rootnode is level 0
		level = _level;
		tree = _tree;
		position = _position;
		width = (tree.radius * 2) / Mathf.Pow (2, level); 
		halfWidth = width / 2;
	}
	
	public void Draw ()
	{
		if (level == 0 || (ShouldSplit () && level < 4)) {
			if (isDrawn) {
				GameObject.Destroy (prefab);
			}
			if (! isSplit) {
				Split ();
			} 
			topLeftChild.Draw ();
			topRightChild.Draw ();
			bottomLeftChild.Draw ();
			bottomRightChild.Draw ();
			
		} else if (! isDrawn) {
			prefab = tree.sphere.GetPrefab ();
			MeshRenderer mr = (MeshRenderer)prefab.GetComponent ("MeshRenderer");
			mr.renderer.material.SetVector ("HeightDir", tree.heightDir);
			mr.renderer.material.SetVector ("WidthDir", tree.widthDir);
			mr.renderer.material.SetVector ("Position", position);
			mr.renderer.material.SetFloat ("Width", width);
			mr.renderer.material.SetFloat ("c", c);	
			MeshFilter mf = (MeshFilter)prefab.GetComponent ("MeshFilter");
			mf.mesh = tree.sphere.meshProvider.GetStandardMesh ();
			mf.mesh.RecalculateBounds ();	
			isDrawn = true;
			Debug.Log (name + " : " + tree.widthDir + " : " + tree.heightDir + " : " + position);
		}
	}

	private bool ShouldSplit ()
	{
		if (ContainsCamera ()) {
			float sd = (tree.sphere.splitDistance / (level + 1)) - tree.sphere.radius;
			float td = Vector3.Distance (tree.localMatrix.MultiplyPoint (tree.sphere.camera.transform.position), position + (tree.widthDir + tree.heightDir) * halfWidth);
			Debug.Log(sd + " : " + td);
			if (td < sd) {
				return true;	
			}
		}
		return false;
	}
	
	private bool ContainsCamera ()
	{
		if (tree.containsCamera) {
//			Debug.Log (tree.name + " : " + level + " " + name + " contained the camera: " + width + " : " + position + " : " + tree.cameraPoint);
			if(InThisAxis(tree.cameraPoint, position, tree.heightDir, width)){
				if(InThisAxis(tree.cameraPoint, position, tree.widthDir, width)){
					return true;
				}	
			}
		}
		return false;
	}
	
	private bool InThisAxis(Vector3 c, Vector3 p, Vector3 axis, float w){
		bool i = false;
		if(axis.x == -1){
			i = (p.x > c.x && c.x > p.x + (w * Mathf.Sign(axis.x)));
		}else if(axis.x == 1){
			i = (p.x < c.x && c.x < p.x + (w * Mathf.Sign(axis.x)));
			
		}else if(axis.y == -1){
			i = (p.y > c.y && c.y > p.y + (w * Mathf.Sign(axis.y)));
		}else if(axis.y == 1){
			i = (p.y < c.y && c.y < p.y + (w * Mathf.Sign(axis.y)));
			
		}else if(axis.z == -1){
			i = (p.z > c.z && c.z > p.z + (w * Mathf.Sign(axis.z)));
		}else{
			i = (p.z < c.z && c.z < p.z + (w * Mathf.Sign(axis.z)));
		}
//		Debug.Log(i);
		return i;
	}
	
	public void Split ()
	{
		float halfWidth = width / 2;
		topLeftChild = new Node (this, level + 1, tree, position + (tree.heightDir * halfWidth), "BottomLeft");
		topLeftChild.c = 1;
		
		topRightChild = new Node (this, level + 1, tree, position + (tree.heightDir + tree.widthDir) * halfWidth, "BottomRight");
		topRightChild.c = 2;
		
		bottomLeftChild = new Node (this, level + 1, tree, position, "TopLeft");
		bottomLeftChild.c = 3;
		
		bottomRightChild = new Node (this, level + 1, tree, position + (tree.widthDir * halfWidth), "TopRight");
		bottomRightChild.c = 4;
		
		isSplit = true;
	}
}
