/*
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game : MonoBehaviour {
//	public static LevelPack PACK;
	public static int LEVEL;
	public static string LEVELNAME;
	public static Vector3 STARTPOS;
	public static int CRISTALLS;
	[SerializeField] PlayerMoveControll hopmon;
	[SerializeField] GameCamera gCamera;

	void Start () {
		try{
			LoadLevel.Load(PACK, PACK._levelName[LEVEL]);
		}
		catch(System.NullReferenceException){
			LevelPack pk = new LevelPack("Custom");
			LoadLevel.Load(pk, pk._levelName[0]);			
		}
		hopmon.enabled = true;
		gCamera.SendMessage("Start");
		CRISTALLS = GameObject.FindGameObjectsWithTag ("Cristall").Length;
		print(STARTPOS);
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)){
			if (LEVEL < PACK._levelName.Count - 1){
				LEVEL++;
			}
			else{
				LEVEL = 0;
			}
			print(PACK._levelName[LEVEL]);
			if (transform.childCount > 0){
				foreach (Transform child in transform){
					Destroy(child.gameObject);
				}
			}
			hopmon.enabled = false;
			SendMessage("Start");
		}
		if (Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene(0);
		}
	}
}
*/
