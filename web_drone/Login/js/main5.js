var i = 0
var jsonStrOld
var obj
function collect() {
//var x = new XMLHttpRequest();
//x.open("GET", "http://127.0.0.1:8000/api/v1/questions/");
//x.onload = function (){
//    alert( x.responseText);
//}
//x.send(null);
	
var q_s = document.getElementById('q_s').value
var q_f = document.getElementById('q_f').value
var a_text = document.getElementById('a').value
var b_text = document.getElementById('b').value
var c_text = document.getElementById('c').value
var d_text = document.getElementById('d').value
var e_text = document.getElementById('e').value
var jsonStrNew =  [
    {
      "short_name": q_s,
        "wording": q_f,
        "category": "Тест",
        "answers": [ {"a":a_text,
					 "b":b_text,
					 "c":c_text,
					 "d":d_text,
					 "e":e_text,}
     ]	}  ]
if (jsonStrOld == null)
{jsonStrOld = jsonStrNew
 i++
alert(JSON.stringify(jsonStrOld))
}
else{
obj = jsonStrOld;
obj.push(jsonStrNew);
jsonStrOld = JSON.stringify(obj);
i++;
	alert(i)
alert(jsonStrOld)}
	
	
}// JavaScript Document
function end() {
const url = 'https://example.com/profile';
const data = jsonStrOld;

try {
  const response = await fetch(url, {
    method: 'POST', // или 'PUT'
    body: JSON.stringify(data), // данные могут быть 'строкой' или {объектом}!
    headers: {
      'Content-Type': 'application/json'
    }
  });
  const json = await response.json();
  console.log('Успех:', JSON.stringify(json));
} catch (error) {
  console.error('Ошибка:', error);
}
}