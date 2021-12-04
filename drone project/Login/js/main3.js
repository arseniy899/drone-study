// JavaScript Document
function createTasks(){
var x = new XMLHttpRequest();
x.open("GET", "http://127.0.0.1:8000/api/v1/tests/");
x.onload = function (){
    alert( x.responseText);
}
x.send(null);
z = x.responseText;

let li = document.createElement('li')
li.innerHTML += '<div style="padding-top:20px;"><a class = "login-form-btn" href="quiz.html" >Тест1(локальный)</a></div>';
  document.getElementById("listok").appendChild(li);
	
//for i in x[name] {
//	
//}
//} 