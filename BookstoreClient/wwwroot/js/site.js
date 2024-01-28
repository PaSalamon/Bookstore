// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setUrlParam(variableName, value) {
    let currentUrl = new URL(window.location.href);
    currentUrl.searchParams.set(variableName, value);
    history.replaceState(null, null, currentUrl);
}

function filterButtonHandler(event) {
    event.preventDefault();
    const genreName = event.target.querySelector('input').value;

    setUrlParam('genre', genreName);

    const genreNameLowerCase = genreName.toLowerCase();
    const rows = document.querySelectorAll('.books-table tbody tr');
    rows.forEach(row => {
        const rowGenreName = row.querySelector('.genre-name').textContent.toLowerCase();
        if (rowGenreName.includes(genreNameLowerCase)) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

function sortByPrice() {
    const sortParam = new URLSearchParams(window.location.search).get("sort");

    const rows = document.querySelectorAll('.books-table tbody tr');
    const sortedRows = Array.from(rows).sort((rowA, rowB) => {
        const priceA = parseFloat(rowA.querySelector('.price').textContent);
        const priceB = parseFloat(rowB.querySelector('.price').textContent);
        return sortParam === 'asc' ? priceA - priceB : priceB - priceA;
    });

    const tbody = document.querySelector('.books-table tbody');
    tbody.innerHTML = '';
    sortedRows.forEach(row => tbody.appendChild(row));
}

function sort() {
    const sortParam = new URLSearchParams(window.location.search).get("sort");

    if (!sortParam || sortParam === 'desc') {
        setUrlParam('sort', 'asc');
    } else {
        setUrlParam('sort', 'desc');
    }

    sortByPrice();
}

