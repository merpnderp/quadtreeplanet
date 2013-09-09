using UnityEngine;
using System.Collections;


public class QuadTree{
	
	public int maxLevel;
	public int patchSize;
	public int radius;
	public MeshBuilder meshBuilder;
	private Vector3 startPosition;
	public Vector3 widthDir;
	public Vector3 heightDir;
	
	private Node rootNode;
	
	public QuadTree(int _maxLevel, int _patchSize, int _radius, Vector3 _startPosition, Vector3 _widthDir, Vector3 _heightDir, MeshBuilder _meshBuilder){
		maxLevel = _maxLevel;
		patchSize = _patchSize;
		radius = _radius;
		startPosition = _startPosition;
		rootNode = new Node(null, 0, this, startPosition);
		widthDir = _widthDir;
		heightDir = _heightDir;
		meshBuilder = _meshBuilder;
	}

	public void Draw(){
		rootNode.Draw ();
		meshBuilder.CreateMesh();	
	}
}
