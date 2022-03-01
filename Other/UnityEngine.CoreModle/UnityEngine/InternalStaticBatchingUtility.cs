using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
	internal class InternalStaticBatchingUtility
	{
		public class StaticBatcherGOSorter
		{
			public virtual long GetMaterialId(Renderer renderer)
			{
				if (renderer == null || renderer.sharedMaterial == null)
				{
					return 0L;
				}
				return renderer.sharedMaterial.GetInstanceID();
			}

			public int GetLightmapIndex(Renderer renderer)
			{
				if (renderer == null)
				{
					return -1;
				}
				return renderer.lightmapIndex;
			}

			public static Renderer GetRenderer(GameObject go)
			{
				if (go == null)
				{
					return null;
				}
				MeshFilter meshFilter = go.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (meshFilter == null)
				{
					return null;
				}
				return meshFilter.GetComponent<Renderer>();
			}

			public virtual long GetRendererId(Renderer renderer)
			{
				if (renderer == null)
				{
					return -1L;
				}
				return renderer.GetInstanceID();
			}
		}

		private const int MaxVerticesInBatch = 64000;

		private const string CombinedMeshPrefix = "Combined Mesh";

		public static void CombineRoot(GameObject staticBatchRoot, StaticBatcherGOSorter sorter)
		{
			Combine(staticBatchRoot, combineOnlyStatic: false, isEditorPostprocessScene: false, sorter);
		}

		public static void Combine(GameObject staticBatchRoot, bool combineOnlyStatic, bool isEditorPostprocessScene, StaticBatcherGOSorter sorter)
		{
			GameObject[] array = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			List<GameObject> list = new List<GameObject>();
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if ((!(staticBatchRoot != null) || gameObject.transform.IsChildOf(staticBatchRoot.transform)) && (!combineOnlyStatic || gameObject.isStaticBatchable))
				{
					list.Add(gameObject);
				}
			}
			array = list.ToArray();
			CombineGameObjects(array, staticBatchRoot, isEditorPostprocessScene, sorter);
		}

		public static GameObject[] SortGameObjectsForStaticbatching(GameObject[] gos, StaticBatcherGOSorter sorter)
		{
			gos = gos.OrderBy(delegate(GameObject x)
			{
				Renderer renderer3 = StaticBatcherGOSorter.GetRenderer(x);
				return sorter.GetMaterialId(renderer3);
			}).ThenBy(delegate(GameObject y)
			{
				Renderer renderer2 = StaticBatcherGOSorter.GetRenderer(y);
				return sorter.GetLightmapIndex(renderer2);
			}).ThenBy(delegate(GameObject z)
			{
				Renderer renderer = StaticBatcherGOSorter.GetRenderer(z);
				return sorter.GetRendererId(renderer);
			})
				.ToArray();
			return gos;
		}

		public static void CombineGameObjects(GameObject[] gos, GameObject staticBatchRoot, bool isEditorPostprocessScene, StaticBatcherGOSorter sorter)
		{
			Matrix4x4 matrix4x = Matrix4x4.identity;
			Transform staticBatchRootTransform = null;
			if ((bool)staticBatchRoot)
			{
				matrix4x = staticBatchRoot.transform.worldToLocalMatrix;
				staticBatchRootTransform = staticBatchRoot.transform;
			}
			int batchIndex = 0;
			int num = 0;
			List<MeshSubsetCombineUtility.MeshContainer> list = new List<MeshSubsetCombineUtility.MeshContainer>();
			gos = SortGameObjectsForStaticbatching(gos, sorter ?? new StaticBatcherGOSorter());
			GameObject[] array = gos;
			foreach (GameObject gameObject in array)
			{
				MeshFilter meshFilter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (meshFilter == null)
				{
					continue;
				}
				Mesh sharedMesh = meshFilter.sharedMesh;
				if (sharedMesh == null || (!isEditorPostprocessScene && !sharedMesh.canAccess))
				{
					continue;
				}
				Renderer component = meshFilter.GetComponent<Renderer>();
				if (component == null || !component.enabled || component.staticBatchIndex != 0)
				{
					continue;
				}
				Material[] array2 = component.sharedMaterials;
				if (array2.Any((Material m) => m != null && m.shader != null && m.shader.disableBatching != DisableBatchingType.False))
				{
					continue;
				}
				int vertexCount = sharedMesh.vertexCount;
				if (vertexCount == 0)
				{
					continue;
				}
				MeshRenderer meshRenderer = component as MeshRenderer;
				if (meshRenderer != null && meshRenderer.additionalVertexStreams != null && vertexCount != meshRenderer.additionalVertexStreams.vertexCount)
				{
					continue;
				}
				if (num + vertexCount > 64000)
				{
					MakeBatch(list, staticBatchRootTransform, batchIndex++);
					list.Clear();
					num = 0;
				}
				MeshSubsetCombineUtility.MeshInstance instance = default(MeshSubsetCombineUtility.MeshInstance);
				instance.meshInstanceID = sharedMesh.GetInstanceID();
				instance.rendererInstanceID = component.GetInstanceID();
				if (meshRenderer != null && meshRenderer.additionalVertexStreams != null)
				{
					instance.additionalVertexStreamsMeshInstanceID = meshRenderer.additionalVertexStreams.GetInstanceID();
				}
				instance.transform = matrix4x * meshFilter.transform.localToWorldMatrix;
				instance.lightmapScaleOffset = component.lightmapScaleOffset;
				instance.realtimeLightmapScaleOffset = component.realtimeLightmapScaleOffset;
				MeshSubsetCombineUtility.MeshContainer item = default(MeshSubsetCombineUtility.MeshContainer);
				item.gameObject = gameObject;
				item.instance = instance;
				item.subMeshInstances = new List<MeshSubsetCombineUtility.SubMeshInstance>();
				list.Add(item);
				if (array2.Length > sharedMesh.subMeshCount)
				{
					Debug.LogWarning("Mesh '" + sharedMesh.name + "' has more materials (" + array2.Length + ") than subsets (" + sharedMesh.subMeshCount + ")", component);
					Material[] array3 = new Material[sharedMesh.subMeshCount];
					for (int j = 0; j < sharedMesh.subMeshCount; j++)
					{
						array3[j] = component.sharedMaterials[j];
					}
					component.sharedMaterials = array3;
					array2 = array3;
				}
				for (int k = 0; k < Math.Min(array2.Length, sharedMesh.subMeshCount); k++)
				{
					MeshSubsetCombineUtility.SubMeshInstance item2 = default(MeshSubsetCombineUtility.SubMeshInstance);
					item2.meshInstanceID = meshFilter.sharedMesh.GetInstanceID();
					item2.vertexOffset = num;
					item2.subMeshIndex = k;
					item2.gameObjectInstanceID = gameObject.GetInstanceID();
					item2.transform = instance.transform;
					item.subMeshInstances.Add(item2);
				}
				num += sharedMesh.vertexCount;
			}
			MakeBatch(list, staticBatchRootTransform, batchIndex);
		}

		private static void MakeBatch(List<MeshSubsetCombineUtility.MeshContainer> meshes, Transform staticBatchRootTransform, int batchIndex)
		{
			if (meshes.Count < 2)
			{
				return;
			}
			List<MeshSubsetCombineUtility.MeshInstance> list = new List<MeshSubsetCombineUtility.MeshInstance>();
			List<MeshSubsetCombineUtility.SubMeshInstance> list2 = new List<MeshSubsetCombineUtility.SubMeshInstance>();
			foreach (MeshSubsetCombineUtility.MeshContainer mesh2 in meshes)
			{
				list.Add(mesh2.instance);
				list2.AddRange(mesh2.subMeshInstances);
			}
			string text = "Combined Mesh";
			text = text + " (root: " + ((!(staticBatchRootTransform != null)) ? "scene" : staticBatchRootTransform.name) + ")";
			if (batchIndex > 0)
			{
				text = text + " " + (batchIndex + 1);
			}
			Mesh mesh = StaticBatchingHelper.InternalCombineVertices(list.ToArray(), text);
			StaticBatchingHelper.InternalCombineIndices(list2.ToArray(), mesh);
			int num = 0;
			foreach (MeshSubsetCombineUtility.MeshContainer mesh3 in meshes)
			{
				MeshFilter meshFilter = (MeshFilter)mesh3.gameObject.GetComponent(typeof(MeshFilter));
				meshFilter.sharedMesh = mesh;
				int num2 = mesh3.subMeshInstances.Count();
				Renderer component = mesh3.gameObject.GetComponent<Renderer>();
				component.SetStaticBatchInfo(num, num2);
				component.staticBatchRootTransform = staticBatchRootTransform;
				component.enabled = false;
				component.enabled = true;
				MeshRenderer meshRenderer = component as MeshRenderer;
				if (meshRenderer != null)
				{
					meshRenderer.additionalVertexStreams = null;
				}
				num += num2;
			}
		}
	}
}
