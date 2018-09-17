/*ChannelPage*/
function iconHoverOver(element) {
    element.style.backgroundColor = 'rgba(8, 73, 244, 0.46)';
    element.style.border = '2px solid blue';
}

function iconHoverOut(element) {
    element.style.backgroundColor = "transparent";
    element.style.border = "2px solid transparent";
}

/*Long click event and context menu for channel folders and files*/
var currentChannelClickedID;
var seconds;
var timer;
var isTrue;
var hidden = false;
//1-Long Click

function onLongClick(eID) {
    var delay = 500;   //how much long to hold
    isTrue = true;
    timer = setTimeout(function () { changes(eID); }, delay);
}

function changes(eID) {

    if (timer)
        clearTimeout(timer);

    if (isTrue) {
        currentChannelClickedID = document.getElementById(eID);
        var contextMenu = document.getElementById("contextMenu");
        contextMenu.style.visibility = "visible";
        contextMenu.style.opacity = 1;
        seconds = 10;
        hidden = true;
        countDown();
        
    }
}

function reset() {
    isTrue = false;
}
var ChannelContainer = document.getElementById("ChannelContainer");
ChannelContainer.ondblclick = function () {
    ContextHiddent();
};
window.onscroll = function () { ContextHiddent(); };



//CountDown to make context menu hidden

function countDown() {
    if (hidden === false) {
        console.log("it is over");
        return;
    }
    if (seconds === 1) {
        console.log("Time end ");
        ContextHiddent();
        return;
    }
    seconds--;
    setTimeout(countDown, 1000);

}

function ContextHiddent() {
    var contextMenu = document.getElementById("contextMenu");
    contextMenu.style.visibility = "hidden";
    contextMenu.style.opacity = 0;
    hidden = false;
}

//Dialogue part
var dialoguemodal = document.getElementById("dialoguemodal");
var renameButton = document.getElementById("rename");
var renameInput = document.getElementById("renameInput");
var renameSubmit = document.getElementById("renameSubmit");
var close = document.getElementsByClassName("close")[0];


renameButton.onclick = function () {
    ContextHiddent();
    renameInput.value = currentChannelClickedID.childNodes[1].childNodes[3].innerHTML;
    document.body.style.overflowY = "hidden";
    dialoguemodal.style.display = "block";    
};

renameInput.onkeyup = function () {

    if (renameInput.value === "")
        renameSubmit.disabled = true;
    else
        renameSubmit.disabled = false;
};
renameSubmit.onclick = function () {
    currentChannelClickedID.childNodes[1].childNodes[3].innerHTML = renameInput.value;
    document.body.style.overflowY = "auto";
    dialoguemodal.style.display = "none";
};

close.onclick = function () {
    document.body.style.overflowY = "auto";
    dialoguemodal.style.display = "none";

};

window.onclick = function (event) {
    if (event.target === dialoguemodal) {
        document.body.style.overflowY = "auto";
        dialoguemodal.style.display = "none";
    }
};
//var e = document.getElementById("0");
//var ef = e.childNodes[1];
//var echild = e.childNodes[0];
//console.log(ef.childNodes[0]);
//console.log(ef.childNodes[1]);
//console.log(ef.childNodes[2]);
//console.log(ef.childNodes[3]);
//console.log(ef.childNodes[4]);

