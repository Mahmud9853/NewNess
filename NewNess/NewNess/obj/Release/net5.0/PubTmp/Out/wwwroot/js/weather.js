const url = 'https://api.openweathermap.org/data/2.5/'
const key = '22db928abe3fa85e852c136dab7187a8'

const setQuery = (e) => {
    if (e.keyCode == '13')
        getResult(searchbar.value)
        
}

const getResult = (cityName) => {
    var query = `${url}weather?q=${cityName}&appid=${key}&units=metric&lang=az`
    fetch(query)
        .then(weather => {
            return weather.json()
        })
        .then(displayResult)
}
const displayResult = (result) => {

    var city = document.querySelector('.city')
    city.innerText = `${result.name},${result.sys.country}`

    var temp = document.querySelector('.temp')
    temp.innerText = `${Math.round(result.main.temp)}°C`

    var desc = document.querySelector('.desc')
    desc.innerText = result.weather[0].description

    var minmax = document.querySelector('.minmax')
    minmax.innerText = `${Math.round(result.main.temp_min)}°C / ${ Math.round(result.main.temp_max) } °C`
}
const searchbar = document.getElementById('searchbar')
searchbar.addEventListener('keypress', setQuery)




    var icon = document.getElementById('icon')
    icon.onclick = function () {
    document.body.classList.toggle(".active")
    if (document.body.classList.contains(".active")) {
        icon.class = "fa - solid fa - sun"
    } else {
        icon.class = "fa-solid fa-moon"

    }
    console.log();
}



