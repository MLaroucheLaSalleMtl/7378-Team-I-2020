using UnityEngine;
using System.Collections;
using UnityEditor;
/*
	Main decal manager script. Created and freely shared by Tudor Nita (tudor.cgrats.com).
	Feel free to modify/ use this in any way you see fit. 
	Just remember to share your improvements.
	
	Decals are grouped and combined by material name automatically;
	Create Decal - instantiates a new decal;
	Reset All Decals - umm, resets all the decals;
	Delete All Decals - deletes all the decals and all the saved meshes (NewEditorMeshes folder)
	Triangle Strip - sets the mesh combine utility to generate triangle strips.
 */
[ExecuteInEditMode]
public class DecalManager : MonoBehaviour {

public GameObject decalPrefab;
public Material material;
Transform decalParent;
public bool triangleStrips = false;
public int meshIndex;
public bool combineAtRuntime = true;
	
	void Start () 
	{
		if(EditorPrefs.HasKey("meshIndex"))
		{
			meshIndex = EditorPrefs.GetInt("meshIndex",0);
		}
		else
		{
			EditorPrefs.SetInt("meshIndex",0);
			meshIndex = 0;
		}	
		decalParent = transform;
	}
	
	GameObject NewDecal()
    {
		GameObject newDecal = Instantiate(decalPrefab,decalParent.position,Quaternion.identity) as GameObject;
		newDecal.name = "simpleMeshDecal";
		if(!newDecal.GetComponent<Renderer>().sharedMaterial)
		{
			Debug.LogError("The decal must have a material. Set one on the prefab or through the decal manager");
			Destroy(newDecal);
		}
		
		if(material)
			newDecal.GetComponent<Renderer>().sharedMaterial = material;
		
		meshIndex++;
		SaveIndex();
		newDecal.GetComponent<SimpleMeshDecal>().meshName =  newDecal.name + meshIndex.ToString();
		Transform parentTransform  = transform.Find(newDecal.GetComponent<Renderer>().sharedMaterial.name) as Transform;
		if(parentTransform)
			newDecal.transform.parent = parentTransform;
		else
		{
			GameObject newParent = new GameObject(newDecal.GetComponent<Renderer>().sharedMaterial.name) as GameObject;
			newParent.name = newDecal.GetComponent<Renderer>().sharedMaterial.name;
			newParent.transform.parent = decalParent;
			AddCombineScript(newParent);
			newDecal.transform.parent = newParent.transform;
		}
		return newDecal;
    }
	
	void ResetAllDecals()
	{
		SimpleMeshDecal[] children =  GetComponentsInChildren<SimpleMeshDecal>();
		foreach(SimpleMeshDecal decalScript in children){
			decalScript.ResetDecal();
		}
	}
	
	void DeleteAllDecals()
	{
		SimpleMeshDecal[] decalChildren = GetComponentsInChildren<SimpleMeshDecal>();
		foreach (SimpleMeshDecal decalChild in decalChildren)
		{
			DestroyImmediate(decalChild.gameObject);
		}
		CombineDecals[] combineChildren = GetComponentsInChildren<CombineDecals>();
		foreach (CombineDecals combineChild in combineChildren)
		{
			DestroyImmediate(combineChild.gameObject);
		}
		meshIndex = 0;
		SaveIndex();
	}
	
	void AddCombineScript(GameObject GO)
	{
		CombineDecals decalScript = GO.AddComponent<CombineDecals>() as CombineDecals;
		decalScript.generateTriangleStrips = triangleStrips;
	}
	
	void OnDrawGizmos() 
	{
		Gizmos.DrawIcon(decalParent.position, "smdGizmo.tif");
	}
	
	void SaveIndex(){
		EditorPrefs.SetInt("meshIndex",meshIndex);
	}
}
