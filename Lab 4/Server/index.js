var io = require('socket.io')(4567);

var Player = require("./components/Player.js");

var players = [];
var sockets = [];

console.log('Server start...');

Object.defineProperty(Number.prototype, 'formatNumber', {
    value: function() {
        return new Intl.NumberFormat('en-US', {style: 'decimal'}).format(this);
    }
});

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

	socket.on('updatePosition', function(data) {
		// new Intl.NumberFormat().format(data);
		// new Intl.NumberFormat('ru-RU').format(data)
		player.position.x = data.position.x;

		// player.position.x = float.Parse((Mathf.Round(transform.position.x * 1000.0f) / 1000.0f).ToString("0.000"), NumberStyles.Any, CultureInfo.InvariantCulture);
		player.position.y = data.position.y;

		socket.broadcast.emit('updatePosition', player);
	});

	socket.on('disconnect', function() {
		console.log('A player has disconnected');
		delete players[thisPlayerID];
		delete sockets[thisPlayerID];
		socket.broadcast.emit('disconnected', player)
	});
});

