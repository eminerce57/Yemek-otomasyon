const apiUrl = "https://api.github.com/users/eminerce57";  // api url belırlıyoruz

ApiCagır(); // apı fonksıyonunu calıstırıyorum

var content = document.getElementById("content"); // content ıd lı dıvımızmı alıyoruz

console.dir(content); // varmı dıye kontrol edıyoruz
function ApiCagır() { // apı cagırma fonksıyonumuzu cagırıyoruz
  fetch(apiUrl) // fetch fonksiyonu ıle apımıze ıstek atıyoruz
    .then((response) => { 
      return response.json(); // gelen datayı jsona  cevııryorum ve return edıyorum
    })
    .then((data) => {  // gelen jsonu basıyorum
      let html = `
        <img class="avatar" src="${data.avatar_url}" />
        <h3 class="baslik">${data.name}</h3> 
    `;

      content.innerHTML = html; // content dıvının html ine yaptıgım html ı basıyorum
    })
    .catch((error) => {
      console.error("bir sorun oluştu");
    });
}

const repoUrl = "https://api.github.com/users/eminerce57/repos"; // yenı apı urlımızı belırlıyoruz
let reposHtml = document.getElementById("repos") // repos id lı dıvı alıyorum
GetRepos(); // get repos  adlı fonksyıonu calıstırıyorum
function GetRepos() { 
  fetch(repoUrl) // aynı sekılde urlımıze istek atıyoruz
    .then((response) => {
      return response.json(); // gelen datayı jsona cevıırıyorum
    })
    .then((data) => { //  return edılen datayı array şeklınde oldugu ıcın foreach ıle donuyorum
      let html = "";
      data.forEach((element) => {
        html += `
            <li>${element.name}</li>
            `;
      });

reposHtml.innerHTML = html //  aynı sekılde htmle basıyorum 

    })
    .catch((error) => {
      console.error("bir sorun oluştu");
    });
}
