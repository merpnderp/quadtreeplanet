using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A singleton class for providing copies of the 9 possible meshes in a quadtree.
/// </summary>


public sealed class MeshProvider
{
	
	private MeshBuilder[] _meshBuilders = new MeshBuilder[9];
	private int _patchSize;
	private Vector3 _heightDir;
	private Vector3 _widthDir;

	public MeshProvider (int patchSize, Vector3 heightDir, Vector3 widthDir)
	{
		_patchSize = patchSize;
		_heightDir = heightDir;
		_widthDir = heightDir;
		
		CreateMeshes ();
			
		for (int x = 0; x <= patchSize; x++) {
				
			for (int y = 0; y <= patchSize; y++) {
					
			}
		}
	}

	private void CreateMeshes ()
	{

		for (int i = 0; i < 9; i++) {
			_meshBuilders[i] = new MeshBuilder();
			CreateMesh (i);
		}
		
	}
	
	private void CreateMesh (int i)
	{
		float step = 1f / _patchSize;
		
		for (int x = 0; x <= _patchSize; x++) {
		
			for (int y = 0; y <= _patchSize; y++) {
				
				Vector3 offset = (_widthDir * step * x) + (_heightDir * step * y);
				
				Vector2 uv = new Vector2 (x,y);
				
				bool buildTriangles = x > 0 && y > 0;
				bool swapOrder = y % 2 == 0 ? false: true;	
				ProceduralQuad.BuildQuadForGrid (_meshBuilders[i], offset, uv, buildTriangles, _patchSize + 1, swapOrder);	
				
			}
		}
		
	}
	
	public Mesh GetStandardMesh ()
	{
		return _meshBuilders [0].CreateMesh ();
	}
	
}
