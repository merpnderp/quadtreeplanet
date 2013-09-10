using UnityEngine;
using System.Collections;

public class Node {

	private int level;
	private float size;
	
	private Node parent;
	private QuadTree tree;
	public Vector3 startPosition;
	
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
	
	public Node(Node _parent, int _level, QuadTree _tree, Vector3 _position){
		parent = _parent;
		level = _level;
		tree = _tree;
		startPosition = _position;
		size = (tree.radius * 2) / Mathf.Pow(2,level); 
	}
	
	public void Draw(){
	
		var step = size / tree.patchSize;
		Vector3 heightStep = tree.heightDir * step;	
		Vector3 widthStep = tree.widthDir * step;	
		
		for(int x = 0; x < tree.patchSize; x++){
		
			for(int y = 0; y < tree.patchSize; y++){
				
				Vector3 offset = startPosition + (tree.widthDir * step * x) + (tree.heightDir * step * y);
				
				ProcBase.BuildQuad(tree.meshBuilder, offset, widthStep, heightStep);
				
			}
		}
	}
	
	public void Split(){
		
	}
}
