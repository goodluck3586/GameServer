var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4567);  // 포트번호 설정 
console.log('서버 실행');

// 클라이언트가 접속하면 실행되는 이벤트 함수
io.on('connection', function(socket){
  console.log('클라이언트 접속');

	socket.on('game play', function(data){
		console.log('game play 이벤트 발생');
		console.log(data);
	});

  // 클라이언트가 'beep'라는 이벤트가 수신되면 실행되는 함수
	socket.on('beep', function(data){
    console.log('beep 이벤트 발생 : ', data);
		socket.emit('boop', {boopMsg:"Hello Client"});  // 클라이언트로 'boop' 이벤트 전송
		io.emit('broadMSG', {broadMsg:"broadcast message"});
		//socket.broadcast.emit('broadMSG', "broadcast message");
	});
})

