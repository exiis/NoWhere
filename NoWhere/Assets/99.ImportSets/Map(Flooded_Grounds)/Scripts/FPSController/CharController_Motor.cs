using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharController_Motor : MonoBehaviour {
	enum characterState{
		GROUND = 0,
		WATER,
		JUMP
	};

	public float speed = 10.0f;
	public float sensitivity = 30.0f;
	public float WaterHeight = 16.8f;  // condition of in water state
	CharacterController character;	
	public GameObject cam;
	float moveFB, moveLR;
	float rotX, rotY;
	public bool webGLRightClickRotation = true;
	float gravity = -9.8f;


	void Start(){
		//LockCursor ();
		character = GetComponent<CharacterController> ();
		if (Application.isEditor) {
			webGLRightClickRotation = false;
			sensitivity = sensitivity * 1.5f;
		}
	}

	//-----------------------------------------------------------------//
	//                        < N O T E >                              //
	// 부력 = 자신이 대체한 유체의 밀도 * 물 속에 있는 부피 * 중력가속도  //
	// 물의 밀도 20° = 대략 1g/cm3									   //
	// 타원 부피 = 4/3 * π * a * b * c								   //
	//																  //
	// 물이 깊어 질 수록 수압에 의하여 물의 밀도가 올라감                //
	// 10m당 1기압                                                     //
	// 해당 판단 로직은 기획 폴더 [ 물속 판정 로직.jpg] 그림 참고        //
	//----------------------------------------------------------------//
	void checkCharacterState(){
		// 부력의 영향을 받지 않는 상태(물 밖에 있는 상태)
		if(transform.position.y > WaterHeight){
			gravity = -9.8f;
		}

		// 일정 수준 물 속에 잠긴 상태
		else {
			float originalGravity = -9.8f;
			float volume = 4.0f / 3.0f * character.radius * character.radius * (character.height/2.0f); //부피
			float surfaceDiff = WaterHeight - transform.position.y;
			float sunken =  surfaceDiff > character.height ? 1.0f : surfaceDiff / character.height; // 물에 잠긴 부피 비율
			float buoyancy = volume * (volume * sunken) * (-1) * originalGravity;
			gravity = buoyancy + originalGravity;
		}
	}


	void Update(){
		moveFB = Input.GetAxis ("Horizontal") * speed;
		moveLR = Input.GetAxis ("Vertical") * speed;

		rotX = Input.GetAxis ("Mouse X") * sensitivity;
		rotY = Input.GetAxis ("Mouse Y") * sensitivity;

		//rotX = Input.GetKey (KeyCode.Joystick1Button4);
		//rotY = Input.GetKey (KeyCode.Joystick1Button5);

		checkCharacterState();


		Vector3 movement = new Vector3 (moveFB, gravity, moveLR);



		if (webGLRightClickRotation) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				CameraRotation (cam, rotX, rotY);
			}
		} else if (!webGLRightClickRotation) {
			CameraRotation (cam, rotX, rotY);
		}

		movement = transform.rotation * movement;
		character.Move (movement * Time.deltaTime);
	}


	void CameraRotation(GameObject cam, float rotX, float rotY){		
		transform.Rotate (0, rotX * Time.deltaTime, 0);
		cam.transform.Rotate (-rotY * Time.deltaTime, 0, 0);
	}




}
