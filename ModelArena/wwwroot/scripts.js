function scrollToBottom(elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.scrollTop = element.scrollHeight;
    } else {
        console.error(`Element with ID "${elementId}" not found.`);
    }
}

function highlightCode() {
    if (typeof Prism === "undefined") {
        console.error("Prism is not loaded.");
        return;
    }

    const codeBlocks = document.querySelectorAll("pre code");
    codeBlocks.forEach((block) => {
        Prism.highlightElement(block);
    });
}


