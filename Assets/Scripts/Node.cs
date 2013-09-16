﻿using UnityEngine;
using System.Collections;

public class Node {

	private int _level;
	private float _width;
	
	private Node _parent;
	private QuadTree _tree;
	private Vector3 _position;
	
	// Children	
	private Node _topLeftChild;
	private Node _topRightChild;
	private Node _bottomLeftChild;
	private Node _bottomRightChild;
	
	// Neighbors
	public Node leftNeighbor;	
	public Node rightNeighbor;	
	public Node topNeighbor;
	public Node bottomBeighbor;
	
	public bool isSplit = false;
	public bool isDrawn = false;

	private MeshFilter _meshFilter;	
	
	public Node(Node parent, int level, QuadTree tree, Vector3 position){
		_parent = parent;
		_level = level;
		_tree = tree;
		_position = position;
		_width = (tree.radius * 2) / Mathf.Pow(2,level); 
	}
	
	public void Draw(){
		if(! isSplit){
			if(! isDrawn){
				GameObject prefab = _tree.sphere.GetPrefab();
				MeshRenderer mr = (MeshRenderer)prefab.GetComponent("MeshRenderer");
				mr.material.SetVector("HeightDir", _tree.heightDir);
				mr.material.SetVector("WidthDir", _tree.widthDir);
				mr.material.SetVector("Position", _position);
				mr.material.SetFloat("Width", _width);

				MeshFilter mf = (MeshFilter)prefab.GetComponent("MeshFilter");
				mf.mesh = _tree.sphere.meshProvider.GetStandardMesh();
				mf.mesh.RecalculateBounds();	
				
				isDrawn = true;
			}
		}else{
			_topLeftChild.Draw();
			_topLeftChild.Draw();
			_bottomLeftChild.Draw();
			_bottomRightChild.Draw();
		}
	}
	
	public void Split(){
		
	}
}
