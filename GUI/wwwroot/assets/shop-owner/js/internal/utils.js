﻿function renderProductTable(products, pageNumber, pageSize) {
    if (products.length == 0) {
        $('.table-responsive.p-0').html('<p style="text-align: center">There is no product to show!</p>');
    } else {
        $('.table-responsive.p-0').html(buildProductTableHtml(products, pageNumber, pageSize));
    }
}

function renderInvoiceTable(invoices, pageNumber, pageSize) {
    if (invoices.length == 0) {
        $('.table-responsive.p-0').html('<p style="text-align: center">There is no invoice to show!</p>');
    } else {
        $('.table-responsive.p-0').html(buildInvoiceTableHtml(invoices, pageNumber, pageSize));
    }
}

function renderReportTable(reports, pageNumber, pageSize) {
    if (reports.length == 0) {
        $('.table-responsive.p-0').html('<p style="text-align: center">There is no report to show!</p>');
    } else {
        $('.table-responsive.p-0').html(buildReportTableHtml(reports, pageNumber, pageSize));
    }
}

function renderCustomerTable(customers, pageNumber, pageSize) {
    if (customers.length == 0) {
        $('.table-responsive.p-0').html('<p style="text-align: center">There is no customer to show!</p>');
    } else {
        $('.table-responsive.p-0').html(buildCustomerTableHtml(customers, pageNumber, pageSize));
    }
}

function buildProductTableHtml(products, pageNumber, pageSize) {
    let tableRowHtml = '';
    products.forEach((product, index) => {
        tableRowHtml += buildProductTableRowHtml(product, index, pageNumber, pageSize);
    });
    return `<table class="table align-items-center mb-0">
                <thead>
                    <tr>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            #
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2"
                            style="padding-left: 24px!important">
                            Name
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Quantity
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Price
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Status
                        </th>
                        <th class="text-secondary opacity-7">Action</th>
                    </tr>
                </thead>
                <tbody>
                    ${tableRowHtml}
                </tbody>
            </table>`;
}

function buildInvoiceTableHtml(invoices, pageNumber, pageSize) {
    let tableRowHtml = '';
    invoices.forEach((invoice, index) => {
        tableRowHtml += buildInvoiceTableRowHtml(invoice, index, pageNumber, pageSize);
    });
    return `<table class="table align-items-center mb-0">
                <thead>
                    <tr>
                        <th style="display: none"></th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            #
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2"
                            style="padding-left: 24px!important">
                            Invoice Code
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Phone Number
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Created At
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Status
                        </th>
                        <th class="text-secondary opacity-7">Action</th>
                    </tr>
                </thead>
                <tbody>
                    ${tableRowHtml}
                </tbody>
            </table>`;
}

function buildReportTableHtml(reports, pageNumber, pageSize) {
    let tableRowHtml = '';
    reports.forEach((report, index) => {
        tableRowHtml += buildReportTableRowHtml(report, index, pageNumber, pageSize);
    });
    return `<table class="table align-items-center mb-0">
                <thead>
                    <tr>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            #
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            Id
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2"
                            style="padding-left: 24px!important">
                            Reporter
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Affectee
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Created At
                        </th>
                    </tr>
                </thead>
                <tbody>
                    ${tableRowHtml}
                </tbody>
            </table>`;
}

function buildCustomerTableHtml(customers, pageNumber, pageSize) { 
    let tableRowHtml = '';
    customers.forEach((customer, index) => {
        tableRowHtml += buildCustomerTableRowHtml(customer, index, pageNumber, pageSize);
    });
    return `<table class="table align-items-center m b-0">
                <thead>
                    <tr>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            #
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            Id
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2"
                            style="padding-left: 24px!important">
                            Name
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Phone number
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Email
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Report Count
                        </th>
                        <th class="text-center text-uppercase text-secondary text-xxs font-weight-bolder opacity-7">
                            Status
                        </th>
                        <th class="text-secondary opacity-7">Action</th>
                    </tr>
                </thead>
                <tbody>
                    ${tableRowHtml}
                </tbody>
            </table>`;
}

