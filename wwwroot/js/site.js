// start vars

/**
 * @type {HTMLSpanElement}
*/
let counterSpan = null,
    /**
     * @type {HTMLDivElement}
    */
    inputBox = null

/**
 * @return {void}
*/
function cleanPaste(e) {
    /**
     * @type {HTMLDivElement}
     */
    const target = e.target;

    if (!target) return;

    e.stopPropagation();
    e.preventDefault();

    const pasteData = e.clipboardData;
    const cleanDataText = pasteData?.getData("text");
    if (cleanDataText) {
        const currentText = target.innerText.trim();
        const remainingCharacters = 1024 - currentText.length;

        const textToAdd = cleanDataText.trim().slice(0, remainingCharacters);

        target.innerText = currentText + textToAdd;

        const range = document.createRange();
        const selection = window.getSelection();

        range.selectNodeContents(target);
        range.collapse(false);

        selection?.removeAllRanges();
        selection?.addRange(range);

        target.focus();
        target.scrollTop = target.scrollHeight;

        const newLength = target.innerText.length;
        counterSpan.innerText = newLength + "/1024";

        return;
    }
}

/**
 * @description pq crls js n deteta backspace se o evento for keypress?
 * @param {InputEvent} e 
 * @returns {void}
 */
function inputCheck(e) {
    /**
     * @type {HTMLDivElement}
     */
    const input = e.target;
    const currentLength = input.innerText.length;

    if (e.inputType === 'deleteContentBackward' || e.ctrlKey || e.altKey) {
        counterSpan.innerText = currentLength + "/1024";
        return;
    }

    if (!currentLength > 1024) {
        counterSpan.innerText = currentLength + "/1024";
    }
}

/**
 * @description pq crls js n deteta backspace se o evento for keypress?
 * @param {InputEvent} e 
 * @returns {void}
 */
function keydownCheck(e) {
    /**
     * @type {HTMLDivElement}
     */
    const input = e.target;
    const currentLength = input.innerText.length + 1;

    if (e.key.match(/Backspace|ArrowUp|ArrowDown|ArrowLeft|ArrowRight/) || e.ctrlKey || e.altKey) {
        return
    }

    if (currentLength > 1024) {
        e.preventDefault();
        return;
    }


}



document.addEventListener("DOMContentLoaded", function () {
    // after page being fully loaded store div
    counterSpan = document.getElementById("counterLength");
    inputBox = document.getElementById("postContent");

    inputBox.addEventListener("input", inputCheck);
    inputBox.addEventListener("keydown", keydownCheck);
    inputBox.addEventListener("paste", cleanPaste);
});
