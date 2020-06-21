const io = require('socket.io')(3000);

let users = {};

io.on('connection', socket => {

    let redis = require('redis').createClient(6379, 'redis');

    redis.on('message', function (chan, msg) {
        io.in(socket.id).emit('message', msg);
    });

    redis.on('pmessage', (chan, macAddress, msg) => {
        io.in(socket.id).emit('pmessage', msg);
    });

    users[socket.id] = {
        redis: redis,
        channels: new Set()
    }

    socket.on('subscribe', room => {

        if (!users[socket.id].channels.has(room)) {

            users[socket.id].channels.add(room);
            users[socket.id].redis.subscribe(room);
            socket.join(socket.id);
        }
    });

    socket.on('psubscribe', pattern => {

        if (!users[socket.id].channels.has(pattern)) {

            users[socket.id].channels.add(pattern);
            users[socket.id].redis.psubscribe(pattern);
            socket.join(socket.id);
        }
    });

    socket.on('unsubscribe', room => {

        if (users[socket.id].channels.has(room)) {

            users[socket.id].channels.delete(room);
            users[socket.id].redis.unsubscribe(room);
        }
    });

    socket.on('punsubscribe', room => {

        if (users[socket.id].channels.has(room)) {

            users[socket.id].channels.delete(room);
            users[socket.id].redis.punsubscribe(room);
        }
    });

    socket.on('disconnect', () => {

        users[socket.id].redis.unsubscribe();
        users[socket.id].redis.punsubscribe();
        users[socket.id] = {}
    });
});