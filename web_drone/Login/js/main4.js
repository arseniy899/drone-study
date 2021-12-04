const quizData = [
    {
        question: "Вы правильно заметили, тогда что нужно сделать?",
        a: "увеличить высоту полёта",
        b: "уменьшить высоту полёта",
        c: "сократить дистанцию",
        d: "не реагировать",
        correct: "c",
    }]

const quiz= document.getElementById('quiz')
const answerEls = document.querySelectorAll('.answer')
const questionEl = document.getElementById('question')
const a_text = document.getElementById('a')
const b_text = document.getElementById('b')
const c_text = document.getElementById('c')
const d_text = document.getElementById('d')
const e_text = document.getElementById('e')
const submitBtn = document.getElementById('submit')
var value = 0


let currentQuiz = 0
let score = 0

//loadQuiz()

function loadQuiz() {
	if (value == 2) {return 1}
	value = 1

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

function decide(answer){
	if (value == 1){
    if(answer) {
       if(answer === quizData[currentQuiz].correct) {
           e_text.innerText = "Да, всё верно!"
		   value = 2
       }
		else {e_text.innerText = "Нет, вы ошиблись."
		   value = 2}
}}}



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
           <h2 class="main-form-title p-b-51">Вы ответили правильно на ${score}/${quizData.length} вопросов</h2>
           <a class = "login-form-btn" href="menu.html">Назад в меню</a>
           `
       }
    }
})