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
	
	public Node (Node _parent, int _level, QuadTree _tree, Vector3 _position)
	{
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
//		if (level == 0 || ShouldSplit ()) {
		if (false){
			if (isDrawn) {
				GameObject.Destroy (prefab);
			}
			if (! isSplit) {
				Split ();
			} else {
				topLeftChild.Draw ();
				topRightChild.Draw ();
				bottomLeftChild.Draw ();
				bottomRightChild.Draw ();
			}
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
		}
	}

	private bool ShouldSplit ()
	{
		if (ContainsCamera ()) {
			float sd = (tree.sphere.splitDistance / level) - tree.sphere.radius;
			float td = Vector3.Distance (tree.sphere.camera.transform.localPosition, position + (tree.widthDir + tree.heightDir) * halfWidth);
			if (level > 0 && td < sd) {
//				return true;	
				return false;	
			}
		}
		return false;
	}
	
	private bool ContainsCamera ()
	{
		if(tree.containsCamera){
			float heightProjectionLength = Vector3.Distance(Vector3.Project(tree.heightDir, tree.cameraPoint),position);
			if(heightProjectionLength > 0){
				float widthProjectionLength = Vector3.Distance(Vector3.Project(tree.widthDir, tree.cameraPoint),position);
				if(widthProjectionLength > 0){
					if(heightProjectionLength < width && widthProjectionLength < width){
						Debug.Log (tree.name + " : " + level + " contained the camera");
						return true;
					}	
				}
			}
		}
		return false;
	}
	
	public void Split ()
	{
		float halfWidth = width / 2;
		topLeftChild = new Node (this, level + 1, tree, position - (tree.heightDir * halfWidth));
		topLeftChild.c = 1;
		
		topRightChild = new Node (this, level + 1, tree, position - (tree.heightDir + tree.widthDir) * halfWidth);
		topRightChild.c = 2;
		
		bottomLeftChild = new Node (this, level + 1, tree, position);
		bottomLeftChild.c = 3;
		
		bottomRightChild = new Node (this, level + 1, tree, position - (tree.widthDir * halfWidth));
		bottomRightChild.c = 4;
		
		isSplit = true;
	}
}
