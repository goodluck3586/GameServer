using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;
using System;

namespace Sample
{
    public class Namespace : MonoBehaviour
    {
        private SocketIOComponent socket;

        public class res_login
        {
            public string name;
        }

        public class login
        {
            public string name;
            public string userid;
            public login(string name, string userid)
            {
                this.name = name;
                this.userid = userid;
            }
        }

        public class chat
        {
            public login from;
            public string msg;

            public chat(login from, string msg)
            {
                this.from = from;
                this.msg = msg;
            }
        }

        public class move_friend
        {
            public float x;
            public float y;
            public float z;
            public move_friend(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }

        public string userName;
        public string userId;

        public InputField inputField;
        public Button btnSend;

        private GameObject HeroPrefab;
        private Hero friend;

        Dictionary<string, string> data = new Dictionary<string, string>();
        JSONObject jsonData;

        private void Awake()
        {
            this.HeroPrefab = Resources.Load<GameObject>("HeroPrefab");
            jsonData = new JSONObject();
        }

        private void Start()
        {
            GameObject go = GameObject.Find("SocketIO");
            socket = go.GetComponent<SocketIOComponent>();

            socket.On("connect", OnUserConnect);
        }

        private void OnUserConnect(SocketIOEvent e)
        {
            data.Add("userName", "monkey");
            data.Add("userId", "monkey5255");
            Debug.LogFormat("data: {0}", data);

            Debug.Log(jsonData);
            socket.Emit("login", jsonData);
        }

        //void Start()
        //{
        //    GameObject go = GameObject.Find("SocketIO");
        //    socket = go.GetComponent<SocketIOComponent>();

        //    socket.On("connect", () =>
        //    {
        //        var data = JsonConvert.SerializeObject(new login(this.userName, this.userId));
        //        Debug.LogFormat("data: {0}", data);
        //        socket.EmitJson("login", data);

        //    });

        //    socket.On("login", (string data) =>
        //    {

        //        Debug.Log(data);




        //        //if (aa.name != this.userName)
        //        //{

        //        //}
        //        this.friend = Instantiate(this.HeroPrefab).GetComponent<Hero>();
        //        this.friend.transform.position = Vector3.zero;

        //    });

        //    socket.On("move_friend", (string data) =>
        //    {
        //        Vector3 tPos = JsonConvert.DeserializeObject<Vector3>(data);

        //        if (this.friend != null)
        //            this.friend.Move(tPos);
        //    });

        //    socket.On("chat", (string data) =>
        //    {
        //        Debug.LogFormat("data: {0}", data);
        //    });

        //    socket.On(SystemEvents.disconnect, () =>
        //    {
        //        Debug.LogFormat("disconnect");
        //        GameObject.Destroy(this.friend.gameObject);
        //    });



        //    this.btnSend.onClick.AddListener(() =>
        //    {

        //        Debug.LogFormat("{0}", inputField.text);
        //        var chat = JsonConvert.SerializeObject(new chat(new login(this.userName, this.userId), inputField.text));
        //        socket.EmitJson("chat", chat);
        //    });
        //}

        //public void Send(Vector3 tPos)
        //{
        //    var data = JsonConvert.SerializeObject(new move_friend(tPos.x, tPos.y, tPos.z));
        //    socket.EmitJson("move_friend", data);

        //    //if (this.friend != null)
        //    //    this.friend.Move(tPos);
        //}
    }
}


