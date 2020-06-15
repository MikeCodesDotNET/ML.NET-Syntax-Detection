const mongoose = require('mongoose');

//create Url Schema (format)
const urlSchema = new mongoose.Schema({
    originalUrl: {
        type : String,
        required : true
    },
    shortUrl : {
        type : String,
        required : true
    },
    unique_name : {
        type : String,
        required : true
    },
    dateCreated : {
        type : Date,
        default : Date.now
    }  
});
//Use schema to create a Url model
const Url = mongoose.model('Url', urlSchema);

//Export Url Model
module.exports = Url;