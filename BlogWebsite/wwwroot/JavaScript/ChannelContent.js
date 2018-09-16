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

//1-Long Click
var timer;
var isTrue;
function onLongClick() {
    var delay = 500;   //how much long to hold
    isTrue = true;
    timer = setTimeout(function () { changes() }, delay);
}

function changes() {

    if (timer)
        clearTimeout(timer);

    if (isTrue)
        alert("Good start");

}

function reset() {
    isTrue = false;
}


