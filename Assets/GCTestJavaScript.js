#pragma strict

import System.Runtime.InteropServices;

@DllImportAttribute("__Internal")
static private function mono_gc_get_used_size() : long {}

@DllImportAttribute("__Internal")
static private function mono_gc_get_heap_size() : long {}

private var sizeFactor = 0;

function Start() {
	while (true) {
		yield WaitForSeconds(1.0);
		//System.GC.Collect();
		Debug.Log("Used: " + (mono_gc_get_used_size() / 1024) + " KB, Heap: " + (mono_gc_get_heap_size() / 1024) + " KB");
	}
}

function Update() {
	var dummy = new int[(sizeFactor + 1) * 1024];
	sizeFactor = (sizeFactor + 1) & 0x3ff;
}
