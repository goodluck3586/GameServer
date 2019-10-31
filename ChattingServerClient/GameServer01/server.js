//#region express를 사용하는 경우의 코드
/* express와 socket.io 이용 */
// var app = require('express')();
// var server = require('http').Server(app);
// var io = require('socket.io')(server);
// server.listen(4567, function(){console.log('--server is running--')});
//#endregion

var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4567);  // 포트번호 설정 
console.log('127.0.0.1:4567 서버 실행');

// 클라이언트와의 소켓 통신 코드
io.on('connection', function(socket){
  console.log('클라이언트 접속');
  console.log('New connection from ' + socket.request.connection.remoteAddress);

  // 'beep' 이벤트가 수신되면 실행되는 함수
	socket.on('beep', function(data){
    console.log('beep 이벤트 발생 : ', data);
		socket.emit('boop', {serverMsg:"Hello Client"});  // 연결된 클라이언트에 전송
		io.emit('broadMSG', {broadcastMsg:"Hello everyone"});  // 모든 클라이언트에 전송
		//socket.broadcast.emit('broadMSG', "broadcast message");
  });

  socket.on('join', function(data){
    socket.userName = data.userName;
    console.log("새로운 사용자 접속: "+data.userName);
    io.emit('join', {newMsg: "join", userName: data.userName});
  });

  // 새로운 채팅 메시지가 수신되면 모두에게 전송
  socket.on('newMsg', function(data){
    console.log('newMsg 이벤트 발생 : ', data);
    io.emit('broadMSG', {newMsg: data.newMsg, userName: data.userName});
  })
  
  // 클라이언트 접속 해제 이벤트
  socket.on('disconnect', function(reason){
    console.log(`${socket.userName} 접속 해제`);
    io.emit('exitUser', {newMsg: 'exit', userName: socket.userName});
    console.log(reason);
  });
});
