var GenralinfoBtn = document.getElementById("genraInfoBtn");
var followedChannelBtn = document.getElementById("followedChannelBtn");
var muteTagsBtn = document.getElementById("muteTagsBtn");
var blockUsersBtn = document.getElementById("blockUsersBtn");
var changeImgPicBtn = document.getElementById("changeImgPicBtn");


function ClearResult() {
    document.getElementById("genraInfo").style.display = "none";
    document.getElementById("followedChannel").style.display = "none";
    document.getElementById("blockUsers").style.display = "none";
    document.getElementById("muteTags").style.display = "none";
}


GenralinfoBtn.onclick = function () {

    ClearResult();
    document.getElementById("genraInfo").style.display = "Block";

};

followedChannelBtn.onclick = function () {
    ClearResult();
    document.getElementById("followedChannel").style.display = "Block";
};

muteTagsBtn.onclick = function () {
    ClearResult();
    document.getElementById("muteTags").style.display = "Block";

};

blockUsersBtn.onclick = function () {
    ClearResult();
    document.getElementById("blockUsers").style.display = "Block";

};

changeImgPicBtn.onclick = function () {
    document.documentElement.style.overflow = 'hidden';
    document.getElementById("Headers").style.display = "Block";

}


