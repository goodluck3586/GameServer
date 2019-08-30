var io = require('socket.io')({
  transports: ['websocket'],
});

io.attach(3000);

// 클라이언트가 접속하면 connection 이벤트가 발생한다.
io.on('connection', function(socket){
  console.log('a user connected');

  socket.on('move', function(data){
    console.log('플레이어가 움직였다.');
    console.log(data);
    socket.emit('move', {serverMsg: 'player moved....'});
  });

  // 클라이언트 접속이 끊어지면 발생함.
  socket.on('disconnect', function(){
    console.log('user disconnected');
  });
});









