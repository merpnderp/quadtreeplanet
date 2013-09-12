using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A singleton class for providing copies of the 9 possible meshes in a quadtree.
/// </summary>


public sealed class MeshProvider {
	
	private List<MeshBuilder> _meshBuilders = new List<MeshBuilder>();

	public MeshProvider( int patchSize, Vector3 heightDir, Vector3 widthDir){
		for(int i = 0; i < 9; i++){
			
			for(int x = 0; x <= patchSize; x++){
				
				for(int y = 0; y <= patchSize; y++){
					
				}
			}
				
			
		}	
	}
	
	public Mesh GetStandardMesh(){
		return _meshBuilders[0].CreateMesh();
	}
	
}
