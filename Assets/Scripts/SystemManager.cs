using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SystemManager : MonoBehaviour {

	public static void LoadGivenScene(string levelName = "SampleScene") {
		SceneManager.LoadScene(levelName);
	}

	public static void lockMouse() {
		Cursor.lockState = CursorLockMode.Locked;
	}

	public static void unlockMouse() {
		Cursor.lockState = CursorLockMode.Confined;
	}

}
