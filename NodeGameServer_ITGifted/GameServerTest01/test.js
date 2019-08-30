var fs = require('fs');  // 파일 시스템 모듈

var data;

data = fs.readFileSync('./readme.txt');
console.log(data.toString());

data = fs.readFileSync('./readme2.txt');
console.log(data.toString());

data = fs.readFileSync('./readme3.txt');
console.log(data.toString());






