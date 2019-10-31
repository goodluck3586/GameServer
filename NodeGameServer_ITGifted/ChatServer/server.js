var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567);
console.log("127.0.0.1:4567에서 서버 실행중...");

io.on('connection', function(socket){
  console.log("클라이언트 접속");

	// 새로운 사용자 조인
	socket.on("join", function(data){
		console.log("새로운 사용자 조인");
		io.emit("broadcastJoin", {userName: data.userName});
	});

	// 새로운 채팅 메시지 수신
	socket.on("newMsg", function(data){
		console.log("newMsg 이벤트 수신 : ", data);
		io.emit("broadcastMsg", {msg: data.msg, userName: data.userName});
	});
})