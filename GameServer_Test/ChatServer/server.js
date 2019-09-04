var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567);
console.log('127.0.0.1:4567 서버 실행');

io.on('connection', function(socket){
  console.log('클라이언트 접속');
  console.log('New connection from ' + socket.request.connection.remoteAddress);

  // 새로운 채팅 메시지가 수신되면 모두에게 전송
  socket.on('newMsg', function(data){
    console.log('newMsg 이벤트 수신 : ', data);
    io.emit('broadcastMsg', {msg: data.msg});
    //io.emit('broadcastMsg', {msg: data.msg, userName: data.userName});
  });

	socket.on('beep', function(data){
    console.log('beep 이벤트 발생 : ');  
    console.log(data);  // 클라이언트에서 받은 데이터 출력
		socket.emit('boop', {msg: "Hello Client!"});  // 클라이언트로 데이터 전송
	});
});
















