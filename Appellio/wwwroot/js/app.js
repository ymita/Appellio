﻿//読み上げ対象行インデックス(starting from 1)
let currentRowIndex;

//読み上げメッセージ
let message;

let isSpeechCancelled = false;
let cancelReason = "";

document.addEventListener("DOMContentLoaded", function () {
    //ページ表示時に speechSynthesis をキャンセル
    speechSynthesis.cancel();

    $('#exampleModalCenter').on('hide.bs.modal', function (e) {
        stop();
    });
});
window.addEventListener("beforeunload", function () {
    //ページ離脱時に speechSynthesis をキャンセル
    speechSynthesis.cancel();
});

const startButton = document.getElementById("start");
const pauseButton = document.getElementById("pause");

startButton.addEventListener("click", (e) => {
    isSpeechCancelled = false;

    let row = getActiveRow();

    if (row === null) {
        //1行目を読み上げ対象行インデックスに設定する。
        currentRowIndex = 1;

        //アクティブ行スタイルの更新
        setActiveRow(currentRowIndex);

        //行オブジェクトを取得
        row = table.rows.item(currentRowIndex);
    }

    //読み上げ対象セルを取得
    const cell = findText(row);

    //読み上げ
    readText(cell);
});

pauseButton.addEventListener("click", (e) => {
    isSpeechCancelled = true;
    cancelReason = "stopButton";
    speechSynthesis.cancel();
});

const table = document.getElementById("wordsTable");

table.addEventListener("click", (evt) => {
    if (evt.target.classList.contains("table-cell") === false) {
        return;
    }
    if (evt.target.classList.contains("table-cell")) {
        Array.from(table.rows).forEach(row => { row.classList.remove("active-row"); });
        evt.target.parentNode.classList.add("active-row");

        isSpeechCancelled = true;
        cancelReason = "otherRow";
        speechSynthesis.cancel();
    }

    //if (
    //    evt.target.tagName === "TD"
    //    &&
    //    evt.target.parentElement.tagName === "TR"
    //) {
    //    const row = evt.target.parentNode;

    //    isSpeechCancelled = true;
    //    speechSynthesis.cancel();

    //    //クリックされた行を読み上げ対象行インデックスに設定する。
    //    currentRowIndex = row.rowIndex;

    //    //アクティブ行スタイルの更新
    //    setActiveRow(currentRowIndex);

    //    //読み上げ対象セルを取得
    //    const cell = findText(row);

    //    //読み上げ
    //    readText(cell);
    //}
});

function findText(row) {
    return row.getElementsByClassName(".cell-text").item(0).innerText;
}

function getActiveRow() {
    var activeRows = table.getElementsByClassName("table-primary");
    if (activeRows.length === 0) {
        return null;
    }
    return table.getElementsByClassName("table-primary")[0];
}

function setActiveRow(currentRowIndex) {
    //既存のアクティブ行スタイルをクリア
    var activeRow = getActiveRow();

    if (activeRow !== null) {
        //Array.from(activeRows).forEach(row => row.classList.remove("table-primary"));
        activeRow.classList.remove("table-primary");
    }

    //新たにアクティブ行スタイルを設定
    const row = table.rows.item(currentRowIndex);
    row.classList.add('table-primary');
}

function removeActiveRow() {
    //既存のアクティブ行スタイルをクリア
    var activeRows = table.getElementsByClassName("table-primary");
    Array.from(activeRows).forEach(row => row.classList.remove("table-primary"));
}

function isNextRow(currentRowIndex) {

    //読み上げ対象行インデックスを更新
    const nextRowIndex = currentRowIndex + 1;

    //読み上げ対象行インデックスから行オブジェクトを取得
    const nextRow = table.rows.item(nextRowIndex);

    if (nextRow !== null) {
        return true;
    }
    else {
        return false;
    }
}

message = new SpeechSynthesisUtterance();
//message.onstart = readStarted;
message.onend = readEnded;
message.lang = 'en-US';
function readText(text) {
    isSpeechCancelled = false;
    message.text = text;
    speechSynthesis.speak(message);
}

//function readStarted(evt) {

//}

function readEnded(evt) {
    if (isSpeechCancelled) {
        return;
    }
    isSpeechCancelled = false;
    next();
    play();
    //if (isSpeechCancelled === true) {
    //    //isSpeechCancelled の値を false に戻す（初期化）
    //    isSpeechCancelled = false;

    //    if (cancelReason === "otherRow") {
    //        //他の行がクリックされたことにより読み上げがキャンセルされた場合、新しいアクティブ行を読み上げる
    //        setTimeout(() => {
    //            const row = document.getElementsByClassName("active-row").item(0);

    //            //クリックされた行を読み上げ対象行インデックスに設定する。
    //            currentRowIndex = row.rowIndex;
    //            //アクティブ行スタイルの更新
    //            setActiveRow(currentRowIndex);
    //            //読み上げ対象セルを取得
    //            const cell = findText(row);
    //            //読み上げ
    //            readText(cell);
    //        }, 400);
    //    }
    //    return false;
    //}
    //cancelReason = "";
    ////isSpeechCancelled の値を false に戻す（初期化）
    //isSpeechCancelled = false;

    ////次の行が存在すれば、次の行の読み上げを実施する。
    //if (isNextRow(currentRowIndex)) {

    //    //読み上げ対象行インデックスの更新
    //    currentRowIndex = currentRowIndex + 1;

    //    //行オブジェクトを取得
    //    const row = table.rows.item(currentRowIndex);

    //    //アクティブ行スタイルの更新
    //    setActiveRow(currentRowIndex);

    //    //読み上げ対象セルを取得
    //    const cell = findText(row);

    //    //読み上げ
    //    readText(cell);
    //}
    //else {
    //    //次の行が存在しなければ、処理を終了する。

    //    //既存のアクティブ行スタイルは削除する。
    //    removeActiveRow();

    //    speechSynthesis.cancel();
    //}
}

table.addEventListener('click', function (evt) {

    // 詳細リンクをクリックすると、オーバーレイは表示しない
    if (evt.target.tagName !== 'TD') {
        return;
    }

    // 単語を取得
    var id = +evt.target.parentElement.dataset.id;
    renderWord(id);

    $('#exampleModalCenter').modal('show');
});

function next() {
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
}

function play() {
    var text = document.getElementById('text').textContent;
    readText(text);
}

function stop() {
    isSpeechCancelled = true;
    speechSynthesis.cancel();
}

window.onload = function () {
    
};
