

$(document).ready(function () {


    $(".addDept").on('click', function () {
        var T = { obj: 'Department', Name: $("#DeptInput").val() }
        Add(T); 
    });
    $(".addComp").on('click', function () {

        var T = { obj: 'Company', Name: $("#CompInput").val() }
        Add(T);
    });

});


function Delete(T) {
    $.ajax({
        url: `Delete${T.obj}/${T.btn.attr('data-id')}`,
        method: 'DELETE',
        contentType: 'application/json',
        success: function (result) {
            T.btn.parents('tr').remove();
        },
        error: function (request, msg, error) {

        }
    });
}

function Edit(T) {
    if (!T.Name || !T.id) {
        alert('please insert correct information');
        return;
    }
    var Obje = { ID: T.id, Name: T.Name }
    var ObjColumn = $(`td[data-obj='${T.type}'][data-id='${T.id}']`);

    $.ajax({
        type: "PUT",
        url: `Edit${T.type}/`,
        data: JSON.stringify(Obje),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (e) {
            ObjColumn.html(T.Name);
            ObjColumn.attr("data-Name", T.Name);
            alert('item has been edited successfully');

        }, error: function (er) {
            console.log(er.responseText);
        }
    });

}


function Add(T) {

    if (!T.Name)  {
        alert("please insert a name");
        return;
    }

    if (!confirm('are you sure you want to add a new ' + T.obj)) {
        return;
    }
    var Obje = {"ID":T.id, "Name": T.Name };
    $.ajax({
        type: "POST",
        url: `Add${T.obj}/`,
        data: JSON.stringify(Obje),
        dataType: "json",
        contentType: "application/json; charset=utf-8",

        success: function (response) {
            $(`#${T.obj}sTbody`).append(DeptCompTemplate(response));
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(`please insert the Name of the ${T.obj} in the correct form and check if it doesn't already exist`);
        }
    });
}

function DeptCompTemplate(obj) {
    return `
        <tr >
                    <td>${obj.name}</td>
                        <td>
                           <button class="btn btn-danger deleteDepartment" data-id="${obj.id}">delete</button>
                           </td>
                   </tr>

`;
}

$(document).on('click', '.Edit', function () {


    var ApplyBtn = $(this).closest(".modal-body").find(".ApplyEdit");
    var ApplyInput = $(this).closest(".modal-body").find("Input");
    ApplyInput.val($(this).attr('data-Name'));

    ApplyBtn.attr("data-id", $(this).attr('data-Id'));
});

$(document).on('click', '.ApplyEdit', function () {
    var ApplyInput = $(this).closest(".modal-body").find("Input");

    var Obj = { id: $(this).attr('data-id'), Name: ApplyInput.val(), type: $(this).attr('data-Obj')}
    Edit(Obj);

});

$(document).on('click', '.deleteDepartment', function () {
    var Department = {};
    Department.obj = 'Department';
    Department.btn = $(this);
    Delete(Department);

});

$(document).on('click', '.deleteCompany', function () {
    var Company = {};
    Company.obj = 'Company';
    Company.btn = $(this);
    Delete(Company);
});