var modelObj = {};
modelObj.prop1 = $('#txtField1').val();
modelObj.prop2 = $('#txtField2').val();
// etc... make sure the properties of this model match EditScreenModelValidation

var postObj = JSON.stringify(modelObj); // convert object to json

$.ajax({
    type: "POST",
    traditional: true,
    dataType: "json",
    url: "/Workflow/Home/ProcessWorkflowAction",
    data: { model: postObj, stepActionId: stepId, stepNumber: 3 }
    cache: false,
    complete: function (data) {
        if (data.responseText.length > 0) {
            var values = $.parseJSON(data.responseText)
            $('#ActionErrors').html(values.message)
        }
        else {
            location.reload();
        }
    }
});