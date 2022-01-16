var io = require('socket.io')(process.env.PORT || 4000);

var Player = require("./components/Player.js");

var players = [];
var sockets = [];

console.log('Server start...');

io.on('connection', function(socket) {
	console.log('Connection Made!');

	var player = new Player();
	var thisPlayerID = player.id;

	players[thisPlayerID] = player;
	sockets[thisPlayerID] = socket;

	socket.emit('register', {id: thisPlayerID});
	socket.emit('spawn', player);
	socket.broadcast.emit('spawn', player)

	for (var playerID in players) {
		if(playerID != thisPlayerID) {
			socket.emit('spawn', players[playerID]);
		}
	}
	

	socket.on('disconnect', function(){
		console.log('A player has disconnected');
		delete players[thisPlayerID];
		delete socket[thisPlayerID];
		socket.broadcast.emit('disconnected', player)
	});
});

