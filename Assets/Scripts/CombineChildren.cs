using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Mesh/Combine Children")]
public class CombineChildren : MonoBehaviour {
	
	void Start()
	{
		System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
		stopWatch.Start();

		Matrix4x4 myTransform = transform.worldToLocalMatrix;
		Dictionary<Material, List<CombineInstance>> combines = new Dictionary<Material, List<CombineInstance>>();
		MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
		foreach (var meshRenderer in meshRenderers)
		{
			foreach (var material in meshRenderer.sharedMaterials)
				if (material != null && !combines.ContainsKey(material))
					combines.Add(material, new List<CombineInstance>());
		}
		
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		foreach(var filter in meshFilters)
		{
			if (filter.sharedMesh == null)
				continue;
			CombineInstance ci = new CombineInstance();
			ci.mesh = filter.sharedMesh;
			ci.transform = myTransform * filter.transform.localToWorldMatrix;
			//combines[filter.renderer.sharedMaterial].Add(ci);
			//filter.renderer.enabled = false;
		}
		
		foreach(Material m in combines.Keys)
		{
			var go = new GameObject("Combined mesh");
			go.transform.parent = transform;
			go.transform.localPosition = Vector3.zero;
			go.transform.localRotation = Quaternion.identity;
			go.transform.localScale = Vector3.one;
			
			var filter = go.AddComponent<MeshFilter>();
			filter.mesh.CombineMeshes(combines[m].ToArray(), true, true);
			
			var renderer = go.AddComponent<MeshRenderer>();
			renderer.material = m;
		}

		stopWatch.Stop();
		System.TimeSpan ts = stopWatch.Elapsed;

		// Format and display the TimeSpan value.
		int elapsedTime =ts.Minutes*60*1000+ts.Seconds*1000+ts.Milliseconds;

		UnityEngine.Debug.Log("CombineChildren setup time: " + elapsedTime.ToString() +" ms");
	}
}