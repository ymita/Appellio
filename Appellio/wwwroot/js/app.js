//読み上げ対象行インデックス(starting from 1)
let currentRowIndex;

//読み上げメッセージ
let message;

let isSpeechCancelled = false;

document.addEventListener("DOMContentLoaded", function () {
    //ページ表示時に speechSynthesis をキャンセル
    speechSynthesis.cancel();
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
    speechSynthesis.cancel();
});

const table = document.getElementById("wordsTable");

table.addEventListener("click", (evt) => {

    if (
        evt.target.tagName === "TD"
        &&
        evt.target.parentElement.tagName === "TR"
    ) {
        const row = evt.target.parentNode;

        isSpeechCancelled = true;
        speechSynthesis.cancel();

        //クリックされた行を読み上げ対象行インデックスに設定する。
        currentRowIndex = row.rowIndex;

        //アクティブ行スタイルの更新
        setActiveRow(currentRowIndex);

        //読み上げ対象セルを取得
        const cell = findText(row);

        //読み上げ
        readText(cell);
    }
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

function readText(text) {
    message = new SpeechSynthesisUtterance();
    message.onend = readEnded;
    message.lang = 'en-US';
    message.text = text;
    speechSynthesis.speak(message);
}

function readEnded(evt) {
    console.log("read ended", evt);

    if (isSpeechCancelled === true) {
        //isSpeechCancelled の値を false に戻す（初期化）
        isSpeechCancelled = false;
        return;
    }

    //isSpeechCancelled の値を false に戻す（初期化）
    isSpeechCancelled = false;

    //次の行が存在すれば、次の行の読み上げを実施する。
    if (isNextRow(currentRowIndex)) {
        //読み上げ対象行インデックスの更新
        currentRowIndex = currentRowIndex + 1;

        //行オブジェクトを取得
        const row = table.rows.item(currentRowIndex);

        //アクティブ行スタイルの更新
        setActiveRow(currentRowIndex);

        //読み上げ対象セルを取得
        const cell = findText(row);

        //読み上げ
        readText(cell);
    }
    else {
        //次の行が存在しなければ、処理を終了する。
        console.log("process ends");

        //既存のアクティブ行スタイルは削除する。
        removeActiveRow();

        speechSynthesis.cancel();
    }
}
