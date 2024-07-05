const cartProducts = document.querySelectorAll(".product");
const total = document.querySelector(".total");
const all = document.querySelector(".all");
const quantities = document.querySelectorAll(".quantity");

var totalPrice = 0;
function calc() {
    var Price = 0;
    cartProducts.forEach((p, i) => {
        Price +=
            +p.querySelector(".price").innerHTML.split(" ")[0] *
            +quantities[i].value;
    });
    totalPrice = Price;
    total.innerHTML = `${totalPrice} EGP`;
    all.innerHTML = `${totalPrice + 30} EGP`;
}
calc();
quantities.forEach((q) => {
    q.onchange = (v) => {
        if (v.target.value > 0) {
            calc();
        } else {
            q.value = 1;
        }
    };
});
total.innerHTML = `${totalPrice} EGP`;
all.innerHTML = `${totalPrice + 30} EGP`;
