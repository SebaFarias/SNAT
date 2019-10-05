// Value is the element to be validated, params is the array of name/value pairs of the parameters extracted from the HTML, element is the HTML element that the validator is attached to
$.validator.addMethod("requeridosegunvalor", function (value, element, params) {
    var $control = $("[id$='" + params[0] + "']");

    // Control a evaluar del tipo checkbox
    if ($control.is("input[type='checkbox']")) {
        if (value == '' && $control.prop('checked')) {
            return false;
        }
        else {
            return true;
        }
    }

    // Select (ComboBox)
    if ($control.is("select")) {
        if (value == '' && $control.val() == params[1]) {
            return false;
        }
        else {
            return true;
        }
    }

    // Resto de controles
    if ($control.is("input")) {
        if (value == '' && $control.val() == params[1]) {
            return false;
        }
        else {
            return true;
        }
    }
});

$.validator.addMethod("requeridoarchivosegunvalor", function (value, element, params) {
    if (value == '' && $(params[0]).val() == params[1])
        return false;
    else
        return true;
});

$.validator.addMethod("validarango", function (value, element, params) {
    var form = this.currentForm;
    var propiedad = Globalize.parseFloat($("[id$='" + params[0] + "']", form).val());
    var valor = Globalize.parseFloat(value);
    var debeSerMayor = JSON.parse(params[1].toLowerCase());
    var debeSerIgual = JSON.parse(params[2].toLowerCase());

    if (debeSerMayor) { // si value debe ser mayor
        if (debeSerIgual) {
            if (value != '' && propiedad >= valor)
                return false;
        } else {
            if (value != '' && propiedad > valor)
                return false;
        }
    } else {
        if (debeSerIgual) {
            if (value != '' && propiedad <= valor)
                return false;
        } else {
            if (value != '' && propiedad < valor)
                return false;
        }
    }

    return true;
});

// Validador de archivo
$.validator.addMethod("validararchivo", function (value, element, params) {
    var file = GetNameFromPath(value);
    if (file == null)
        return true;
    else {
        var extension = file.substr(file.lastIndexOf('.') + 1);
        var extValidas = params;
        var res = extValidas.match(extension);
        if (res == null)
            return false;
        else
            return true;
    }
});

$.validator.addMethod("tamarchivo", function (value, element, params) {
    var file = GetNameFromPath(value);
    if (file == null)
        return true;
    else {
        var size = GetFileSize(element);
        if (size > parseInt(params))
            return false;
        else
            return true;
    }
});

$.validator.addMethod("validarrut", function (value, element, params) {
    if ($.Rut.validar(value) || !value)
        return true;
    else {
        return false;
    }
});

$.validator.addMethod("validarcheckbox", function (value, element, params) {
    if ($(element).prop('checked'))
        return true;
    else {
        return false;
    }
});

GetFileSize = function (element) {
    try {
        var fileSize = 0;
        //for IE
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf("MSIE ");

        if (msie > 0 || !!ua.match(/Trident.*rv\:11\./)) {
            //before making an object of ActiveXObject, 
            //please make sure ActiveX is enabled in your IE browser
            //var objFSO = new ActiveXObject("Scripting.FileSystemObject"); var filePath = $("#" + fileid)[0].value;
            var objFSO = new ActiveXObject("Scripting.FileSystemObject");
            var filePath = $(element)[0].value;
            var objFile = objFSO.getFile(filePath);
            var fileSize = objFile.size; //size in kb
            fileSize = fileSize / 1048576; //size in mb 
        }
        //for FF, Safari, Opeara and Others
        else {
            //fileSize = $("#" + fileid)[0].files[0].size //size in kb
            fileSize = $(element)[0].files[0].size //size in kb
            fileSize = fileSize / 1048576; //size in mb 
        }

        return fileSize;
    }
    catch (e) {
        alert("" + e);
    }
}

GetNameFromPath = function (strFilePath) {
    var objRE = new RegExp(/([^\/\\]+)$/);
    if (strFilePath == '')
        return null;
    else {
        var strName = objRE.exec(strFilePath);

        if (strName == null) {
            return null;
        }
        else {
            return strName[0];
        }
    }
}

/* The adapter signature:
adapterName is the name of the adapter, and matches the name of the rule in the HTML element.
 
params is an array of parameter names that you're expecting in the HTML attributes, and is optional. If it is not provided,
then it is presumed that the validator has no parameters.
 
fn is a function which is called to adapt the HTML attribute values into jQuery Validate rules and messages.
 
The function will receive a single parameter which is an options object with the following values in it:
element
The HTML element that the validator is attached to
 
form
The HTML form element
 
message
The message string extract from the HTML attribute
 
params
The array of name/value pairs of the parameters extracted from the HTML attributes
 
rules
The jQuery rules array for this HTML element. The adapter is expected to add item(s) to this rules array for the specific jQuery Validate validators
that it wants to attach. The name is the name of the jQuery Validate rule, and the value is the parameter values for the jQuery Validate rule.
 
messages
The jQuery messages array for this HTML element. The adapter is expected to add item(s) to this messages array for the specific jQuery Validate validators that it wants to attach, if it wants a custom error message for this rule. The name is the name of the jQuery Validate rule, and the value is the custom message to be displayed when the rule is violated.
*/
$.validator.unobtrusive.adapters.add("requeridosegunvalor", ["propiedadacomparar", "valorrequerido"], function (options) {
    options.rules["requeridosegunvalor"] = [options.params.propiedadacomparar, options.params.valorrequerido];
    options.messages["requeridosegunvalor"] = options.message;
});

$.validator.unobtrusive.adapters.add("requeridoarchivosegunvalor", ["propiedadacomparar", "valorrequerido"], function (options) {
    options.rules["requeridoarchivosegunvalor"] = ["#" + options.params.propiedadacomparar, options.params.valorrequerido];
    options.messages["requeridoarchivosegunvalor"] = options.message;
});

$.validator.unobtrusive.adapters.add("validarango", ["propiedadacomparar", "debesermayor", "debeserigual"], function (options) {
    options.rules["validarango"] = [options.params.propiedadacomparar, options.params.debesermayor, options.params.debeserigual];
    options.messages["validarango"] = options.message;
});

// Validador de archivo
$.validator.unobtrusive.adapters.add("validararchivo", ["extensiones"], function (options) {
    options.rules["validararchivo"] = options.params.extensiones;
    options.messages["validararchivo"] = options.message;
});

$.validator.unobtrusive.adapters.add("tamarchivo", ["tam"], function (options) {
    options.rules["tamarchivo"] = options.params.tam;
    options.messages["tamarchivo"] = options.message;
});

$.validator.unobtrusive.adapters.add("validarrut", [], function (options) {
    options.rules["validarrut"] = "";
    options.messages["validarrut"] = options.message;
});

$.validator.unobtrusive.adapters.add("validarcheckbox", [], function (options) {
    options.rules["validarcheckbox"] = "";
    options.messages["validarcheckbox"] = options.message;
});