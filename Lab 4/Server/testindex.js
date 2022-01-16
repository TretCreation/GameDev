var io = require('socket.io')(process.env.PORT || 4000);

console.log('Server test');

io.on('connection', function(socket) {
	console.log('Connection Made!');

	socket.on('disconnect', function(){
		console.log('A player has disconnected');
	});
});

