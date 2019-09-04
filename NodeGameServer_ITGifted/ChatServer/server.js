var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567);
console.log("127.0.0.1:4567에서 서버 실행중...");

io.on('connection', function(socket){
  console.log("클라이언트 접속");

	socket.on('beep', function(data){
    console.log('beep 이벤트 수신');
    console.log(data);  // 클라이언트에게 받은 데이터 출력

		socket.emit('boop', {msg: "Hello Client"});  // 클라이언트로 데이터 전송
	});
})