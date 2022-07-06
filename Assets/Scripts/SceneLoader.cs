using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : NetworkSceneManagerBase
{
	[SerializeField] private SceneReference[] scenes;
	protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
	{
		Debug.Log($"Switching Scene from {prevScene} to {newScene}");


		List<NetworkObject> sceneObjects = new List<NetworkObject>();

		yield return SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single);
		var loadedScene = SceneManager.GetSceneByPath(scenes[newScene]);
		
		Debug.Log($"Loaded scene");
		sceneObjects = FindNetworkObjects(loadedScene, disable: false);

		// Delay one frame
		yield return null;
		finished(sceneObjects);

		Debug.Log($"Switched Scene from {prevScene} to {newScene} - loaded {sceneObjects.Count} scene objects");

	}
}
