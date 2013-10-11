using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class QuadTreeSphere : MonoBehaviour
{


	public int radius;
	public int patchSize = 32;
	public int maxLevel;
	public int splitDistance = 500;
	
	public GameObject playerObject;
	public Camera camera;
	public Vector3 oldCameraPosition;
	
	public GameObject PlanetMeshPrefab;
	
	private List<QuadTree> quadTrees = new List<QuadTree> ();
	
	public MeshProvider meshProvider;
	
	// Use this for initialization
	void Start ()
	{
		init();
	}

	public void init(){
		
		meshProvider = new MeshProvider(patchSize);
		MeshFilter mf = (MeshFilter)PlanetMeshPrefab.GetComponent ("MeshFilter");
		mf.sharedMesh = meshProvider.GetStandardMesh ();
		mf.sharedMesh.RecalculateBounds ();	
		
		maxLevel = (int)Mathf.Log (radius * 2f);
		maxLevel -= (int)Mathf.Log (Mathf.Pow (patchSize, 2));
		maxLevel = maxLevel < 0 ? 0 : maxLevel;
	
		Vector3 nearCorner = (Vector3.right + Vector3.up + Vector3.forward) * radius;
	    Vector3 farCorner = -nearCorner;
		
		//near corner quads	
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, nearCorner, Vector3.back, Vector3.left, this, "Bottom"));// Bottom
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, nearCorner, Vector3.left, Vector3.down, this, "Front"));// Front
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, nearCorner, Vector3.down, Vector3.back, this, "Left"));// Left
		
		//far corner quads	
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, farCorner, Vector3.right, Vector3.forward, this, "Top"));// Top
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, farCorner, Vector3.up, Vector3.right, this, "Back"));// Back
		quadTrees.Add(new QuadTree (maxLevel, patchSize, radius, farCorner, Vector3.forward, Vector3.up, this, "Right"));// Right
		//AssignNeighbors();
		
	}
	
	// Update is called once per frame
	void Update (){
//		if(playerObject.transform.position != oldCameraPosition){
			oldCameraPosition = playerObject.transform.position;
			foreach(QuadTree tree in quadTrees){
				tree.Draw();
			}
//		}
	}

	public GameObject GetPrefab(){
		GameObject gm = Instantiate(PlanetMeshPrefab) as GameObject;
		gm.transform.parent = (Transform)GetComponent("Transform");
//		MeshFilter mf = (MeshFilter)gm.GetComponent ("MeshFilter");
//		mf.sharedMesh = meshProvider.GetStandardMesh ();
//		mf.sharedMesh.RecalculateBounds ();	
//		gm.transform.localRotation = Quaternion.identity;
//		gm.transform.localPosition = new Vector3();
		return gm;
	}	
	
/*	void LoadMeshes(){
		MeshSplitter meshSplitter = new MeshSplitter(meshBuilder.Triangles.ToArray(), meshBuilder.Vertices.ToArray(),
			meshBuilder.UVs.ToArray(), 65534 );
		Mesh[] meshes = meshSplitter.SplitMesh(); 
		for(int i = 0; i < meshes.Length; i++){
			if(PlanetMeshPrefabs.Count < i + 1){
				PlanetMeshPrefabs.Add(Instantiate(PlanetMeshPrefab) as GameObject);
//				PlanetMeshPrefabs[i].transform.parent = (Transform)GetComponent("Transform");
//				PlanetMeshPrefabs[i].transform.localRotation = Quaternion.identity;
//				PlanetMeshPrefabs[i].transform.localPosition = new Vector3();
			}
			MeshFilter filter = (MeshFilter)PlanetMeshPrefabs[i].GetComponent("MeshFilter");
			
			filter.mesh.vertices = meshes[i].vertices;
			filter.mesh.uv = meshes[i].uv;
			filter.mesh.normals = meshes[i].normals;
			filter.mesh.triangles = meshes[i].triangles;	
			filter.mesh.RecalculateNormals();
		}
	}*/
	
	private void AssignNeighbors(){
		Node bottom = quadTrees[0].GetRootNode();
		Node front = quadTrees[1].GetRootNode();
		Node left = quadTrees[2].GetRootNode();
		Node top = quadTrees[3].GetRootNode();
		Node back = quadTrees[4].GetRootNode();
		Node right = quadTrees[5].GetRootNode();
		
		quadTrees[0].AssignNeighbors(left, back, right, front);	
		quadTrees[1].AssignNeighbors(left, top, right, bottom);	
		quadTrees[2].AssignNeighbors(bottom, back, top, front);	
		quadTrees[3].AssignNeighbors(right, front , left, back);	
		quadTrees[4].AssignNeighbors(top, left , bottom, right);	
		quadTrees[5].AssignNeighbors(back, bottom , front, top);	
	}
}