function buildProductTableRowHtml(product, index, pageNumber, pageSize) {
    let baseIndex = (pageNumber - 1) * pageSize;
    return `<tr>
                <td style="display: none" id="prod-id">${product.id}</td>
                <td class="align-middle text-center" style="padding: 0;">${baseIndex + index + 1}</td>
                <td class="align-middle text-center">
                    <div class="d-flex px-2 py-1">
                        <div>
                            <img src="${getProductImageUrl(product.images[0])}" class="avatar avatar-sm me-3 border-radius-lg" alt="user1">
                        </div>
                        <div class="d-flex flex-column">
                            <h6 class="mb-0 text-sm">${product.productName}</h6>
                            <p class="text-xs text-secondary mb-0" style="text-align: left">${product.categoryName}</p>
                        </div>
                    </div>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${product.quantity}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${formatPrice(product.price)}đ</span>
                </td>
                <td class="align-middle text-center text-sm">
                    <span class="badge badge-sm bg-gradient-${product.isAvailable ? 'success' : 'secondary'}">
                        ${product.isAvailable ? 'Activated' : 'Deactivated'}
                    </span>
                </td>
                <td class="align-middle">
                    ${buildProductActionButtonHtml(product.isAvailable)}
                </td>
            </tr>`;
}

function buildInvoiceTableRowHtml(invoice, index, pageNumber, pageSize) {
    let baseIndex = (pageNumber - 1) * pageSize;
    let statusList = ['New', 'Confirmed', 'Shipper Received', 'Succeed', 'Canceled'];
    return `<tr>
                <td style="display: none">${invoice.invoiceId}</td>
                <td class="align-middle text-center" style="padding: 0;">${baseIndex + index + 1}</td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${invoice.invoiceCode}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${invoice.phoneNumber}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${formatDateTime(invoice.createdAt)}</span>
                </td>
                <td class="align-middle text-center text-sm">
                    ${statusList[invoice.status]}
                </td>
                <td class="align-middle">
                    <a href="/invoice/detail/${invoice.invoiceCode}" class="text-secondary font-weight-bold text-xs me-2" data-toggle="tooltip" target="_blank">
                        <i class="fas fa-check"></i>
                        <span> Detail</span>
                    </a>
                    ${buildInvoiceActionButtonHtml(invoice.isReported)}
                </td>
            </tr>`;
}

function buildReportTableRowHtml(report, index, pageNumber, pageSize) {
    let baseIndex = (pageNumber - 1) * pageSize;
    return `<tr>
                <td class="align-middle text-center" style="padding: 0;">${baseIndex + index + 1}</td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${report.id}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${report.reporter}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${report.affectedUser}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${formatDateTime(report.createdAt)}</span>
                </td>
            </tr>`;
}

function buildCustomerTableRowHtml(customer, index, pageNumber, pageSize) {
    let baseIndex = (pageNumber - 1) * pageSize;
    return `<tr>
                <td class="align-middle text-center" style="padding: 0;">${baseIndex + index + 1}</td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${customer.id}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${customer.fullName}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${customer.phoneNumber}</span>
                </td>
                <td class="align-middle text-center">
                    <span class="text-secondary text-xs font-weight-bold">${customer.email}</span>
                </td>
                <td class="align-middle text-center">${customer.reportCount}</td>
                <td class="align-middle text-center text-sm">
                    ${customer.isLockedOut ? 'Locked out' : (customer.isAvailable ? (!customer.isConfirmed ? 'Unavailable' : 'Available') : 'Banned')}
                </td>
                <td class="align-middle">
                    ${buildCustomerActionButtonHtml(customer)}
                </td>
            </tr>`;
}

function formatDateTime(dateStr) {
    let dateObj = new Date(dateStr);
    let day = dateObj.getDate().toString().padStart(2, '0');
    let month = dateObj.getMonth().toString().padStart(2, '0');
    let year = dateObj.getFullYear();
    let hour = dateObj.getHours().toString().padStart(2, '0');
    let minute = (dateObj.getMinutes() + 1).toString().padStart(2, '0');
    let second = dateObj.getSeconds().toString().padStart(2, '0');
    return `${day}/${month}/${year} ${hour}:${minute}:${second}`;
}

function buildProductActionButtonHtml(isAvailable) {
    if (isAvailable)
        return `
                ${buildEditButtonHtml()}
                <a href="#" class="text-secondary font-weight-bold text-xs" data-toggle="tooltip"
                    name="btn-action" style="margin-right: 24px">
                    <i class="far fa-trash-alt"></i>
                    <span> Deactivate</span>
                </a>
                ${buildImportQuantityButtonHtml()}`;
    else
        return `<a href="#" class="text-secondary font-weight-bold text-xs" data-toggle="tooltip"
                    name="btn-action" style="margin-right: 24px">
                    <i class="fas fa-check"></i>
                    <span> Activate</span>
                </a>`;
}

