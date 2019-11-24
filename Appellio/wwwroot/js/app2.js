// 読み上げメッセージ
let message;

let isSpeechCancelled = false;

document.addEventListener("DOMContentLoaded", function () {
    //ページ表示時に speechSynthesis をキャンセル
    speechSynthesis.cancel();

    $('#exampleModalCenter').on('shown.bs.modal', function (e) {
        var isAutoplay = document.getElementById('isAutoplay').checked;
        if (isAutoplay) {
            //play();
            togglePlayStopButtons(false);
        } else {
            togglePlayStopButtons(true);
        }
    });
    $('#exampleModalCenter').on('hide.bs.modal', function (e) {
        stop();
        togglePlayStopButtons(true);
    });
});
window.addEventListener("beforeunload", function () {
    //ページ離脱時に speechSynthesis をキャンセル
    speechSynthesis.cancel();
});

//const table = document.getElementById("wordsTable");

message = new SpeechSynthesisUtterance();
message.onend = readEnded;
message.lang = 'en-US';
function readText(text) {
    isSpeechCancelled = false;
    message.text = text;
    speechSynthesis.speak(message);
}

function readEnded(evt) {
    if (isSpeechCancelled) {
        return;
    }
    togglePlayStopButtons(true);
    var isAutoplay = document.getElementById('isAutoplay').checked;
    if (!isAutoplay) {
        return;
    }
    isSpeechCancelled = false;
    next();
    play();
}

//table.addEventListener('click', onCellClicked);
//table.addEventListener('ontouchend', onCellClicked, true);

//function onCellClicked(evt) {
//    alert();
//    // 詳細リンクをクリックすると、オーバーレイは表示しない
//    //if (evt.target.tagName !== 'TD') {
//    //    return;
//    //}

//    // 単語を取得
//    var id = +evt.target.parentElement.dataset.id;
//    renderWord(id);

//    $('#exampleModalCenter').modal('show');
//}

function next() {
    stop();
    var id = +document.getElementById('id').value;
    var word = words.find(x => x.id === id);
    var wordIndex = words.indexOf(word);

    if (words[wordIndex + 1]) {
        id = words[wordIndex + 1].id;
    } else {
        id = words[0].id;
    }
    renderWord(id);
}

function prev() {
    stop();
    var id = +document.getElementById('id').value;
    var word = words.find(x => x.id === id);
    var wordIndex = words.indexOf(word);

    if (words[wordIndex - 1]) {
        id = words[wordIndex - 1].id;
    } else {
        id = words[words.length - 1].id;
    }
    renderWord(id);
}

function renderWord(id) {
    var word = words.find(x => x.id === id);
    document.getElementById('id').value = word.id;
    document.getElementById('spelling').textContent = word.spelling;
    document.getElementById('meaning').textContent = word.meaning;
    document.getElementById('text').textContent = word.text;
    document.getElementById('textMeaning').textContent = word.textMeaning;
}

function play() {
    togglePlayStopButtons(false);
    var text = document.getElementById('text').textContent;
    setTimeout(function () {
        readText(text);
    }, 300);
}

function playButtonClicked(id) {
    //renderWord(id);
    //var text = document.getElementById('text').textContent;
    //$('#exampleModalCenter').modal('show');
    ////document.getElementById('playButton').style.display = 'none';
    ////document.getElementById('stopButton').style.display = '';
    //readText(text);

    renderWord(id);

    $('#exampleModalCenter').modal('show');
    setTimeout(function () {
        play();
    }, 200);
}

function stop() {
    togglePlayStopButtons(true);
    isSpeechCancelled = true;
    speechSynthesis.cancel();
}

function togglePlayStopButtons(showPlayButton) {
    if (showPlayButton) {
        document.getElementById('playButton').style.display = '';
        document.getElementById('stopButton').style.display = 'none';
    } else {
        document.getElementById('playButton').style.display = 'none';
        document.getElementById('stopButton').style.display = '';
    }
    var isAutoplay = document.getElementById('isAutoplay').checked;
    if (isAutoplay) {
        document.getElementById('prevButton').style.display = 'none';
        document.getElementById('nextButton').style.display = 'none';
    } else {
        document.getElementById('prevButton').style.display = '';
        document.getElementById('nextButton').style.display = '';
    }
}