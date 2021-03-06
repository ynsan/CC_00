﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCharaCreate : MonoBehaviour {

	//public bool ShowGUI = true;

	public string Copyright;


	public Transform Head;


	// 髪色
	public Image colorInputHair_panel;
	public InputField[] colorInput_Hair;
	public Color32 HAIRcolor = new Color32(50, 12, 12, 255);
	[HideInInspector]
	public float[] HAIRcolors_val = new float[3];
	// 目色
	public Image colorInputEye_panel;
	public InputField[] colorInput_Eye;
	public Color32 EYEcolor = new Color32(80, 25, 45, 255);
	[HideInInspector]
	public float[] EYEcolors_val = new float[3];

	// 髪のオプション
	public bool bang_Reverse = false;
	public bool backHair_Reverse = false;
	public Transform[] Parents_HairTransform = new Transform[2];
	/*
		[0] 前髪
		[1] 後ろ
	*/
	public GameObject[] BangParts;
	public int selectIs_BangParts = -1;
	public GameObject[] BackHairParts;
	public int selectIs_BackHairParts = -1;
	public MeshRenderer mr_bang;
	public MeshRenderer mr_backBair;

	// 表示非表示_顔の装飾
	public GameObject Ear_parts;
	private bool Ear_show = true;
	public GameObject FaceFur_parts;
	private bool FaceFur_show = false;
	public GameObject NoseFur_parts;
	private bool NoseFur_show = false;

	public Button[] OnOffButtons = new Button[2];

	public SkinnedMeshRenderer mr_eyeBlash;
	public Texture2D[] Materials_EyeBlash;
	private int EyeBlashMaterial_value;

	/// <summary>
	/// 目のMesh
	/// </summary>
	public MeshRenderer mr_EyeL, mr_EyeR;

	// アニメーション
	public Animator chr_animator;

	void OnGUI() {
		GUI.Label(new Rect(720, 480, 480, 480), "by" + Copyright); //テキスト表示
	}

	void Start() {

		StartSetColor();

		selectIs_BangParts = -1;
		Button_BangHair();
		Button_BackHair();

		EyeBlashMaterial_value = 0;
		mr_eyeBlash.material.mainTexture = Materials_EyeBlash[EyeBlashMaterial_value];

		Input_HairColor();
		Input_EyeColor();

		Ear_parts.SetActive(Ear_show);
		FaceFur_parts.SetActive(FaceFur_show);
		NoseFur_parts.SetActive(NoseFur_show);



		for (int i = 0; i < SkinButtons.Length; i++) {
			int nodes = i;
			SkinButtons[i].onClick.AddListener(() => SetTextureFace(nodes));
		}

	}

	private void StartSetColor() {
		colorInput_Hair[0].text = "" + HAIRcolor.r;
		colorInput_Hair[1].text = "" + HAIRcolor.g;
		colorInput_Hair[2].text = "" + HAIRcolor.b;

		colorInput_Eye[0].text = "" + EYEcolor.r;
		colorInput_Eye[1].text = "" + EYEcolor.g;
		colorInput_Eye[2].text = "" + EYEcolor.b;
	}

	void FixedUpdate() {

	}

	public void Button_BangHair() {
		foreach (Transform child in Parents_HairTransform[0]) {
			Destroy(child.gameObject);
		} 
		if (selectIs_BangParts < BangParts.Length - 1) {
			selectIs_BangParts++;
		} else {
			Debug.Log("前髪_一周_" + selectIs_BangParts);
			selectIs_BangParts = 0;
		}

		Debug.Log("前髪_" + selectIs_BangParts);
		GameObject neww = Instantiate(BangParts[selectIs_BangParts]);
		neww.name = BangParts[selectIs_BangParts].name;
		neww.transform.parent = Parents_HairTransform[0];
		neww.transform.position = Parents_HairTransform[0].position;
		neww.transform.rotation = Parents_HairTransform[0].rotation;
		foreach (Transform child in neww.transform) {
			mr_bang = child.gameObject.GetComponent<MeshRenderer>();
			Debug.Log(mr_bang.gameObject);
		} 
		Input_HairColor();
	}
	public void Button_BackHair() {
		foreach (Transform child in Parents_HairTransform[1]) {
			Destroy(child.gameObject);
		} 
		if (selectIs_BackHairParts < BackHairParts.Length - 1) {
			selectIs_BackHairParts++;
		} else {
			Debug.Log("後ろ髪_一周_" + selectIs_BackHairParts);
			selectIs_BackHairParts = 0;
		}

		Debug.Log("後ろ髪_" + selectIs_BackHairParts);
		GameObject neaw = Instantiate(BackHairParts[selectIs_BackHairParts]);
		neaw.name = BackHairParts[selectIs_BackHairParts].name;
		neaw.transform.parent = Parents_HairTransform[1];
		neaw.transform.position = Parents_HairTransform[1].position;
		neaw.transform.rotation = Parents_HairTransform[1].rotation;
		foreach (Transform child in neaw.transform) {
			mr_backBair = child.gameObject.GetComponent<MeshRenderer>();
			Debug.Log(mr_backBair.gameObject);
		} 
		Input_HairColor();
	}

	/// <summary>
	/// 色変更の値操作
	/// </summary>
	public void Input_HairColor() {
		// 文字列変換
		for (int i = 0; i < colorInput_Hair.Length; i++) {
			HAIRcolors_val[i] = int.Parse(colorInput_Hair[i].text);
			if (HAIRcolors_val[i] >= 255)
				HAIRcolors_val[i] = 255;
			colorInput_Eye[i].text = (HAIRcolors_val[i]).ToString();
		}
		HAIRcolor = new Color(HAIRcolors_val[0] * 0.01f, HAIRcolors_val[1] * 0.01f, HAIRcolors_val[2] * 0.01f);
		colorInputHair_panel.color = EYEcolor;
		Update_HairColor();
	}
	public void Input_EyeColor() {
		// 文字列変換
		for (int i = 0; i < colorInput_Eye.Length; i++) {
			EYEcolors_val[i] = int.Parse(colorInput_Eye[i].text);
			if (EYEcolors_val[i] >= 255)
				EYEcolors_val[i] = 255;
			colorInput_Eye[i].text = (EYEcolors_val[i]).ToString();
		}
		EYEcolor = new Color(EYEcolors_val[0] * 0.01f, EYEcolors_val[1] * 0.01f, EYEcolors_val[2] * 0.01f);
		colorInputEye_panel.color = EYEcolor;
		UpdateColor();
	}

	/// <summary>
	/// 色変更処理
	/// </summary>
	public void UpdateColor() {
		mr_EyeL.material.SetColor("_PupilTex", EYEcolor);
		mr_EyeR.material.SetColor("_PupilTex", EYEcolor);
	}
	public void Update_HairColor() {
		mr_bang.material.color = HAIRcolor;
		mr_backBair.material.color = HAIRcolor;
		foreach (Transform child in mr_backBair.gameObject.transform) {
			if (child) {
				child.GetComponent<MeshRenderer>().material.color = HAIRcolor;
			} 
		}
	}
	/// <summary>
	/// まつ毛の切り替え
	/// </summary>
	public void Button_EyeBlash() {
		if (EyeBlashMaterial_value < Materials_EyeBlash.Length - 1) {
			EyeBlashMaterial_value++;
		} else {
			EyeBlashMaterial_value = 0;
		}
		Debug.Log(EyeBlashMaterial_value);
		mr_eyeBlash.material.mainTexture = Materials_EyeBlash[EyeBlashMaterial_value];
	}

	public void Button_BangReverse() {
		bang_Reverse = !bang_Reverse;
		mr_bang.gameObject.transform.localScale = new Vector3(-mr_bang.gameObject.transform.localScale.x, 1, 1);
		foreach (Transform child in mr_bang.gameObject.transform) {
			if (child) {
				child.gameObject.transform.localScale = new Vector3(-child.gameObject.transform.localScale.x, 1, 1);
			} 
		}
	}
	public void Button_BackHairReverse() {
		backHair_Reverse = !backHair_Reverse;
		mr_backBair.gameObject.transform.localScale = new Vector3(-mr_backBair.gameObject.transform.localScale.x, 1, 1);
		foreach (Transform child in mr_backBair.gameObject.transform) {
			if (child) {
				child.gameObject.transform.localScale = new Vector3(-child.gameObject.transform.localScale.x, 1, 1);
			} 
		}
	}

	public void EarShow() {
		Ear_show = !Ear_show;
		Ear_parts.SetActive(Ear_show);
		string a;
		if (Ear_show == true)
			a = "On";
		else
			a = "Off";
		foreach (Transform child in OnOffButtons[0].transform) {
			child.GetComponent<Text>().text = a;
		}
	}
	public void faceFurShow() {
		FaceFur_show = !FaceFur_show;
		FaceFur_parts.SetActive(FaceFur_show);
		string a;
		if (FaceFur_show == true)
			a = "On";
		else
			a = "Off";
		foreach (Transform child in OnOffButtons[1].transform) {
			child.GetComponent<Text>().text = a;
		}
	}
	public void NoseFurShow() {
		NoseFur_show = !NoseFur_show;
		NoseFur_parts.SetActive(NoseFur_show);
		string a;
		if (NoseFur_show == true)
			a = "On";
		else
			a = "Off";
		foreach (Transform child in OnOffButtons[2].transform) {
			child.GetComponent<Text>().text = a;
		}
	}

	/// <summary>
	/// キャラクター本体のアニメーションOnOff
	/// </summary>
	public void chrAnimation_PlayAndStop() {
		chr_animator.enabled = !chr_animator.enabled;
	}

	public Button[] SkinButtons;
	public SkinnedMeshRenderer faceTopMesh, faceBottomMesh;
	public MeshRenderer headBackMesh, neckCapMesh;
	public MeshRenderer earMesh, noseFurMesh, faceFurLMesh, faceFurRMesh;
	public Texture2D[] faceTextures;

	public void SetTextureFace(int aa) {
		faceTopMesh.material.mainTexture = faceTextures[aa];
		faceBottomMesh.material.mainTexture = faceTextures[aa];
		headBackMesh.material.mainTexture = faceTextures[aa];
		neckCapMesh.material.mainTexture = faceTextures[aa];
		earMesh.material.mainTexture = faceTextures[aa];
		noseFurMesh.material.mainTexture = faceTextures[aa];
		faceFurLMesh.material.mainTexture = faceTextures[aa];
		faceFurRMesh.material.mainTexture = faceTextures[aa];
		//faceMesh.UpdateGIMaterials();
	}
}
