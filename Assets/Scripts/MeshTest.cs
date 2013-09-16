using UnityEngine;
using System.Collections;
public class MeshTest : MonoBehaviour
{

	public GameObject PlanetMeshPrefab;
	private MeshBuilder _meshBuilder = new MeshBuilder ();
	// Use this for initialization
	GameObject go;
	GameObject mesh1;
	GameObject mesh2;
	Mesh mesh;

	void Start ()
	{
		int step = 1;
		int patchSize = 50;	
		Vector3 startPosition = new Vector3 ();
	
		for (int y = 0; y <= patchSize; y++) {

			for (int x = 0; x <= patchSize; x++) {
				
				Vector3 offset = startPosition + (Vector3.right * step * x) + (Vector3.up * step * y);
				bool buildTriangles = x > 0 && y > 0;
				Vector2 uv = new Vector2 ();
				bool swapOrder = x % 2 == y % 2 ? true: false;
				ProceduralQuad.BuildQuadForGrid (_meshBuilder, offset, uv, buildTriangles, patchSize + 1, swapOrder);	
			}
		}
		mesh = _meshBuilder.CreateMesh ();
		MeshProvider mp = new MeshProvider(32);
		mesh = mp.GetStandardMesh();
		mesh1 = Instantiate (PlanetMeshPrefab) as GameObject;
		mesh2 = Instantiate (PlanetMeshPrefab) as GameObject;
		
		go = new GameObject ();
		go.AddComponent ("MeshFilter");
		go.AddComponent ("MeshRenderer");
//		go.transform.Translate (Vector3.right * 50);
		
//		mesh1.transform.Translate (Vector3.right * 100);
	}
	
	// Update is called once per frame
	void Update ()
	{
		MeshFilter mf = (MeshFilter)go.GetComponent ("MeshFilter");
		mf.mesh = mesh;
		MeshFilter meshFilter1 = (MeshFilter)mesh1.GetComponent ("MeshFilter");
		MeshFilter meshFilter2 = (MeshFilter)mesh2.GetComponent ("MeshFilter");
		meshFilter1.mesh = mesh;
		meshFilter2.mesh = mesh;
	
	}
}
