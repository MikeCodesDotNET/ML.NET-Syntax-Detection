const mongoose = require('mongoose');

//MONGO_URI 
const MONGO_URI = process.env.MONGO_URI || 'mongodb://localhost:27017/url'; 

//Connect to DB function
const connect = () => {
  mongoose.connect(MONGO_URI, {useNewUrlParser : true, useUnifiedTopology : true})
  .then(() => console.log('DB connected'))
  .catch(err => console.log(err));
  //On connection error, log the message
  mongoose.connection.on('error', err => {
    console.log(`DB connection error : ${err.message}`);
  });
}

//export the connect function, to use in server.js
module.exports = { connect }; 