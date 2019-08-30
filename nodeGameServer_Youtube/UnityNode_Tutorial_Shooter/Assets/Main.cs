using Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Hero hero;
    //public Namespace gameLauncher;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        { 
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // mainCamera에서 ray 발사, Camera.main(메인 카메라에 접근), Screen의 위치에서 Ray를 발사
            RaycastHit hit;  //어떤 위치에서 광선을 쏴서 어떤 충돌체와 붙이면 RaycastHit에 그 정보를 전달시킬 수 있다.
            if (Physics.Raycast(ray, out hit, 1000f))  // Physics.Raycast(ray, out hit))란 ray를 발사하여 일직선으로 나아가다가 충돌체를 만나면 그 정보를 hit에 입력한다.
            {
                print(hit.collider.tag);
                if (hit.collider.tag == "ground")
                {
                    hero.Move(hit.point);
                    //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;  // 맞힘 오브젝트의 색상 변경
                    //gameLauncher.Send(hit.point);
                }
            }
        }

    }
}
