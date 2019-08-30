var fs = require('fs');

fs.readFile('./readme.txt', function(err, data){
  if(err){
    console.log('에구 에러가 났네...');
  }else{
    console.log(data.toString());
  }
});
fs.readFile('./readme2.txt', function(err, data){
  if(err){
    console.log('에구 에러가 났네...');
  }else{
    console.log(data.toString());
  }
});
fs.readFile('./readme3.txt', function(err, data){
  if(err){
    console.log('에구 에러가 났네...');
  }else{
    console.log(data.toString());
  }
});
console.log('Hello');


