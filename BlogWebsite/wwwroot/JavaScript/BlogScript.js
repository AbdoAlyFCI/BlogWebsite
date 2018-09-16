/*Sign Up page*/

//List of Years
(function YearList() {
    var yearSelect = document.getElementById("year");
    var d = new Date();
    var statYear = 1905;
    var endYear = d.getFullYear();
    //generate years
    for (var i = statYear; i <= endYear; i++) {
        var option = document.createElement("option");
        option.setAttribute("value", i);
        option.text = i;
        yearSelect.appendChild(option);
    }
})();

//List of Months
(function MonthsList() {
    var monthSelect = document.getElementById("month");
    var month = ["January", "February", "March", "April", "May", "June", "July",
        "August", "September", "October", "November", "December"];

    for (var i = 0; i < month.length; i++) {
        var option = document.createElement("option");
        option.setAttribute("value", month[i]);
        option.text = month[i];
        monthSelect.appendChild(option);
    }
})();



//list of days
(function DaysList() {
    var daySelect = document.getElementById("day");
    var startDay = 1;
    var endDay = 31;
    //generate days
    for (var i = startDay; i <= endDay; i++) {
        var option = document.createElement("option");
        option.setAttribute("value", i);
        option.text = i;
        daySelect.appendChild(option);
    }
})();

/*ChannelPage*/
function iconHoverOver(element) {
    element.style.backgroundColor = 'rgba(8, 73, 244, 0.46)';
    element.style.border = '2px solid blue';
}

function iconHoverOut(element) {
    element.style.backgroundColor = "transparent";
    element.style.border ="2px solid transparent";
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





