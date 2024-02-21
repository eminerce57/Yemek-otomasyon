const apiUrl = "https://api.github.com/users/TalhacckM";

ApiCagır();

var content = document.getElementById("content");

console.dir(content);
function ApiCagır() {
  fetch(apiUrl)
    .then((response) => {
      return response.json();
    })
    .then((data) => {
      let html = `
        <img class="avatar" src="${data.avatar_url}" />
        <h3 class="baslik">${data.name}</h3> 
    `;

      content.innerHTML = html;
    })
    .catch((error) => {
      console.error("bir sorun oluştu");
    });
}

const repoUrl = "https://api.github.com/users/TalhacckM/repos";
let reposHtml = document.getElementById("repos")
GetRepos();
function GetRepos() {
  fetch(repoUrl)
    .then((response) => {
      return response.json();
    })
    .then((data) => {
      let html = "";
      data.forEach((element) => {
        html += `
            <li>${element.name}</li>
            `;
      });

reposHtml.innerHTML = html

    })
    .catch((error) => {
      console.error("bir sorun oluştu");
    });
}
