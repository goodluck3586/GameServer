var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567);
console.log('127.0.0.1:4567 서버 실행');

// 소켓 통신 이벤트 처리
io.on('connection', function(socket){
  console.log("클라이언트 접속");
  console.log("New connection from " + socket.request.connection.remoteAddress);

  // 새로운 사용자 조인 이벤트 처리
  socket.on('join', function(data){
    console.log('새로운 사용자 입장: ', data);
    socket.userName = data.userName;  // 사용자 이름을 소켓 객체에 저장
    io.emit('broadcastJoin', {msg: socket.userName});  // 모든 클라이언트에게 텍스트 전송
  });

  // 새로운 채팅 텍스트가 넘어오면, 모든 클라이언트들에게 전송
  socket.on('newMsg', function(data){
    console.log('newMsg 수신: ', data);
    io.emit('broadcastMsg', {msg: data.msg, userName: socket.userName});  // 모든 클라이언트에게 텍스트 전송
  });

	socket.on('beep', function(data){  // 클라이언트의 이벤트 수신
    console.log('beep 이벤트 발생');
    console.log('수신 데이터', data);
    console.log(data.msg);
		socket.emit('boop', {msg: "Hello client!!!"});  // 클라이언트로 이벤트 송신
  });
  
  socket.on('disconnect', function(reason){
    console.log(`${socket.userName}님이 퇴장하셨습니다.`);
    io.emit('exitUser', {userName: socket.userName});
  });
})