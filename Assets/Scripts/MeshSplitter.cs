using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshSplitter
{
 
// Maps old vertex indices to new ones.
	private Dictionary<int, int> _map = new Dictionary<int, int> ();
 
// Input mesh data.
	int[] _triangles;
	Vector3[] _verts;
	Vector2[] _uvs;
	Vector2[] _normals;
	int _meshSize;
 
// Temporary mesh data.
	List<int> _tempTriangles = new List<int> ();
	List<Vector3> _tempVerts = new List<Vector3> ();
	List<Vector2> _tempUVs = new List<Vector2> ();
	List<Vector2> _tempNormals = new List<Vector2> ();
 
	public MeshSplitter (int[] triangles, Vector3[] verts, Vector2[] uvs, int meshSize)
	{
		_triangles = triangles;
		_verts = verts;
		_uvs = uvs;
		_meshSize = meshSize;
	}
 
	public Mesh[] SplitMesh ()
	{
		List<Mesh> meshes = new List<Mesh> ();
 
		StartMesh ();
 
		for (int tri = 0; tri < _triangles.Length; tri += 3) {
// Might overflow occur?
			if (_tempVerts.Count >= _meshSize) {
				meshes.Add (GenerateMesh ());
				StartMesh ();
			}
 
			_tempTriangles.Add (MapVertex (_triangles [tri + 2]));
			_tempTriangles.Add (MapVertex (_triangles [tri + 1]));
			_tempTriangles.Add (MapVertex (_triangles [tri]));
		}
 
		if (_tempVerts.Count > 0)
			meshes.Add (GenerateMesh ());
 
		return meshes.ToArray ();
	}
 
	private void StartMesh ()
	{
		_tempTriangles.Clear ();
		_tempVerts.Clear ();
		_tempUVs.Clear ();
 
		_map.Clear ();
	}
 
	private Mesh GenerateMesh ()
	{
		var mesh = new Mesh ();
		mesh.vertices = _tempVerts.ToArray ();
		mesh.uv = _tempUVs.ToArray ();
		mesh.triangles = _tempTriangles.ToArray ();
		return mesh;
	}
 
	private int MapVertex (int inputIndex)
	{
		if (!_map.ContainsKey (inputIndex)) {
			_map [inputIndex] = _tempVerts.Count;
			_tempVerts.Add (_verts [inputIndex]);
			_tempUVs.Add (_uvs [inputIndex]);
		}
		return _map [inputIndex];
	}
 
}