function buildCategoryActionButtonHtml() {
    return `<a href="#" class="text-secondary font-weight-bold text-xs" data-toggle="tooltip"
                    name="btn-action">
                    <i class="fas fa-check"></i>
                    <span> Import</span>
                </a>`;
}

function buildInvoiceActionButtonHtml(isReported) {
    if (!isReported)
        return `<a class="text-secondary font-weight-bold text-xs" data-toggle="tooltip" name="btn-action" style="cursor: pointer">
                    <i class="fas fa-check"></i>
                    <span> Report</span>
                </a>`;
    return '';
}

function buildReportActionButtonHtml(isApproved) {
    if (!isApproved)
        return `<a class="text-secondary font-weight-bold text-xs" data-toggle="tooltip" name="btn-action" style="cursor: pointer">
                    <i class="fas fa-check"></i>
                    <span> Approve</span>
                </a>`;
    return '';
}

function buildCustomerActionButtonHtml(customer) {
    if (customer.isLockedOut)
        return '';
    if (!customer.isAvailable)
        return `<a class="text-secondary font-weight-bold text-xs" data-toggle="tooltip" name="btn-action" style="cursor: pointer">
                    <i class="fas fa-check"></i>
                    <span> Unban</span>
                </a>`;
    else
        return `<a class="text-secondary text-xs ${!customer.isConfirmed ? 'disabled' : 'font-weight-bold'}" data-toggle="tooltip" name="btn-action" style="${customer.isConfirmed ? 'cursor: pointer' : 'text-decoration: none; cursor: default'}">
                    <i class="fas fa-check"></i>
                    <span> Ban</span>
                </a>`;
}

function buildEditButtonHtml() {
    return `<a href="#" class="text-secondary font-weight-bold text-xs"
                data-toggle="tooltip" data-original-title="Edit user" style="margin-right: 24px" name="btn-edit">
                <i class="far fa-edit"></i><span> Edit</span>
            </a>`;
}

function buildImportQuantityButtonHtml() {
    return `<a href="#" class="text-secondary font-weight-bold text-xs" data-toggle="tooltip"
                name="btn-import-quantity">
                <i class="fas fa-pencil-alt"></i>
                <span> Import quantity</span>
            </a>`;
}

function renderPagination(paginationObject) {
    let paginationHtml = '';
    if (paginationObject.hasPreviousPage)
        paginationHtml += '<a href="#" id="previous-page">«</a>';
    for (let i = 1; i <= paginationObject.maxPageNumber; i++) {
        if (i == paginationObject.pageNumber)
            paginationHtml += `<a class="active">${i}</a>`;
        else
            paginationHtml += `<a href="#" class="pagination-item">${i}</a>`;
    }
    if (paginationObject.hasNextPage)
        paginationHtml += '<a href="#" id="next-page">»</a>';
    $('.pagination').html(paginationHtml);
}

function renderCategoryDropdown(categories) {
    let optionsHtml = '';
    categories.forEach(category => {
        optionsHtml += `<option value="${category.category_Id}">${category.category_Name}</option>`;
    });
    if (optionsHtml != '') {
        $('option[selected=selected]').after(optionsHtml);
    }
}

function renderCategoryTable(categories, pageNumber, pageSize) {
    if (categories.length == 0) {
        $('.table-responsive.p-0').html('<p style="text-align: center">There is no category to show!</p>');
    } else {
        $('.table-responsive.p-0').html(buildCategoryTableHtml(categories, pageNumber, pageSize));
    }
}

function buildCategoryTableHtml(categories, pageNumber, pageSize) {
    let tableRowHtml = '';
    categories.forEach((category, index) => {
        tableRowHtml += buildCategoryTableRowHtml(category, index, pageNumber, pageSize);
    });
    return `<table class="table align-items-center mb-0">
                <thead>
                    <tr>
                        <th style="display: none">Id</th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7"
                            style="width: 50px; min-width: 50px !important;">
                            #
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2 text-center">
                            Category name
                        </th>
                        <th class="text-uppercase text-secondary text-xxs font-weight-bolder opacity-7 ps-2 text-center">
                            Product count
                        </th>
                        <th class="text-secondary opacity-7">Action</th>
                    </tr>
                </thead>
                <tbody>
                    ${tableRowHtml}
                </tbody>
            </table>`;
}

