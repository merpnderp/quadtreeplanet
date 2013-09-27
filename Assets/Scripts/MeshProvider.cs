using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A singleton class for providing copies of the 9 possible meshes in a quadtree.
/// </summary>


public sealed class MeshProvider
{
	
	private Mesh[] _meshes = new Mesh[9];
	private int _patchSize;
	private Vector3 _heightDir;
	private Vector3 _widthDir;

	public MeshProvider (int patchSize)
	{
		_patchSize = patchSize;
		_widthDir = Vector3.down;
		_heightDir = Vector3.left;
		
		CreateMeshes ();
	}

	private void CreateMeshes ()
	{
		MeshBuilder meshBuilder = new MeshBuilder();
		CreateMesh (meshBuilder);
		_meshes[0] = meshBuilder.CreateMesh();
		
		for (int i = 1; i < 9; i++) {
			_meshes[i] = new Mesh();
			_meshes[i].vertices = _meshes[0].vertices;
			_meshes[i].triangles = _meshes[0].triangles;
			_meshes[i].uv = _meshes[0].uv;
			_meshes[i].vertices = _meshes[0].vertices;
			_meshes[i].normals = _meshes[0].normals;
			_meshes[i].bounds = _meshes[0].bounds;
		}
	}
	
	private MeshBuilder CreateMesh (MeshBuilder meshBuilder)
	{
		float step = 1f / (_patchSize );
		
		for (int y = 0; y <= _patchSize; y++) {
		
			for (int x = 0; x <= _patchSize; x++) {
				
				Vector3 offset = (_widthDir * step * x) + (_heightDir * step * y);
//				Vector3 normal = Vector3.Cross(_widthDir, _heightDir);	
				Vector2 uv = new Vector2 (x,y);
				
				bool buildTriangles = x > 0 && y > 0;
				
				bool swapOrder = x % 2 == y % 2 ? true: false;
				
//				ProceduralQuad.BuildQuadForGrid (meshBuilder, offset, normal, uv, buildTriangles, _patchSize + 1, swapOrder);	
				ProceduralQuad.BuildQuadForGrid (meshBuilder, offset, uv, buildTriangles, _patchSize + 1, swapOrder);	
			}
		}
		return meshBuilder;
		
	}
	
	public Mesh GetStandardMesh ()
	{
		return _meshes[0];
	}
}
