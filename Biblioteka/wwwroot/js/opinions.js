const stars = document.querySelectorAll(".add_opinion_star");
const stars_div = document.querySelector(".stars_div");
let opinion_added = false;
const user_rating = document.querySelector(".your_rating");

const other_comments_ratings = document.querySelectorAll(".user_rating");
const other_opinions_stars = document.querySelectorAll(".opinion_star");

AddHoverStarsListeners();
FillOtherCommentsStars();
AddClickStarListener();


function FillOtherCommentsStars() {
    if (other_comments_ratings != null && other_opinions_stars != null) {
        let index = 0;
        other_comments_ratings.forEach(rating => {
            for (let i = index; i < 5 + index; i++) {
                if (i%5 < parseFloat(rating.textContent)) {
                    other_opinions_stars[i].style.color = "orange";
                    
                }
                else {
                    other_opinions_stars[i].style.color = "black";
                }
            }
            index += 5;
        })
    }
}
function AddClickStarListener() {
    if (stars.length!=0) {
        stars.forEach(star => {
            star.onclick = () => {
                opinion_added = !opinion_added;
                for (let i = 0; i < stars.length; i++) {
                    stars[i].style.color = "orange";
                    if (stars[i] == star) {
                        if (opinion_added) {
                            console.log(user_rating);
                            user_rating.value = i + 1;
                        }
                        else {
                            user_rating.value = "";
                        }
                        console.log("clicked_star: ", i + 1);
                        break;
                    }
                }
            }
        });
    }   
}
function AddHoverStarsListeners() {
    if (stars.length != 0) {
        stars.forEach(star => {
            star.onmouseover = () => {
                if (!opinion_added) {
                    for (let i = 0; i < stars.length; i++) {
                        stars[i].style.color = "orange";
                        if (stars[i] == star) {
                            break;
                        }
                    }
                }
            }
            star.onmouseleave = () => {
                if (!opinion_added) {
                    star.style.color = "black";
                }
            }
        })
        stars_div.onmouseleave = () => {
            if (!opinion_added) {
                stars.forEach(star => {
                    star.style.color = "black";
                })
            }
        }          
    }       
}