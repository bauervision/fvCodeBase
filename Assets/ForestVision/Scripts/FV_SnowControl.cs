using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FV_SnowControl : MonoBehaviour {
	public Shader FVShader = Shader.Find ("ForestVision/FV_ImageEFX");
	public Vector4 SnowDirection = new Vector4(0,0,0);
	public float SnowLevel = -0.1f;
	public float SnowDepth = 1;
	

	void Start(){

		if (!FVShader && !FVShader.isSupported) {
			enabled = false;
		}
	}

	
	void FindShader(Shader shaderName) {
		int count = 0;
		List<Material> armat = new List<Material>();
		
		Renderer[] arrend = (Renderer[])Resources.FindObjectsOfTypeAll(typeof(Renderer));
		foreach (Renderer rend in arrend) {
			foreach (Material mat in rend.sharedMaterials) {
				if (!armat.Contains (mat)) {
					armat.Add (mat);
				}
			}
		}
		
		foreach (Material mat in armat) {
			if (mat != null && mat.shader != null && mat.shader.name != null) {
				count++;
				mat.SetFloat("_SnowLevel",SnowLevel);
				mat.SetFloat("_SnowDepth",SnowDepth);
				mat.SetVector("_SnowDirection",SnowDirection);
			}
		}
		
		//Debug.Log ("\n" + count + " materials using shader " + shaderName + " found.");
	}

	void LateUpdate(){
		SnowLevel = Mathf.Clamp (SnowLevel, -0.1f, 1f);
		SnowDepth = Mathf.Clamp (SnowDepth, 0.0f, 1f);

		if (FVShader != null) {
			//link up all of these values to the values in the shader
			FindShader (FVShader);
		}
	}
}