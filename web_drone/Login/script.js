// JavaScript Document
const quizData = [
    {
        question: "В какие температуры дрон корректно работает",
        a: "до 0",
        b: "до -10",
        c: "до -20",
        d: "до -30",
        correct: "b",
    },
    {
        question: "С какого года ввели штрафы за незарегистрированный запуск",
        a: "2016",
        b: "2019",
        c: "2020",
        d: "2018",
        correct: "b",
    },
    {
        question: "Максимальная высота полета",
        a: "2000м",
        b: "1000м",
        c: "500м",
        d: "100м",
        correct: "d",
    },
    {
        question: "макисмальная дистанция для комфортного управления",
        a: "1000м",
        b: "3000м",
        c: "800м",
        d: "100м",
        correct: "d",
	},

];

const quiz= document.getElementById('quiz')
const answerEls = document.querySelectorAll('.answer')
const questionEl = document.getElementById('question')
const a_text = document.getElementById('a_text')
const b_text = document.getElementById('b_text')
const c_text = document.getElementById('c_text')
const d_text = document.getElementById('d_text')
const submitBtn = document.getElementById('submit')


let currentQuiz = 0
let score = 0

loadQuiz()

function loadQuiz() {

    deselectAnswers()

    const currentQuizData = quizData[currentQuiz]

    questionEl.innerText = currentQuizData.question
    a_text.innerText = currentQuizData.a
    b_text.innerText = currentQuizData.b
    c_text.innerText = currentQuizData.c
    d_text.innerText = currentQuizData.d
}

function deselectAnswers() {
    answerEls.forEach(answerEl => answerEl.checked = false)
}

function getSelected() {
    let answer
    answerEls.forEach(answerEl => {
        if(answerEl.checked) {
            answer = answerEl.id
        }
    })
    return answer
}


submitBtn.addEventListener('click', () => {
    const answer = getSelected()
    if(answer) {
       if(answer === quizData[currentQuiz].correct) {
           score++
       }

       currentQuiz++

       if(currentQuiz < quizData.length) {
           loadQuiz()
       } else {
           quiz.innerHTML = `
           <h2 class="main-form-title p-b-51">Ты ответил правильно на ${score}/${quizData.length} вопросов</h2>
           <a class = "login-form-btn" href="menu.html">Назад в меню</a>
           `
       }
    }
})