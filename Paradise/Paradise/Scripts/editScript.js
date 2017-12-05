/*
Add event listener on window load to listen for left mouse clicks.
When the left mouse is clicked, if the toolbox is inactive and we are 
not currently editing, hide the toolbox. Also adds an event listener that
changes the toolbox z-index when the user scrolls.
*/
window.onload = function () {
    window.addEventListener('mouseup', function (e) {
        if (e.button == 0 && tlBxActive == false && inEdit == false) {
            hideToolBox();
        }
        else {
            setTlBxZIndex(highZIndex);
            currentZIndex = highZIndex;
        }
    });

    window.addEventListener('scroll', function (e) {
        if (currentZIndex > lowZIndex) {
            setTlBxZIndex(lowZIndex);
            currentZIndex = lowZIndex;
        }
    });
}

var canEdit = "false";    // Can we edit?
var tlBxActive = false; // Is the mouse pointer over the toolbox?
var inEdit = false;     // Are we currently editing?
var activeEl;           // The element that currently has focus.
var tlbxTopOffst = 120;
var highZIndex = 11;
var lowZIndex = 9;
var currentZIndex; // The current z-index of the toolbox

/*
Turns Edit mode ON and OFF. Also adds and removes tag attributes necessary
for editing page content.
*/
function toggleEditBtn() {
    //Switch modes
    if (canEdit == "true") {
        includeCntEdit("remove");
        canEdit = "false";
        chngeEditBtn("OFF");
    }
    else {
        includeCntEdit("add");
        canEdit = "true";
        chngeEditBtn("ON");
    }
    setEditable(canEdit);
}

/*
Changes the Color and appearance of the edit button.
*/
function chngeEditBtn(state) {
    var btn = document.getElementById("editBtn");

    if (state == "ON") {
        btn.innerHTML = "Turn edit OFF";
        btn.style.backgroundColor = "#e83930";
    }
    else {
        btn.innerHTML = "Turn edit ON";
        btn.removeAttribute("style");
    }
}

/*
Finds all elements that have the contenteditable attribute and
changes the value of that attribute for each element to the value of canEdit.
*/
function setEditable(isEditable) {
    var tags = document.getElementsByTagName("*"); //An array of all elements

    //Set the value of contenteditable for each element with that tag
    for (var i = 0; i < tags.length; i++) {
        if (tags[i].hasAttribute("contenteditable")) {
            tags[i].setAttribute("contenteditable", isEditable);
        }
    }
}

/*
Displays the edit tool box and places it near the content being edited.
*/
function dsplyToolBox() {
    inEdit = true;
    activeEl = document.activeElement;
    var rect = activeEl.getBoundingClientRect();
    var box = document.getElementById("toolbx");

    var bxTop = (rect.top - tlbxTopOffst) + window.scrollY;
    var bxLeft = (((activeEl.offsetWidth / 2) + rect.left) - (box.offsetWidth / 2)) + window.scrollX;

    box.style.top = (bxTop + "px");
    box.style.left = (bxLeft + "px");
    setTlBxZIndex(highZIndex);
    currentZIndex = highZIndex;
    $(".tlBxIcon").attr("draggable", false);
    box.style.visibility = "visible";
}

/*
Hides the edit tool box.
*/
function hideToolBox() {
    inEdit = false;
    if (tlBxActive == false) {
        document.getElementById("toolbx").style.visibility = "hidden";
    }
}

function setTlBxActive(sBool) {
    if (sBool == "true") {
        tlBxActive = true;
    }
    else {
        tlBxActive = false;
    }
}

function setTlBxZIndex(_zIndex) {
    document.getElementById("toolbx").style.zIndex = _zIndex;
}

/* Modified from Mozilla text editor code: https://developer.mozilla.org/en-US/docs/Rich-Text_Editing_in_Mozilla
   Formats the selected text based on the parameters given.
*/
function formatDoc(sCmd, sValue) {
    if (canEdit == "true") {
        if (sCmd == 'fontsize') {
            sCmd = 'insertHTML';
            var _fontSize = sValue;
            sValue = $('<span/>', {
                'text': document.getSelection()
            }).css('font-size', _fontSize + 'px').prop('outerHTML');
        }
        document.execCommand(sCmd, false, sValue);
    }
}

/*
Adds or removes the contenteditable, onfocus, and onblur attributes in all paragraph and
header tags. If the argument string passed to the function is "add", the attributes are added
to the tags. Any other string input results in the removal of the attributes.
*/
function includeCntEdit(sAction) {
    if (sAction == "add") {
        // Add contenteditable, onfocus, and onblur to paragraph and header tags.
        $("p,h1,h2,h3,h4,h5,h6").attr({
            contenteditable: "false",
            onfocus: "dsplyToolBox()",
            onblur: "hideToolBox()"
        });
    }
    else {
        // Remove contenteditable, onfocus, and onblur from paragraph and header tags.
        $("p,h1,h2,h3,h4,h5,h6").removeAttr("contenteditable onfocus onblur");
    }
}

// Sends a post request to the save controller
function savePage(controller, id, name, desc) {
    //Turn off editting
    canEdit = false;
    includeCntEdit("remove");
    chngeEditBtn("OFF");

    //Save the data
    $.post("/" + controller + "/Save", { page_ID: id, pageName: name, pageDesc: desc, content: document.getElementById("renderBody").innerHTML.toString() })
    .done(saveSuccess());
}

//The save success message
function saveSuccess() {
    alert("Changes have been saved!");
}

//The save failure message
function saveFail() {
    alert("Changes have not been saved! There was an issue!");
}