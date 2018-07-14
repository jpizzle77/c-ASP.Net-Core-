// Write your JavaScript code.
/*$(function(){
     var $select = $(".1-100");
        for (i=1;i<=100;i++){$select.append($('<option></option>').val(i).html(i))
    }
});​*/


var select = '';
for (i=1;i<=200;i++)
{
    
    select += '<option val=' + i + '>' + i + '</option>';
}
$('#some_selector').html(select);


/*function Validate()
{
var e = document.getElementById("ddlView");
var strUser = e.options[e.selectedIndex].value;

var strUser1 = e.options[e.selectedIndex].text;
if(strUser==0) 
{
alert("Please select a user");
}
}


function JSFunctionValidate()
{
if(document.getElementById('<%=ddlView.ClientID%>').selectedIndex == 0)
{
alert("Please select ddl");
return false;
}
return true;
}*/