function ShowLoading() {
    $('#modalCargando').modal({
        keyboard: false,
        backdrop: 'static'
    });
}

function CloseLoading() {
    $('#modalCargando').modal('hide');
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('.solo-numero').keyup(function () {
        this.value = (this.value + '').replace(/[^0-9]/g, '');
    });
    //Minvu.botonVolverArriba();
});

function cortadecimales(monto) {
    var flag = parseFloat(monto.value);
    if (monto.toString().length > 0) {
        if (flag.toString() == "NaN") {
            document.getElementById(monto.id).value = "";
        }
        else {
            var monto0 = monto.value.toString().replace(",", ".");
            var v = parseFloat(monto0);
            var a = Math.floor(v * 100) / 100;
            var monto1 = a.toFixed(2);
            var monto2 = 0;
            monto2 = monto1.toString().replace(".", ",");
            document.getElementById(monto.id).value = monto2;
        }
    }
}

function cortaEspacios(txt) {
    if (txt.value.length > 1) {
        document.getElementById(txt.id).value = txt.value.replace("  ", " ");
    } else {
        document.getElementById(txt.id).value = txt.value.replace(" ", "");
    }
}

function cortaGuiones(txt) {
    if (txt.value.length > 1) { // posicion 0 y 1
        if (txt.value.indexOf("-") != -1) {

            if (txt.value.indexOf("-") != (txt.value.length - 2)) {
                var largo = txt.value.length;
                for (var i = 0; i < largo; i++) {

                    if (txt.value[0] == "-") {
                        txt.value = txt.value.substr(1);
                    }

                    if (txt.value[txt.value.length - 1] == "-") {
                        txt.value = txt.value.substr(0, txt.value.length - 1);
                    }
                }
            }
            document.getElementById(txt.id).value = txt.value;
        }
    } else {
        txt.value = txt.value.replace("-", "");
        document.getElementById(txt.id).value = txt.value;
    }
}

