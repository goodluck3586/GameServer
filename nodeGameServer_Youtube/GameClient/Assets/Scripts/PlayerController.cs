using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player, Enemy 오브젝트에 적용
 * 
 */

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;     // 총알 객체 연결
    public Transform bulletSpawn;       // 총알 생성 위치
    public bool isLocalPlayer = true;  // TODO switch back when networking

    Vector3 oldPosition;
    Vector3 currentPosition;
    Quaternion oldRotation;
    Quaternion currentRotation;

    // 초기(현재) 위치를 oldPosition과 currentPosition에 저장
    void Start()
    {
        oldPosition = transform.position;
        currentPosition = oldPosition;
        oldRotation = transform.rotation;
        currentRotation = oldRotation;
    }

    // LocalPlayer인 경우만 키보드 조작으로 움직이고, 총알을 발사할 수 있도록 한다.
    void Update()
    {
        // local player인지 network player인지 검사
        // 아니면 return(local player이면 코드 실행)
        if (!isLocalPlayer)
        {
            return;
        }

        // 키보드 화살표키 입력을 받아 저장하고, 캐릭터의 회전과 움직임에 반영
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        #region Reflect new moves.
        // network에 캐릭터의 움직임이 반영되도록하는 코드, 이전 상태가 아니면 최신 상태로 수정
        currentPosition = transform.position;
        currentRotation = transform.rotation;

        if (currentPosition != oldPosition)
        {
            //TODO networking
            oldPosition = currentPosition;
        }

        if (currentRotation != oldRotation)
        {
            //TODO networking
            oldRotation = currentRotation;
        }
        #endregion

        // [spacebar]키가 눌리면 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TODO networking
            //n.CommandShot();
            CmdFire();
        }
    }

    // 총알 발사를 처리하는 함수
    public void CmdFire() // 총알 발사 함수
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;

        Bullet b = bullet.GetComponent<Bullet>();

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 6;
        Destroy(bullet, 2.0f);
    }
}
