let defaultDescriptor = ".NET Developer";
const descriptors = [".NET Developer", "Blazor", "C#", "ASP.NET MVC", "Razor Pages", "Entity Framework Core"];
var descriptorsToDisplay = ["Blazor", "C#", "ASP.NET MVC", "Razor Pages", "Entity Framework Core"];

const getRandomWord = () => {
    if (descriptorsToDisplay.length <= 0) {
        descriptorsToDisplay = [...descriptors];
    }
    var index = Math.floor(Math.random() * descriptorsToDisplay.length);
    return descriptorsToDisplay.splice(index, 1)[0];
};

const updateContent = (specifiedWord = "") => {

    if (specifiedWord == "") {
        defaultDescriptor = getRandomWord();
    } else {
        defaultDescriptor = specifiedWord;
    }

    var currentDescriptor = document.querySelector("#title-descriptors");
    currentDescriptor.classList.add("slide-in");
    var newDescriptor = currentDescriptor.cloneNode(true);
    newDescriptor.innerText = defaultDescriptor;
    currentDescriptor.parentNode.replaceChild(newDescriptor, currentDescriptor);
};

const cycleDescriptors = () => {
    var intId;
    setTimeout(() => {
        intId = setInterval(updateContent, 5 * 1000);
        setTimeout(() => {
            clearInterval(intId);
            updateContent(defaultDescriptor);
        }, 60 * 1000);
    }, 1 * 1000);
};