//Variable en la cual se setea el lenguaje para las grillas, jquery Datatables
var lenguajeGrilla = {
    "infoEmpty": "",
    "lengthMenu": "Mostrar _MENU_ registros por página",
    "info": "Página _PAGE_ de _PAGES_ | _MAX_ registros",
    "zeroRecords": "No se encontraron registros",
    "sLoadingRecords": "Cargando...",
    "sProcessing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
    "sInfoFiltered": "(filtros aplicados)",
    "sSearch": "Buscar:",
    "sInfoPostFix": "",
    "sInfoThousands": ".",
    "oPaginate": {
        "sFirst": "Primero",
        "sPrevious": "Anterior",
        "sNext": "Siguiente",
        "sLast": "Último"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
};

function getProvincias(region, idControl, comuna, localidad) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append('<option value="">--Seleccione--</option>');

    $('#' + comuna.id).find('option').remove().end();
    $('#' + comuna.id).append('<option value="">--Seleccione--</option>');

    $('#' + localidad.id).find('option').remove().end();
    $('#' + localidad.id).append('<option value="0">--Seleccione--</option>');


    if (region.value != null) {
        $.post('/Service/Provincias'
            , { idRegion: region.value }
            , function (data) {
                $.each(data, function (i, item) {
                    $('#' + idControl.id).append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            });
    }
}

function getComunas(region, provincia, idControl, localidad) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append('<option value="">--Seleccione--</option>');

    $('#' + localidad.id).find('option').remove().end();
    $('#' + localidad.id).append('<option value="0">--Seleccione--</option>');

    if (region.value != null && provincia.value != null) {
        $.post('/Service/Comunas'
            , { idRegion: region.value, idProvincia: provincia.value }
            , function (data) {
                $.each(data, function (i, item) {
                    $('#' + idControl.id).append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            });
    }
}

function getLlamadosPorAgno(ddlAgno, idDdlLlamado) {
    var agno = $('#' + ddlAgno).val();
    if (agno) {
        $('#' + idDdlLlamado).removeAttr("disabled");
        $('#' + idDdlLlamado).find('option').remove().end();
        return $.post('/Service/LlamadosAgno'
            , { Agno: agno }
            , function (data) {
                $.each(data, function (i, item) {
                    $('#' + idDdlLlamado).append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            });
    }
    else {
        agno = 0
        $('#' + idDdlLlamado).removeAttr("disabled");
        $('#' + idDdlLlamado).find('option').remove().end();
        $('#' + idDdlLlamado).attr("disabled", "true");
        return $.post('/Service/LlamadosAgno'
            , { Agno: agno }
            , function (data) {
                $.each(data, function (i, item) {
                    $('#' + idDdlLlamado).append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            });

    }

}

function getComunasSinProvincia(ddlRegion, idDdlComuna, idComunaSelected) {
    var regid = $('#' + ddlRegion).val();
    $('#' + idDdlComuna).find('option').remove().end();
    //$('#' + idDdlComuna).append('<option value="">-- Seleccione --</option>');
    if (regid == '') { regid = '0' }
    if (regid != null && regid != "") {
        return $.post('/Service/Comunas'
            , { idRegion: regid, idProvincia: 0 }
            , function (data) {
                $.each(data, function (i, item) {
                    $('#' + idDdlComuna).append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            });
    }
}

function getRegionesSelected(IDddlRegion) {
    var regid = 0;
    $('#' + IDddlRegion).find('option').remove().end();

    ShowLoading();

    $.post('/api/core/regiones', function (data) {
        $.each(data, function (i, item) {
            $('#' + IDddlRegion).append($('<option>', {
                value: item.value,
                text: item.text
            }));

            if (item.selected) {
                regid = item.value;
            }

        });
        $('#' + IDddlRegion).val(regid).change();
        CloseLoading();
    });
}

function getRegiones(IDddlRegion) {
    var regid = 0;
    $('#' + IDddlRegion).find('option').remove().end();

    ShowLoading();

    $.post('/Service/Regiones', function (data) {
        $.each(data, function (i, item) {
            $('#' + IDddlRegion).append($('<option>', {
                value: item.Value,
                text: item.Text
            }));

            if (item.Selected) {
                regid = item.Value;
            }

        });
        $('#' + IDddlRegion).val('').change();
        CloseLoading();
    });
}

function getHabitantesPorComuna(ddlComuna, idTextBox) {
    var comid = $('#' + ddlComuna).val();
    if (comid != null && comid != "") {
        $.post('/Service/HabitantesPorComuna'
            , { idComuna: comid }
            , function (data) {
                $('#' + idTextBox).val(data);
                $('#' + idTextBox).prettynumber();
            });
    }
}

function getLocalidad(Comuna, idControl) {
    $('#' + idControl.id).find('option').remove().end();
    $('#' + idControl.id).append($('<option>', {
        value: 0,
        text: '--Seleccione--'
    }));

    if (Comuna.value != null) {
        $.post('/Service/Localidades'
            , { idComuna: Comuna.value }
            , function (data) {
                $.each(data, function (i, item) {
                    $('#' + idControl.id).append($('<option>', {
                        value: item.Value,
                        text: item.Text
                    }));
                });
            });
    }
}

function getEntidad(rut, strRegion) {
    if (rut.val() != null) {
        var aux = rut.val().replace('.', '');
        aux = aux.replace('.', '');

        if (VerificaRut(aux) == false) {
            CloseLoading();
            $('#ED_entNom').val("");
            $('#ModalError').find('.modal-body').text('El RUT ingresado no es válido.')
            $('#ModalError').modal('show');
            return;
        }
        else {
            ShowLoading();
            rut.attr("disabled", "true")
            rut.removeAttr("disabled");

            $.post('/Service/Entidad'
                , { rutCompleto: rut.val(), region: strRegion }
                , function (data) {
                    $('#ED_entId').val(data.entId);
                    $('#ED_entNom').val(data.entNom);
                    CloseLoading();
                })
        }

    }
}


function getInmobiliaria(rut, nombre) {

    //if ($(rut).val() == "") {
    //    $(rut).popover("show");
    //    return;
    //}
    if (rut.value != null) {

        var aux = rut.value.replace('.', '');
        aux = aux.replace('.', '');

        if (VerificaRut(aux) == false) {
            CloseLoading();
            document.getElementById('lblNombre').innerText = " ";
            $('#ModalError').find('.modal-body').text('El RUT ingresado no es válido.')
            $('#ModalError').modal('show');
            //$(rut).popover("show");
            return;
        }

    }

    if (rut.value != null) {
        ShowLoading();


        //remueve validaciones del input-fields
        $('#_txtRut').addClass('input-validation-valid');
        $('#_txtRut').removeClass('input-validation-error');
        //remueve mensajes de validacion input-fields
        $('#idSpanRut').hide();

        //$('#_txtRut .field-validation-error').addClass('field-validation-valid');
        //$('#_txtRut .field-validation-error').removeClass('field-validation-error');
        //remueve validaciones summary 
        //$('#_txtRut .validation-summary-errors').addClass('validation-summary-valid');
        //$('#_txtRut .validation-summary-errors').removeClass('validation-summary-errors')

        $.post('/Service/Inmobiliaria'
            , { rutCompleto: rut.value }
            , function (data) {
                if (data.estado == "VIGENTE") {
                    //$('#lblnombre').append(data.razonSocial);
                    $('#lblNombre').empty();
                    //  $('#' + nombre.id).removeAttr(data.razonSocial)
                    $('#lblNombre').append(data.razonSocial);
                    document.getElementById('lblNombre').innerText = " ";
                    document.getElementById('lblNombre').innerText = data.razonSocial;
                    document.getElementById('razonSocialInmobiliaria').value = data.razonSocial;
                    document.getElementById('rutInmobiliaria').value = data.rutConcatenado;
                    document.getElementById('estadoInmobiliaria').value = data.estado;
                    CloseLoading();


                }
                else if (data.estado == "NULL") {
                    CloseLoading();
                    document.getElementById('lblNombre').innerText = " ";
                    $('#ModalError').find('.modal-body').text('El RUT ingresado no existe en los registros técnicos o no es válido.')
                    document.getElementById('razonSocialInmobiliaria').value = "";
                    $('#ModalError').modal('show');
                    return;
                }
                else {
                    CloseLoading();
                    document.getElementById('lblNombre').innerText = " ";
                    $('#ModalError').find('.modal-body').text('La empresa constructora no se encuentra vigente en los Registros Técnicos.  Debe regularizar su situación para participar en un proceso de postulación.')
                    document.getElementById('estadoInmobiliaria').value = data.estado;
                    document.getElementById('razonSocialInmobiliaria').value = "";
                    $('#ModalError').modal('show');
                    return;
                }

            }
        )
    }
}

function ConfirmarCancelar() {
    var consulta = confirm('¿Esta seguro que desea cancelar?');
    if (consulta) {
        alert(" ");

    }
    else { alert(" ") }

}


function CheckBancoOnline(labelVb, ddlBanco) {
    //   var idBanco = ddlBanco.value;
    $.post('/Service/verificaBanco'
        , { idBanco: ddlBanco.value }
        , function (data) {
            if (data == "True") {
                labelVb.innerHTML = "VB Entidad Financiera no en línea"
            }
            else {

                labelVb.innerHTML = "VB Entidad Financiera no en línea (*)"
            }
        }
    )
}

function VerificaRut(rut) {
    if (rut.toString().trim() != '') {

        var caracteres = new Array();
        var serie = new Array(2, 3, 4, 5, 6, 7);
        var dig = rut.toString().substr(rut.toString().length - 1, 1);
        rut = rut.toString().substr(0, rut.toString().length - 2);

        for (var i = 0; i < rut.length; i++) {
            caracteres[i] = parseInt(rut.charAt((rut.length - (i + 1))));
        }

        var sumatoria = 0;
        var k = 0;
        var resto = 0;

        for (var j = 0; j < caracteres.length; j++) {
            if (k == 6) {
                k = 0;
            }
            sumatoria += parseInt(caracteres[j]) * parseInt(serie[k]);
            k++;
        }

        resto = sumatoria % 11;
        dv = 11 - resto;

        if (dv == 10) {
            dv = "K";
        }
        else if (dv == 11) {
            dv = 0;
        }

        if (dv.toString().trim().toUpperCase() == dig.toString().trim().toUpperCase())
            return true;
        else
            return false;
    }
    else {
        return false;
    }
}
