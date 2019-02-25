using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandMaker : MonoBehaviour {
	public GameObject landContainer,panelMain,panelScore,panelHighscore;
	public Text txtTimer,txtFinaltime,txtHighscore;
	[SerializeField]
	int miles,width;
	bool startLevel=false;
	int level=0;
	float timervalue=0,fastest;

	// Use this for initialization
	void Start () {
		startLevel = true;
		panelMain.SetActive (true);
		int minutes2 = (int)PlayerPrefs.GetFloat ("Fastest") / 60;
		float second2 = PlayerPrefs.GetFloat ("Fastest") % 60;
		float fraction2 = (PlayerPrefs.GetFloat ("Fastest") * 1000) % 1000;
		txtHighscore.text = string.Format ("{0:00}:{1:00}:{2:000}", minutes2, second2, fraction2);
	}


	// Update is called once per frame
	void Update () {

		//Finish
		Transform pos = FindObjectOfType<Bolagerak> ().gameObject.transform;
		if (pos.position.z >= miles - 5&& FindObjectOfType<enemy>()==null) {
			Debug.Log ("Finish");
			level += 1;
			resetPlayer ();
			startLevel = true;
		}

		//Pengaturan Level
		if (startLevel) {
			if (level == 0) {
				level0();
				startLevel = false;
			}
			if (level == 1) {
				level1 ();
				startLevel = false;
			} else if (level == 2) {
				level2 ();
				startLevel = false;
			} else if (level == 3) {
				level3 ();
				startLevel = false;
			} else if(level>=4){
				CreateLand ();
				Debug.Log ("Mission Complete");
				startLevel = false;
				panelHighscore.SetActive (true);
			}
		}

		//Pengaturan Waktu
		if (level >= 1&&level<=3) {
			panelScore.SetActive (true);
			timervalue += Time.deltaTime;
			int minutes=(int)timervalue/60;
			float second = timervalue % 60;
			float fraction = (timervalue * 1000) % 1000;
			txtTimer.text = string.Format ("{0:00}:{1:00}:{2:000}", minutes, second, fraction);
		} else if(level>=4) {
			int minutes=(int)timervalue/60;
			float second = timervalue % 60;
			float fraction = (timervalue * 1000) % 1000;
			txtTimer.text = string.Format ("{0:00}:{1:00}:{2:000}", minutes, second, fraction);
			txtFinaltime.text = txtTimer.text;
			if (timervalue < PlayerPrefs.GetFloat ("Fastest")) {
				PlayerPrefs.SetFloat ("Fastest", timervalue);
				Debug.Log ("kondisi1");
				Debug.Log (PlayerPrefs.GetFloat ("Fastest"));
				int minutes2 = (int)PlayerPrefs.GetFloat ("Fastest") / 60;
				float second2 = PlayerPrefs.GetFloat ("Fastest") % 60;
				float fraction2 = (PlayerPrefs.GetFloat ("Fastest") * 1000) % 1000;
				txtHighscore.text = string.Format ("{0:00}:{1:00}:{2:000}", minutes2, second2, fraction2);
			} 
		}


	}

	//Pembuatan Gameplay
	void CreateLand(){
		for (int i = -2; i <= miles; i++) {
			for (int j = (width / 2) * -1; j <= (width / 2); j++) {
				GameObject lands = (GameObject)Instantiate (Resources.Load ("land"));
				lands.transform.position = new Vector3(j,-2,i);
				//Batas Samping
				if (i == -2 || i == miles || j == (width / 2) * -1 || j == (width / 2)) {
					lands.transform.localScale = new Vector3 (1f, 24f, 1f);
					lands.GetComponent<MeshRenderer> ().enabled = false;
				}
				//Pembuatan Finish
				if (i >= miles - 5) {
					lands.GetComponent<Renderer> ().material.color = Color.red;
				}
				lands.transform.SetParent (landContainer.transform);
			}
		}
		//Batas Atas
		for (int i = -2; i <= miles; i++) {
			for (int j = (width / 2) * -1; j <= (width / 2); j++) {
				GameObject lands = (GameObject)Instantiate (Resources.Load ("land"));
				lands.transform.position = new Vector3(j,10,i);
				lands.GetComponent<MeshRenderer> ().enabled = false;
				lands.transform.SetParent (landContainer.transform);
				
			}
		}


	}
	void CreateEnemy(float x,float y,float z){
		if (z > -2 && z < miles && x > (width / 2) * -1 &&x < (width / 2)){
			GameObject enemybox = (GameObject)Instantiate (Resources.Load ("enemybox"));
			enemybox.transform.position = new Vector3(x,y,z);
			enemybox.transform.SetParent (landContainer.transform);
		} 
	}

	void CreateCrate(float x,float y,float z){
		if (z > -2 && z < miles && x > (width / 2) * -1 &&x <(width / 2)){
		GameObject crates = (GameObject)Instantiate (Resources.Load ("Crate"));
		crates.transform.position = new Vector3(x,y,z);
		crates.transform.SetParent (landContainer.transform);
		} 
	}

	void CreatePohon(float x,float y,float z){
		if (z > -2 && z < miles && x > (width / 2) * -1 &&x <(width / 2)){
			GameObject pohon = (GameObject)Instantiate (Resources.Load ("pohon"));
			pohon.transform.position = new Vector3(x,y,z);
			pohon.transform.SetParent (landContainer.transform);
		} 
	}

	void CreateHutan(int posx,int posz,int width,int deep){
		for (int i = 0; i < width; i++) {
			for(int j=0;j<deep;j++){
					CreatePohon(posx+i,-1,posz+j);
			}
		}
	}

	void CreateWall(int posx,int posz,int height,int width,int deep){
		for (int i = 0; i < width; i++) {
			for(int j=0;j<deep;j++){
				for(int k=-1;k<height;k++){
					CreateCrate(posx+i,k,posz+j);
				}
			}
		}
	}

	void resetPlayer(){
		Transform pos = FindObjectOfType<Bolagerak> ().gameObject.transform;
		pos.position = new Vector3 (0, 0, 0);
		foreach (Transform child in landContainer.transform) {
			Destroy (child.gameObject);
		}
	}
	void level0(){
		CreateLand ();
		CreateHutan (-5, 10, 4, 15);
		CreateHutan (2, 10, 4, 15);
		CreateWall (-1, 8, 0, 3, 1);
		CreateWall (-1, 9, 1, 3, 1);
		CreateWall (-5, 9, 0, 12, 1);
		CreateWall (-1, 10, 2, 3, 15);
		CreateWall (-1, 25, 1, 3, 1);
		CreateWall (-5, 25, 0, 12, 1);
		CreateWall (-1, 26, 0, 3, 1);
	}

	void level1(){
		CreateLand ();
		CreateHutan (-5, 7, 9, 1);
		CreateHutan (-3, 12, 9, 1);
		CreateEnemy (0,2,45);

		CreateWall (-5, 20, 0, 6, 1);
		CreateWall (-5, 21, 1, 6, 1);
		CreateWall (-5, 22, 2, 6, 1);
		CreateWall (-5, 23, 2, 6, 5);
		CreateWall (-5, 28, 1, 6, 1);
		CreateWall (-5, 29, 0, 6, 1);
		CreateWall (-5, 40, 0, 12, 1);
		CreateWall (-5, 41, 1, 12, 1);
		CreateWall (-5, 42, 2, 12, 1);
		CreateWall (-5, 43, 2, 12, 5);
		CreateWall (-5, 48, 1, 12, 1);
		CreateWall (-5, 49, 0, 12, 1);
		CreateWall (-5, 60, 0, 12, 1);
		CreateWall (-5, 70, 0, 12, 1);
		CreateWall (-5, 80, 0, 12, 1);
	}
	void level2(){
		CreateLand ();
		CreateEnemy (-5,2,25);
		CreateEnemy (5,2,25);
		CreateEnemy (-2,1,80);
		CreateCrate (0, -1, 2);
		CreateCrate (3, -1, 8);
		CreateCrate (4, -1, 8);
		CreateCrate (-3, -1, 8);
		CreateCrate (-4, -1, 8);
		CreateCrate (3, 0, 8);
		CreateCrate (-3, 0, 8);
		CreateWall (-5, 20, 0, 2, 1);
		CreateWall (-5, 21, 1, 2, 1);
		CreateWall (-5, 22, 2, 2, 1);
		CreateWall (-5, 23, 2, 2, 5);
		CreateWall (-5, 28, 1, 2, 1);
		CreateWall (4, 29, 0, 2, 1);
		CreateWall (4, 20, 0, 2, 1);
		CreateWall (4, 21, 1, 2, 1);
		CreateWall (4, 22, 2, 2, 1);
		CreateWall (4, 23, 2, 2, 5);
		CreateWall (4, 28, 1, 2, 1);
		CreateWall (4, 29, 0, 2, 1);
		CreateWall (-5, 22, 0, 10, 1);
		CreateWall (-5, 59, 0, 12, 1);
		CreateWall (5, 60, 8, 1, 1);
		CreateWall (0, 60, 1, 5, 1);
		CreateWall (-5, 60, 8, 6, 1);
		CreateWall (-5, 79, 0, 12, 1);
		CreateWall (-5, 80, 8, 1, 1);
		CreateWall (-4, 80, 1, 4, 1);
		CreateWall (0, 80, 8, 6, 1);
		CreateHutan (-5, 40, 9, 1);
		CreateHutan (-3, 45, 9, 1);
		CreateHutan (-5, 50, 9, 1);
	}
	void level3(){
		CreateLand ();
		CreateWall (-5, 10, 0, 12, 1);
		CreateWall (-5, 28, 3, 5, 20);
		CreateWall (-5, 50, 0, 12, 1);
		CreateWall (0, 20, 0, 6, 1);
		CreateWall (0, 21, 1, 6, 1);
		CreateWall (0, 22, 2, 6, 1);
		CreateWall (0, 23, 2, 6, 15);
		CreateHutan (-5, 20, 5, 8);
		CreateWall (0, 38, 1, 6, 10);
		CreateWall (-5, 48, 3, 12, 3);
		CreateWall (-5, 51, 2, 12, 2);
		CreateWall (-5, 53, 1, 12, 2);
		CreateWall (-5, 55, 0, 12, 2);
		CreateWall (-5, 57, 0, 9, 3);
		CreateWall (-5, 60, 1, 9, 2);
		CreateWall (-5, 62, 2, 9, 2);
		CreateWall (-5, 64, 3,9, 2);
		CreateCrate (5, 2, 64);
		CreateCrate (6, 2, 64);
		CreateCrate (4, 2, 64);
		CreateHutan (-5, 80, 4, 15);
		CreateHutan (2, 80, 4, 15);
		CreateWall (-1, 78, 0, 3, 1);
		CreateWall (-1, 79, 1, 3, 1);
		CreateWall (-5, 79, 0, 12, 1);
		CreateWall (-1, 80, 2, 3, 15);
		CreateWall (-1, 95, 1, 3, 1);
		CreateWall (-5, 95, 0, 12, 1);
		CreateWall (-1, 96, 0, 3, 1);
		CreateEnemy (-3, 3, 28);
		CreateEnemy (3, 3, 48);
		CreateEnemy (5, 3,64);
		CreateEnemy (-5, 3, 64);
		CreateEnemy (0, 3, 64);
		CreateEnemy (-4, 1, 83);
		CreateEnemy (4, 1, 83);
		CreateEnemy (-4, 1, 93);
		CreateEnemy (4, 1, 93);
		CreateEnemy (0, 2, 90);
	}

	public void playGame(){
		panelMain.SetActive (false);
		level = 0;
		resetPlayer ();
		startLevel = true;
	}
	public void replayGame(){
		panelMain.SetActive (true);
		panelScore.SetActive (false);
		panelHighscore.SetActive (false);
		level = 0;
		resetPlayer ();
		startLevel = true;
		timervalue = 0;
	}
}
