﻿$(document).ready(() => {
    let currentPageInfo = getCurrentPageInfo();
    loadReports(currentPageInfo.pageNumber, currentPageInfo.pageSize);
    $(`#pagesize-select option[value=${currentPageInfo.pageSize}]`).attr('selected', true);
    $('#input-search').parent().remove();
    $('#pagesize-select').change(function () {
        let selectedValue = $(this).val();
        let pageSize = parseInt(selectedValue);
        let pageInfo = getCurrentPageInfo();
        moveToPage(pageInfo.pageNumber, pageSize);
    });
});

function loadReports(pageNumber, pageSize) {
    let animationLoader = new AnimationLoader('#loading-container > #animation-container', '/assets/shop-owner/img/illustrations/loading.json');
    animationLoader.showAnimation(3500);
    getReports(pageNumber, pageSize).then((paginatedData) => {
        if (paginatedData.pageNumber > paginatedData.maxPageNumber) {
            moveToPage(1, paginatedData.pageSize);
        }
        onLoadReportsCompleted(paginatedData);
        animationLoader.hideAnimation();
    }).catch(() => {
        animationLoader.hideAnimation();
        toastr.error('Failed to load list of reports', 'Error');
    });
}

function onLoadReportsCompleted(paginatedData) {
    let reports = paginatedData.data;
    renderReportTable(reports, paginatedData.pageNumber, paginatedData.pageSize);
    renderPagination({
        hasPreviousPage: paginatedData.hasPreviousPage,
        hasNextPage: paginatedData.hasNextPage,
        pageNumber: paginatedData.pageNumber,
        maxPageNumber: paginatedData.maxPageNumber
    });
    $('#previous-page').click((e) => {
        e.preventDefault();
        let currentPageInfo = getCurrentPageInfo();
        moveToPage(currentPageInfo.pageNumber - 1, currentPageInfo.pageSize);
    });
    $('#next-page').click((e) => {
        e.preventDefault();
        let currentPageInfo = getCurrentPageInfo();
        moveToPage(currentPageInfo.pageNumber + 1, currentPageInfo.pageSize);
    });
    $('a.pagination-item').click(function (e) {
        e.preventDefault();
        let pageNumber = $(this).text();
        let currentPageInfo = getCurrentPageInfo();
        moveToPage(pageNumber, currentPageInfo.pageSize);
    });
}

function moveToPage(pageNumber, pageSize) {
    window.location.href = `https://cap-k24-team13.herokuapp.com/admin/report/index?pageNumber=${pageNumber}&pageSize=${pageSize}`;
}

function getCurrentPageInfo() {
    let url = new URL(window.location.href);
    let queryObj = url.searchParams;
    let currentPageNumber = queryObj.get('pageNumber') ? parseInt(queryObj.get('pageNumber')) : 1;
    let currentPageSize = queryObj.get('pageSize') ? parseInt(queryObj.get('pageSize')) : 5;
    return {
        pageNumber: currentPageNumber,
        pageSize: currentPageSize
    };
}
