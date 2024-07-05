const books = document.querySelectorAll(".books");
let searchBtn = document.querySelector(".searchBtn");
let closeBtn = document.querySelector(".closeBtn");
let searchBox = document.querySelector(".searchBox");
const searchText = document.getElementById("searchText");
const suggestionsList = document.getElementById("suggestions-list");
var suggestions = [];
console.log(searchBtn);
closeBtn.onclick = function () {
    searchBox.classList.remove("active");
    closeBtn.classList.remove("active");

    searchText.value = "";
    books.forEach((e) => {
        e.style = "display:block";
    });
    searchBtn.classList.remove("hide");
    suggestions = [];
};
var list = [];
books.forEach((b, i) => {
    console.log(b);
    list.push({
        name: b.querySelector("#name").innerHTML,
        url: b.querySelector("#url").href,
        index: i,
    });
});

const searchBar = document.querySelector("#searchBar");
searchText.onkeyup = () => {
    nameList = [];
    if (searchText.value != "") {
        const inputText = searchText.value.toLowerCase();
        suggestions = list.filter((tag) =>
            tag.name.toLowerCase().includes(inputText)
        );
    } else {
        suggestions = [];
        displaySuggestions(suggestions);
    }
    displaySuggestions(suggestions);
};
suggestionsList.addEventListener("click", function (event) {
    const selectedTag = event.target.innerText;
    searchText.value = selectedTag;
    suggestionsList.innerHTML = ""; // Clear suggestions
    search();
});

function search() {
    if (searchText.value !== "") {
        list.forEach((e, i) => {
            if (!e.name.includes(searchText.value)) {
                books[e.index].style = "display:none";
            }
        });
    } else {
        suggestions = [];
        displaySuggestions(suggestions);
    }
}
searchBtn.addEventListener("click", function () {
    if (searchText.value !== "") {
        list.forEach((e, i) => {
            if (!e.name.includes(searchText.value)) {
                books[e.index].style = "display:none";
            }
        });
    }
    searchBox.classList.add("active");
    closeBtn.classList.add("active");
    searchBtn.classList.add("hide");
    displaySuggestions(suggestions);
    console.log(searchBox)
});
function displaySuggestions(suggestions, list) {
    suggestionsList.innerHTML = "";

    suggestions.forEach(function (suggestion) {
        const li = document.createElement("li");
        li.innerText = suggestion.name;
        const a = document.createElement("a");
        a.appendChild(li);
        a.href = suggestion.url;
        suggestionsList.appendChild(a);
    });
}
