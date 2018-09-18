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

//Rename part
var renameButton = document.getElementById("rename");
var renmaeDialogue = document.getElementById("renameDialogue");
var renameInput = document.getElementById("renameInput");
var renameSubmit = document.getElementById("renameSubmit");
var close = document.getElementsByClassName("close")[0];




renameButton.onclick = function () {
    ContextHiddent();
    renameInput.value = currentChannelClickedID.childNodes[1].childNodes[3].innerHTML;
    document.body.style.overflowY = "hidden";
    dialoguemodal.style.display = "block";  
    renmaeDialogue.style.display = "block";
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
    renmaeDialogue.style.display = "none";
};

close.onclick = function () {
    document.body.style.overflowY = "auto";
    dialoguemodal.style.display = "none";
    renmaeDialogue.style.display = "none";

};


//Delete part
var deletebutton = document.getElementById("delete");
var deleteDialogue = document.getElementById("deleteDialogue");
var deleteYesButton = document.getElementById("deleteYes");
var deleteNoButton = document.getElementById("deleteNo");
var deleteClose = document.getElementsByClassName("close")[1];

deletebutton.onclick = function () {
    ContextHiddent();
    document.body.style.overflowY = "hidden";
    dialoguemodal.style.display = "block";
    deleteDialogue.style.display = "block";
};

deleteYesButton.onclick = function () {
    //so ajax request will do later
    deleteCloseFunc();
};

deleteNoButton.onclick = function () {
    deleteCloseFunc();
};

deleteClose.onclick = function () {
    deleteCloseFunc();
};

function deleteCloseFunc() {
    document.body.style.overflowY = "auto";
    dialoguemodal.style.display = "none";
    deleteDialogue.style.display = "none";
}

//Info part
var infoButton = document.getElementById("info");
var infoDialogue = document.getElementById("infoDialogue");
var infoClose = document.getElementsByClassName("close")[2];

infoButton.onclick = function () {
    ContextHiddent();
    document.body.style.overflowY = "hidden";
    dialoguemodal.style.display = "block";
    infoDialogue.style.display = "block";
};

infoClose.onclick = function () {
    document.body.style.overflowY = "auto";
    dialoguemodal.style.display = "none";
    infoDialogue.style.display = "none";
};








window.onclick = function (event) {
    if (event.target === dialoguemodal) {
        document.body.style.overflowY = "auto";

        dialoguemodal.style.display = "none";
        renmaeDialogue.style.display = "none";
        deleteDialogue.style.display = "none";
        infoDialogue.style.display = "none";

    }
};




//var e = document.getElementById("0");
//var ef = e.childNodes[1];
//e.parentNode.removeChild;
//console.log(e.parentNode);


