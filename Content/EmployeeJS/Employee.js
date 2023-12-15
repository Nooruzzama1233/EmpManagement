
$(document).ready(function () {

    DisplayData();

    $('#btnSave').click(function () {

        //SaveEmpData();
        checkValidate();
    });

});

function onlyAlphabetsDotsAndSpaces(event) {
    
    var keyCode = event.which || event.keyCode;
      
    if ((keyCode >= 65 && keyCode <= 90) ||       
        (keyCode >= 97 && keyCode <= 122) ||      
        keyCode === 46 || keyCode === 32) {       
        return true;
    } else {       
        event.preventDefault();
        document.getElementById('spnName').innerText = "Invalid Mobile No.";
        return false;
    }
}

function validateEmail() {
    const emailInput = document.getElementById('txtEmail');
    const email = emailInput.value;

    const re = /\S+@\S+\.\S+/;
    if (re.test(String(email).toLowerCase())) {
        //document.getElementById('spnEmail').innerText = "Valid email address";
    } else {
        document.getElementById('spnEmail').innerText = "Invalid email address";
    }
}

function onlyNumbersNDot(evt) {
    var evtobj = window.event ? event : evt;
    var charCode = evtobj.charCode ? evtobj.charCode : evtobj.keyCode;
    if ((charCode > 47 && charCode < 58) || charCode == 8 || charCode == 46 || charCode == 9 || charCode == 10 || charCode == 11)
        return true;
    else {
        document.getElementById('spnMobileNO').innerText = "Invalid Mobile No.";
        return false;
    }
}

function checkValidate() {

    if (($('#txtName').val() == "") && ($('#txtEmail').val() == "") && ($('#txtMobileNo').val()=="") ) {
        document.getElementById('spnName').innerText = "Name is required.";
        document.getElementById('spnMobileNO').innerText = "Invalid Mobile No.";
        document.getElementById('spnEmail').innerText = "Invalid email address";        
        return;
    }
    else if ($('#txtName').val() == "") {
        document.getElementById('spnName').innerText = "Name is required.";
        return;
    }
    else if ($('#txtEmail').val() == "") {
        document.getElementById('spnEmail').innerText = "Email is required.";
        return;
    }
    else if ($('#txtMobileNo').val() == "") {
        document.getElementById('spnMobileNO').innerText = "Mobile No. is required.";
        return;
    } else {
        hideErroeMsg();
        SaveEmpData();
    }
}

function hideErroeMsg() {
    document.getElementById('spnName').innerText = "";
    document.getElementById('spnMobileNO').innerText = "";
    document.getElementById('spnEmail').innerText = "";
}

function SaveEmpData() {

    var EmpId = document.getElementById('spnEmployeeId').innerHTML; //$('#spnEmployeeId').val();
    var EmpName = $('#txtName').val();
    var EmpEmail = $('#txtEmail').val();
    var EmpAddress = $('#txtAddress').val();
    var EmpPhone = $('#txtMobileNo').val();

    var EmpData = { EmployeeId: EmpId, EmployeeName: EmpName, EmployeeEmail: EmpEmail, EmployeeAddress: EmpAddress, EmployeePhone: EmpPhone };

    if (EmpId != null && EmpId.val > 0 ) {

        $.ajax({
            url: "/Employee/UpdateEmployee/",
            data: EmpData,
            type: "POST",
            success: function (Res) {
                alert(Res);               
            },
            error: function (Res) {
                alert("asa");
            }
        });

    }
    else {

        $.ajax({
            url: "/Employee/AddNewEmployee/",
            data: EmpData,
            type: "POST",
            success: function (Res) {
                alert(Res);
               
            },
            error: function (Res) {
                alert("asa");
            }
        });
    }

    clearField();
    DisplayData();    
}

function clearField() {
    $('#txtName').val("");
    $('#txtEmail').val("");
    $('#txtAddress').val("");
    $('#txtMobileNo').val("");
}

function DisplayData() {

    $.ajax({
        url: "/Employee/GetData",
        data: "",
        datatype: "JSON",
        success: function (data) {
            var tablerow = '';           
            $.each(data, function (key, item) {
                tablerow += "<tr>";
                tablerow += "<td> <input type='checkbox'/> </td>";
                //tablerow += "<td>" + item.EmployeeId + "</td>";
                tablerow += "<td>" + item.EmployeeName + "</td>";
                tablerow += "<td>" + item.EmployeeEmail + "</td>";
                tablerow += "<td>" + item.EmployeeAddress + "</td>";
                tablerow += "<td>" + item.EmployeePhone + "</td>";
                tablerow += "<td>" + '<a href="#" data-toggle="modal" data-target="#myModal" onclick=EditEmployee(' + item.EmployeeId + ')> Edit </a>' + ' | ' + '<a href="#" onclick=deleteEmpData(' + item.EmployeeId + ')>  Delete </a>' + "</td>";
                tablerow += "</tr>";

                $('#sonCurrentRecord').text(item.RowNumber);
                $('#sonTotalRecord').text(item.TotalRecord);
            });

            $('#listStdData').html(tablerow);
        },
        error: function () {
            alert("");
        }
    });

}

function EditEmployee(EmpData) {

    var EmpData = { EmployeeId: EmpData };

    $.ajax({
        url: "/Employee/getEmployeeById/",
        data: EmpData,
        type: "POST",
        success: function (Res) {

            $('#spnEmpId').text(" Employee Id -" + Res.EmployeeId);
            $('#spnEmployeeId').text(Res.EmployeeId);
            $('#txtName').val(Res.EmployeeName);
            $('#txtEmail').val(Res.EmployeeEmail);
            $('#txtAddress').val(Res.EmployeeAddress);
            $('#txtMobileNo').val(Res.EmployeePhone);
        },
        error: function (Res) {
            alert("asa");
        }
    });
}

function deleteEmpData(Sid) {

    var EmpData = { EmployeeId: Sid };

    alert('Are you sure you want to delete this record? \n\n This action cannot be undone.');

    $.ajax({
        url: "/Employee/DeleteEmployee/",
        data: EmpData,
        type: "POST",
        success: function (Res) {
            alert(Res);
        },
        error: function (Res) {
            alert("asa");
        }
    });
}