var working = false;
$('.login').on('submit', function (e) {
    e.preventDefault();
    if (working) return;
    working = true;
    var $this = $(this),
        $state = $this.find('button > .state');
    $this.addClass('loading');
    $state.html('Authenticating');
    setTimeout(function () {
        $this.addClass('ok');
        $state.html('Welcome back!');
        setTimeout(function () {
            $state.html('Log in');
            $this.removeClass('ok loading');
            working = false;
        }, 4000);
    }, 3000);
});

///*CountDown*/
//const startingM = 10;
//let time = startingM * 60;

//const countdownEl = document.getElementById('countdown')

//setInterval(updateCountdown, 1000);

//function updateCountdown() {
//    const minutes = Math.floor(time / 60);
//    let secounds = time * 60;

//    countdownEl.innerHTML = `${minutes}: ${secounds}`;
//    time--;
//}

(function () {
    $('#msbo').on('click', function () {
        $('body').toggleClass('msb-x');
    });
}());


function startTimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.textContent = minutes + ":" + seconds;

        if (--timer < 0) {
            timer = duration;
        }
    }, 1000);
}

window.onload = function () {
    var valueTime = document.getElementById("valueTime").value + "";
    var Minutes = valueTime.substring(0, 2) * 60;
    display = document.querySelector('#time');
    startTimer(Minutes, display);
};