function buildCategoryTableRowHtml(category, index, pageNumber, pageSize) {
    let baseIndex = (pageNumber - 1) * pageSize;
    return `<tr>
                <td style="display: none">${category.categoryId}</td>
                <td class="align-middle text-center">${baseIndex + index + 1}</td>
                <td class="align-middle text-center text-sm">
                    ${category.categoryName}
                </td>
                <td class="align-middle text-center">
                    ${category.productCount}
                </td>
                <td class="align-middle">
                    ${buildCategoryActionButtonHtml()}
                </td>
            </tr>`;
}

function sortList(field, direction, dataList) {
    if (!Array.isArray(dataList))
        return dataList;
    if (!dataList.every(element => element.hasOwnProperty(field)))
        return dataList;
    if (typeof (direction) !== 'string' || (direction !== 'ASC' && direction !== 'DESC'))
        return dataList;

    // sort
    return [...dataList].sort((first, second) => {
        let firstValue = first[field];
        let secondValue = second[field];

        if (firstValue === secondValue || typeof (firstValue) !== typeof (secondValue) || firstValue === null || secondValue === null)
            return 0;

        if (direction === 'ASC') {
            if (typeof (firstValue) === 'number')
                return firstValue - secondValue;
            else if (typeof (firstValue) === 'string')
                return firstValue.toLowerCase().localeCompare(secondValue.toLowerCase());
            else if (typeof (firstValue) === 'boolean')
                return firstValue ? 1 : -1;
            else
                return 0;
        } else {
            if (typeof (firstValue) === 'number')
                return secondValue - firstValue;
            else if (typeof (firstValue) === 'string')
                return secondValue.toLowerCase().localeCompare(firstValue.toLowerCase());
            else if (typeof (firstValue) === 'boolean')
                return secondValue ? 1 : -1;
            else
                return 0;
        }
    });
}

function clearTable() {
    $('.table-responsive.p-0').html('');
}

function displayCascadeQuestionDialog(question, buttonOption = {}, confirmedCallback) {
    if (!buttonOption.cascadeButtonText)
        buttonOption.cascadeButtonText = 'Yes and cascade';
    if (!buttonOption.nonCascadeButtonText)
        buttonOption.nonCascadeButtonText = "Non cascade";
    if (!buttonOption.cancelButtonText)
        buttonOption.cancelButtonText = 'Cancel';
    if (buttonOption.shouldShowCascadeButton === null || buttonOption.shouldShowCascadeButton === undefined)
        buttonOption.shouldShowCascadeButton = false;
    if (buttonOption.shouldShowNonCascadeButton === null || buttonOption.shouldShowNonCascadeButton === undefined)
        buttonOption.shouldShowNonCascadeButton = false;
    if (!question)
        throw new Error('question must have a value');
    if (!typeof confirmedCallback === 'function')
        throw new Error('confirmedCallback must be a function');
    var modalHtml = `<div class="modal fade" id="question-modal" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Question</h5>
                                    </div>
                                    <div class="modal-body">
                                        ${question}
                                    </div>
                                    <div class="modal-footer">
                                        ${buttonOption.shouldShowCascadeButton ?
                                            `<button type="button"
                                                class="btn bg-gradient-primary-dark my-shadow text-white"
                                                data-action="cascade">
                                                ${buttonOption.cascadeButtonText}
                                            </button>` : ''}
                                        ${buttonOption.shouldShowNonCascadeButton ?
                                            `<button type="button"
                                                class="btn bg-gradient-primary-dark my-shadow text-white"
                                                data-action="non-cascade">
                                                ${buttonOption.nonCascadeButtonText}
                                            </button>` : ''}
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"
                                            data-action="cancel">
                                            ${buttonOption.cancelButtonText}
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>`;
    $('body').append(modalHtml);
    $('#question-modal').modal({
        backdrop: 'static',
        keyboard: false
    }).modal('show');
    $('#question-modal').on('question-answered', function (_, source) {
        confirmedCallback(source.attr('data-action'));
    });
    $('#question-modal > .modal-dialog > .modal-content > .modal-footer > button:not(:last-child)')
        .click(function () {
            $('#question-modal').modal('hide');
            $('#question-modal').trigger('question-answered', [$(this)]);
        });
    $('#question-modal').on('hidden.bs.modal', function () {
        $(this).modal('dispose');
        $(this).remove();
    });
}

