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
		
		for(int y = 0; y <= tree.patchSize; y++){
		
			for(int x = 0; x <= tree.patchSize; x++){
				
				Vector3 offset = startPosition + (tree.widthDir * step * x) + (tree.heightDir * step * y);
				offset = Vector3.Normalize(offset) * tree.radius;
				bool buildTriangles = x > 0 && y > 0;
				Vector2 uv = new Vector2();
				bool swapOrder = x % 2 == y % 2 ? true: false;
				
				ProceduralQuad.BuildQuadForGrid(tree.meshBuilder, offset, uv, buildTriangles, tree.patchSize+1, swapOrder);	
				
			}
		}
	}
	
	public void Split(){
		
	}
}
