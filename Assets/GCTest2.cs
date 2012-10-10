using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class GCTest2 : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern long mono_gc_get_used_size();

	[DllImport("__Internal")]
	private static extern long mono_gc_get_heap_size();

	private int sizeFactor = 0;

	IEnumerator Start() {
		while (true) {
			yield return new WaitForSeconds(1.0f);
			Debug.Log("Used: " + (mono_gc_get_used_size() / 1024) + " KB, Heap: " + (mono_gc_get_heap_size() / 1024) + " KB");
		}
	}
	
	void Update() {
		int[][] dummy = new int[sizeFactor + 1][];
		for (int i = 0; i < sizeFactor + 1; i++) {
			dummy[i] = new int[1024];
			dummy[i][0] = 0;
		}

		for (int i = 0; i < sizeFactor + 1; i++) {
			dummy[i] = null;
		}
		dummy = null;

		sizeFactor = (sizeFactor + 1) & 0x3ff;

		System.GC.Collect();
	}
}
