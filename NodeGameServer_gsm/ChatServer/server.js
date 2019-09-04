var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567);
console.log('127.0.0.1:4567 서버 실행');

// 소켓 통신 이벤트 처리
io.on('connection', function(socket){
  console.log("클라이언트 접속");
  console.log("New connection from " + socket.request.connection.remoteAddress);

	socket.on('beep', function(data){  // 클라이언트의 이벤트 수신
    console.log('beep 이벤트 발생');
    console.log('수신 데이터', data);
    console.log(data.msg);
		socket.emit('boop', {msg: "Hello client!!!"});  // 클라이언트로 이벤트 송신
	});
})