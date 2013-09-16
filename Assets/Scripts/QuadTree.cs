﻿using UnityEngine;
using System.Collections;

public class QuadTree
{
	
	public int maxLevel;
	public int patchSize;
	public int radius;
	public QuadTreeSphere sphere;
	private Vector3 startPosition;
	public Vector3 widthDir;
	public Vector3 heightDir;
	private Node rootNode;
	
	public QuadTree (int _maxLevel, int _patchSize, int _radius, Vector3 _startPosition, 
		Vector3 _widthDir, Vector3 _heightDir, QuadTreeSphere _sphere)
	{
		maxLevel = _maxLevel;
		patchSize = _patchSize;
		radius = _radius;
		startPosition = _startPosition;
		rootNode = new Node (null, 0, this, startPosition);
		widthDir = _widthDir;
		heightDir = _heightDir;
		sphere = _sphere;
	}

	public void Draw ()
	{
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
