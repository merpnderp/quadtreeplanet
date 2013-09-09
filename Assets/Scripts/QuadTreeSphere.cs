using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadTreeSphere : MonoBehaviour
{


	public int radius;
	public int patchSize = 32;
	public int maxLevel;
	
	private List<QuadTree> quadTrees = new List<QuadTree> ();
	
	private MeshBuilder meshBuilder = new MeshBuilder();
	
	private MeshFilter filter;
	
	// Use this for initialization
	void Start ()
	{
		filter = GetComponent<MeshFilter>();
		init();
	}

	public void init(){
		maxLevel = (int)Mathf.Log (radius * 2f);
		maxLevel -= (int)Mathf.Log (Mathf.Pow (patchSize, 2));
		maxLevel = maxLevel < 0 ? 0 : maxLevel;
	
		Vector3 farCorner = (Vector3.right + Vector3.up + Vector3.forward) * radius;
	//	Vector3 nearCorner =(Vector3.left + Vector3.down + Vector3.back) * radius;
	    Vector3 nearCorner = -farCorner;
		
		//near corner quads	
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, nearCorner, Vector3.forward, Vector3.right, meshBuilder));// Bottom
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, nearCorner, Vector3.right, Vector3.up, meshBuilder));// Front
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, nearCorner, Vector3.up, Vector3.forward, meshBuilder));// Left
		//far corner quads	
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, farCorner, Vector3.left, Vector3.back, meshBuilder));// Top
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, farCorner, Vector3.down, Vector3.left, meshBuilder));// Back
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, farCorner, Vector3.back, Vector3.down, meshBuilder));// Right
		foreach(QuadTree tree in quadTrees){
			tree.Draw();
		}
		filter.sharedMesh = meshBuilder.CreateMesh();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//foreach(QuadTree tree in quadTrees){
		//	tree.Draw();
		//}
	}
}
