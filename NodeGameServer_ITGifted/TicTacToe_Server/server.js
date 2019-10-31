var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567);
console.log("127.0.0.1:4567에서 서버 실행중...");

var ids = [];  // 플레이어들의 socket.id 저장
var userNames = [];  // 사용자 닉네임 저장
var playerMarks = ['X', 'O'];

io.on('connection', function(socket){
  console.log("클라이언트 접속");
  ids.push(socket.id);  
  console.log('ids : ', ids, 'ids.length', ids.length);

	// 새로운 사용자 조인
	socket.on("join", function(data){  // data = {userName: "닉네임"}
    console.log("새로운 사용자 조인 : ", data.userName);
    userNames.push(data.userName);
    socket.userName = data.userName; 
    console.log('userNames : ', userNames);

    if(userNames.length == 1) // 접속자가 1명인 경우 기다리라는 메세지 전송.
      socket.emit("waitForAnotherUser");  
    else if(userNames.length == 2)  // 접속자가 2명이면 게임 시작 메세지 전송.
      io.emit("playGame");
  });
  
  /* 요청한 클라이언트의 마크가 X, O 인지 리턴 */
  socket.on('requestMark', function(data){
    console.log('requestMark 이벤트 수신: ');
    socket.emit('requestMark', {mark: playerMarks[userNames.indexOf(socket.userName)]});
  });

  /* 클라이언트 접속 해제 이벤트 처리 */
  socket.on('disconnect', function(reason){
    console.log(`${socket.id} 접속 해제`);
    ids.splice(ids.indexOf(socket.id), 1);  // 접속 해제된 사용자의 socket.id를 배열에서 제거
    userNames.splice(userNames.indexOf(socket.userName), 1);  // 접속해제된 사용자의 userName 제거
    console.log(ids);
    console.log(userNames);

  });

})