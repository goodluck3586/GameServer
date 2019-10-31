var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(4567, function(){
	console.log('4567포트에서 서버 실행.');
});

var userNicknames = [];

io.on('connection', function(socket){
	console.log('새로운 사용자 접속');
	socket.on('join', function(data){
		console.log('join 이벤트 발생');
		console.log('data', data);
		userNicknames.push(data.userNickname);

		if(userNicknames.length == 1)        // 접속자가 1명인 경우 기다리게함.
			socket.emit('waitAnotherUser');
		else if(userNicknames.length == 2){  //접속자가 2명인 경우 게임을 시작하도록함.
			console.log(userNicknames);
			console.log('playGame 이벤트 발생');
			io.emit('playGame');  
		}	           
	});
})
