$(document).ready(function () {
    var companyDropDown = document.getElementById('CompanyId');
    var companyId = companyDropDown.options[companyDropDown.selectedIndex].value;
    let employeeDropDown = $('#EmployeeId');
    employeeDropDown.empty();
    $.ajax({
        type: "POST",
        url: "/Admin/Tasks/GetEmployeesByCompanyId?id=".concat(companyId),
        success: function (response) {
            $.each(response, function (key, value) {
                employeeDropDown.append($('<option></option>').attr('value', key).text(value));
            })
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
    $('#CompanyId').on('change', function () {

        employeeDropDown.empty();
        $.ajax({
            type: "POST",
            url: "/Admin/Tasks/GetEmployeesByCompanyId?id=".concat(this.value),
            success: function (response) {
                $.each(response, function (key, value) {
                    employeeDropDown.append($('<option></option>').attr('value', key).text(value));
                })
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });
});