let canClick = true;
const list = document.querySelector('.list');
const items = document.querySelectorAll('.item');
const dots = document.querySelectorAll('.dots li');
const prev = document.querySelector('#prev');
const next = document.querySelector('#next');
let active = 0;
let lengthItems = items.length - 1;
let refreshSlider;

const overall_btn = document.querySelector(".overall_details");
const detailed_btn = document.querySelector(".detailed_details");
const detailed_view = document.querySelector(".employee_detailed_data");
const overall_view = document.querySelector(".employee_overall_data");

window.onload = () => {
    if (next != null && prev != null && dots != null) {
        AddButtonsListeners();      
        AddDotsListeners();
        AddSlidesEventListeners();
        refreshSlider = setInterval(() => { next.click() }, 5000);
    }
    if (overall_btn != null && detailed_btn != null) {
        AddProfileButtonsListeners();
    }
    
    AddMenuClickListener();
}
function AddProfileButtonsListeners() {
    overall_btn.onclick = () => {
        if (!overall_btn.classList.contains("border_add")) {
            detailed_btn.classList.remove("border_add");
            detailed_view.classList.add("data_hidden");

            overall_btn.classList.add("border_add");
            overall_view.classList.remove("data_hidden");
        }   
    }
    detailed_btn.onclick = () => {
        if (!detailed_btn.classList.contains("border_add")) {
            overall_view.classList.add("data_hidden");
            overall_btn.classList.remove("border_add");

            detailed_btn.classList.add("border_add");
            detailed_view.classList.remove("data_hidden");
        }

    }
}
function AddMenuClickListener(){
    const button = document.querySelector(".button_menu");
    button.onclick=()=> {
        const content = document.querySelector(".menu_content");
        console.log(content);
        if (content.classList.contains('active')) {
            content.classList.remove('active');
        }
        else {
            content.classList.add('active');
        }
    }
}
function Scroll2Element(element) {
    const scrollToElement = document.querySelector(element);

    if (scrollToElement) {
        scrollToElement.scrollIntoView({
            behavior: 'smooth'     
        });
    }
}
function AddSlidesEventListeners() {
    const slide1 = document.querySelector("#slide1");
    const slide2 = document.querySelector("#slide2");
    const slide3 = document.querySelector("#slide3");

    slide1.onclick = () => {
        Scroll2Element(".main_news");
    }
    slide2.onclick = () => {
        Scroll2Element("#author_meetings");
    }
    slide3.onclick = () => {
        Scroll2Element(".inspirations");
    }
    
}
function AddDotsListeners() {
    dots.forEach((li, key) => {
        li.addEventListener('click', function () {
            active = key;
            reloadSlider();
        })
    });
}
function AddButtonsListeners(){
    next.onclick = () => HandleButtonClick('next');
    prev.onclick = () => HandleButtonClick('prev');
}
function HandleButtonClick(direction) {
    if (!canClick) {
        return;
    }

    if (direction === 'next') {
        if (active + 1 > lengthItems) {
            active = 0;
        } else {
            active += 1;
        }
    } else if (direction === 'prev') {
        if (active - 1 < 0) {
            active = lengthItems - 1;
        } else {
            active = active - 1;
        }
    }

    reloadSlider();
    canClick = false;

    setTimeout(() => {
        canClick = true;
    }, 1000);
}

function reloadSlider() {
    let checkLeft = items[active].offsetLeft;
    list.style.left = -checkLeft + 'px';
    if (active === 1) {
        list.style.transition = "1s";
    }    
    if (active === lengthItems) {
        setTimeout(() => {
            list.style.transition = "0s";
            list.style.left = "0px";
        }, 1000);
        active = 0;
    }

    let lastActiveDot = document.querySelector('.dot_active');
    lastActiveDot.classList.remove('dot_active');
    dots[active].classList.add('dot_active');
    clearInterval(refreshSlider);
    refreshSlider = setInterval(() => { next.click() }, 5000);
}
