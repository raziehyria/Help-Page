// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function updateBreadcrumb() {
    var breadcrumb = document.getElementById("breadcrumb");
    var path = window.location.pathname.split('/').filter(Boolean); // Get the current path segments
    var breadcrumbHTML = ' '; // Initial breadcrumb for the home page

    for (var i = 0; i < path.length; i++) {
        breadcrumbHTML += '<span class="separator">></span>';
        breadcrumbHTML += '<a href="/ ' + path.slice(0, i + 1).join('/') + '  ' + '" >' + '  ' + path[i] + '</a>';
    }

    breadcrumb.innerHTML = breadcrumbHTML;
}

$(document).ready(function () {
    var currentPath = window.location.pathname;
    var homePagePath = '/'; // Update with the actual home page path

    if (currentPath !== homePagePath) {
        $('.breadcrumb').show();
    }
});

// Call the updateBreadcrumb function when the page loads
window.onload = updateBreadcrumb;