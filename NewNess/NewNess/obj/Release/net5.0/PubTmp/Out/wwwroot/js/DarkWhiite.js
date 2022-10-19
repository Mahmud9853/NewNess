const toggle = document.getElementById('toggleDark'); 
const body = document.querySelector('.active');
/*var line = document.getElementsByClassName("product-item ");*/
/*const line = document.querySelector('.news');*/
toggle.addEventListener('click', function () {
    this.classList.toggle('bi-moon');
    if (this.classList.toggle('bi-brightness-high-fill')) {
        body.style.background = 'white';
        body.style.color = 'black';
        body.style.transition = '2s';
      /*  document.getElementsByClassName("product-item ").style.border = 'none';*/
     
    } else {
    /*    line.style.border = '1px solid yellow ';*/
        body.style.background = 'black';
        body.style.color = 'yellow';
        body.style.transition = '2s';
    }
});