function displayYesNoQuestion(question, confirmCallback) {
    displayCascadeQuestionDialog(question, {
        shouldShowCascadeButton: false,
        shouldShowNonCascadeButton: true,
        nonCascadeButtonText: 'Yes',
        cancelButtonText: 'No'
    }, confirmCallback);
}

function displayImportQuantityDialog(product, saveCallback) {
    var modalHtml = `<div class="modal fade" id="quantity-modal" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Edit quantity</h5>
                                </div>
                                <div class="modal-body">
                                    <form class="col-12">
                                        <div class="form-row mb-3">
                                            <label class="col-sm-6 align-items-center col-form-label" for="product-name">Product Name</label>
                                            <input type="text" class="form-control col-sm-8" name="Product-name" id="product-name" disabled value="${product.productName}" />
                                        </div>
                                        <div class="form-row mb-3">
                                            <label class="col-sm-6 align-items-center col-form-label" for="current-product-quantity">Current Product Quantity</label>
                                            <input type="number" class="form-control col-sm-8" name="current-product-quantity" id="current-product-quantity" disabled value="${product.quantity}" />
                                        </div>
                                        <div class="form-row mb-3">
                                            <label class="col-sm-6 align-items-center col-form-label" for="numer-of-products-added">Number of Products Added</label>
                                            <div class="col-sm-6 mb-2">
                                                <div class="input-group">
                                                    <span class="input-group-btn">
                                                        <button type="button" class="quantity-left-minus quantity btn btn-danger btn-number" data-type="minus" data-field="" style="margin-bottom: 0">
                                                            <span class="glyphicon glyphicon-minus">-</span>
                                                        </button>
                                                    </span>
                                                    <input type="number" id="quantity" name="quantity" class="form-control input-number" min="1" value="1" style="margin-bottom: 0">
                                                    <span class="input-group-btn">
                                                        <button type="button" class="quantity-right-plus quantity btn btn-success btn-number" data-type="plus" data-field="" style="margin-bottom: 0">
                                                            <span class="glyphicon glyphicon-plus">+</span>
                                                        </button>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-row mb-3">
                                            <label class="col-sm-6 align-items-center col-form-label" for="total-product-quantity">Total Amount After Adding</label>
                                            <input type="number" class="form-control col-sm-8" name="total-product-quantity" id="total-product-quantity" value="${product.quantity + 1}" disabled />
                                        </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn bg-gradient-primary-dark my-shadow text-white"
                                            data-action="save">
                                        Save
                                    </button>
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" data-action="cancel">
                                        Cancel
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>`;
    $('body').append(modalHtml);
    $('#quantity-modal').modal({
        backdrop: 'static',
        keyboard: false
    }).modal('show');
    let oldQuantity = parseInt($('#current-product-quantity').val());
    $('#quantity').keypress(function (e) {
        let charCode = (e.which) ? e.which : e.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    });
    $('.quantity-right-plus').click(function (e) {
        e.preventDefault();
        let quantity = parseInt($('#quantity').val());
        $('#quantity').val(quantity + 1);
        $('#quantity').change();
    });
    $('.quantity-left-minus').click(function (e) {
        e.preventDefault();
        let quantity = parseInt($('#quantity').val());
        if (!quantity)
            quantity = 1;
        if (quantity > 1) {
            $('#quantity').val(quantity - 1);
            $('#quantity').change();
        }
    });
    $('#quantity').change(function () {
        let value = $(this).val();
        let quantity = parseInt(value);
        if (quantity == 0) {
            quantity = 1;
            $('#quantity').val(1);
        }
        $('#total-product-quantity').val(oldQuantity + quantity);
    });
    $('#quantity-modal div.modal-footer > button:first-child').click(function () {
        $('#quantity-modal').modal('hide');
        let importedQuantity = parseInt($('#quantity').val());
        saveCallback(importedQuantity);
    });
    $('#quantity-modal').on('hidden.bs.modal', function () {
        $(this).modal('dispose');
        $(this).remove();
    });
}

function displayBanCustomerDialog(confirmedCallback) {
    var modalHtml = `<div class="modal fade" id="ban-customer-modal" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Ban Options</h5>
                                </div>
                                <div class="modal-body">
                                    <form class="col-12">
                                        <div class="form-row mb-3">
                                            <label class="col-sm-6 align-items-center col-form-label">Permanently Ban</label>
                                            <input type="radio" name="tab" id="permanently-ban" checked />
                                        </div>
                                        <div class="form-row mb-3">
                                            <label class="col-sm-6 align-items-center col-form-label">Ban depend on the date</label>
                                            <input type="radio" name="tab" id="ban-depend-on-date" />
                                            <div class="hide" style="display: none">
                                                <hr>
                                                <p>Choose option</p>
                                                <button class="btn btn-primary change-day-count-button">3 days</button>
                                                <button class="btn btn-primary change-day-count-button">7 days</button>
                                                <button class="btn btn-primary change-day-count-button">15 days</button>
                                                <button class="btn btn-primary change-day-count-button">30 days</button>
                                                <br>
                                                <input style="margin-top:5px; text-align: center" type="number" name="input-day-count" value="3">
                                            </div>
                                        </div>
                                        <div class="form-row mb-3">
                                            <label class="col-sm-12 align-items-center col-form-label">Ban message</label>
                                            <input class="col-md-12" type="text" name="ban-message" id="ban-message" />
                                        </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn bg-gradient-primary-dark my-shadow text-white"
                                            data-action="save">
                                        Confirm
                                    </button>
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" data-action="cancel">
                                        Cancel
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>`;
    $('body').append(modalHtml);
    $('#ban-customer-modal').modal({
        backdrop: 'static',
        keyboard: false
    }).modal('show');
    $('#ban-customer-modal').on('hidden.bs.modal', function () {
        $(this).modal('dispose');
        $(this).remove();
    });
    $('#permanently-ban').click(() => {
        $('div.hide').css('display', 'none');
    });
    $('#ban-depend-on-date').click(() => {
        $('div.hide').css('display', 'block');
    });
    $('button.btn.change-day-count-button').click(function (e) {
        e.preventDefault();
        $('input[name=input-day-count]').val($(this).text().split(' ')[0]);
    });
    $('#ban-customer-modal div.modal-footer > button:first-child').click(function () {
        $('#ban-customer-modal').modal('hide');
        let dayCount = $('#permanently-ban')[0].checked ? null : $('input[name=input-day-count]').val();
        let message = $('#ban-message').val();
        confirmedCallback(dayCount, message);
    });
}

function formatPrice(price) {
    return new Intl.NumberFormat('en-US', {
        maximumFractionDigits: 3
    }).format(price);
}

function unformatPrice(formattedPrice) {
    return parseFloat(formattedPrice.replace(/,/g, ''));
}

function buildInvoiceItem(invoice) {
    let paymentMethods = ['CoD', 'MoMo', 'VISA'];
    return `<div class="board-item ${invoice.isPaid ? '' : 'unpaid'}">
                <div class="board-item-content ${invoice.isPaid ? '' : 'unpaid'}">
                    <div id="order-id">
                        <input type="hidden" value="${invoice.invoiceId}" />
                        <h4>#${invoice.invoiceCode}</h4>
                        <h5>${invoice.receiverName}</h5>
                        <input type="tel" name="phonenumber" placeholder="phonenumber" value="${invoice.phoneNumber}" disabled />
                        <input type="text" name="address" placeholder="address" value="${invoice.shippingAddress}" disabled />
                        <h5>${paymentMethods[invoice.paymentMethod]}</h5>
                    </div>
                    <button id="btn-order-details" type="button" class="btn btn-primary">
                        View
                    </button>
                </div>
            </div>`;
}

function buildRelatedProductItem(product) {
    return `<div class="product product-sm">
                <figure class="product-media">
                    <a href="/product/index/${product.id}">
                        <img src="${getProductImageUrl(product.images[0])}" alt="Product image" class="product-image">
                    </a>
                </figure>
                <div class="product-body">
                    <h5 class="product-title">
                        <a href="/product/index/${product.id}">${product.productName}</a>\
                    </h5><!-- End .product-title -->
                    <div class="product-price">
                        <span class="new-price">${formatPrice(product.price)}</span>
                    </div>
                </div>
            </div>`;
}