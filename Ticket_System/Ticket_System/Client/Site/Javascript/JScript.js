		  function payOut(pay)
	{

	document.getElementById('ctl00_ContentPlaceHolder1_txtAgentID').value =pay.value;
	}
    
    function searchValidate()
    {
    if (document.getElementById('ctl00_ContentPlaceHolder1_txtSearchBox').value=="")
    {
    alert("Please Enter a value for Search");
    return false;
    }
    else
    {
    return true;
    }
    
    }
    var oldgridSelectedColor;
function setMouseOverColor(element) {

    oldgridSelectedColor = element.style.backgroundColor;
    element.style.backgroundColor='yellow';
    element.style.cursor='hand';
    element.style.textDecoration='underline';
}

function setMouseOutColor(element) {

    element.style.backgroundColor=oldgridSelectedColor;
    element.style.textDecoration='none';
}
	
		function cal_Amt_PayOUT_SDG()
		{		
			var ntxtComm = document.getElementById("ctl00_ContentPlaceHolder1_txtComm").value;
			var ntxtExchComm = document.getElementById("ctl00_ContentPlaceHolder1_TXTAMT_AGENTCOMMISSION").value;
			var ntxtAmt_Out = document.getElementById("ctl00_ContentPlaceHolder1_txtAmt_Out").value;
			ntxtAmt_Out =(isNaN(parseFloat(ntxtAmt_Out))) ? "0.00" : ntxtAmt_Out;	
			if (ntxtAmt_Out=="0.00")
			{
				return;
			}
			
			var stPaymentCurrency=document.getElementById("ctl00_ContentPlaceHolder1_lblPaymentCurrency").outerText;
	
			if (stPaymentCurrency=="(USD)")
			{
				document.getElementById("ctl00_ContentPlaceHolder1_txtTotal").value = parseFloat(ntxtComm) +parseFloat(ntxtAmt_Out)	
			}
			else
			{		
				var ntxtrRate = document.getElementById("ctl00_ContentPlaceHolder1_lblRate").value;
				var ntxtrLCRate = document.getElementById("ctl00_ContentPlaceHolder1_LBLAGENT_LCRATE_USD").value;
				
				var nresult=parseFloat(ntxtComm) +(parseFloat(ntxtAmt_Out)/ntxtrRate);
				
				document.getElementById("ctl00_ContentPlaceHolder1_txtTotal").value = Math.round(nresult*1000)/1000;
				var nTotalAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotal").value;
				var nresultLC=parseFloat(ntxtExchComm) +(parseFloat(nTotalAmount)*ntxtrLCRate);	
				document.getElementById("ctl00_ContentPlaceHolder1_TXTAMT_TOTALCOLLECTION_LC").value = Math.round(nresultLC*1000)/1000;
				
			}
			
		
		}
		
	function cal_Amt_PayOUT()
	{		
	
		var ntxtComm = document.getElementById("ctl00_ContentPlaceHolder1_txtComm").value;
		var ntxtExchComm = document.getElementById("ctl00_ContentPlaceHolder1_TXTAMT_AGENTCOMMISSION").value;
		
		var ntxtAmt_Out = document.getElementById("ctl00_ContentPlaceHolder1_txtAmt_Out").value;
		
		ntxtAmt_Out =(isNaN(parseFloat(ntxtAmt_Out))) ? "0.00" : ntxtAmt_Out;	
		if (ntxtAmt_Out=="0.00"){
			return;
		}
		var stPaymentCurrency=document.getElementById("ctl00_ContentPlaceHolder1_lblPaymentCurrency").outerText;
	
		if (stPaymentCurrency=="(USD)")
		{
			document.getElementById("ctl00_ContentPlaceHolder1_txtTotal").value = parseFloat(ntxtComm) +parseFloat(ntxtAmt_Out)	
			var nTotalAmount = document.getElementById("ctl00_ContentPlaceHolder1_txtTotal").value;
			var ntxtrLCRate = document.getElementById("ctl00_ContentPlaceHolder1_LBLAGENT_LCRATE_USD").value;
			var nresultLC=parseFloat(ntxtExchComm) +(parseFloat(nTotalAmount)*ntxtrLCRate);
			document.getElementById("ctl00_ContentPlaceHolder1_TXTAMT_TOTALCOLLECTION_LC").value = Math.round(nresultLC*1000)/1000;
		}
		else
		{		
		var ntxtrRate = document.getElementById("ctl00_ContentPlaceHolder1_lblRate").value;		
		var nresult=parseFloat(ntxtComm) +(parseFloat(ntxtAmt_Out)/	ntxtrRate)
		
		document.getElementById("ctl00_ContentPlaceHolder1_txtTotal").value = Math.round(nresult*1000)/1000		
		
		
		}
		
	}
	function vldNb(Obj,typ)
		{
			
			var checkOK = "0123456789.";
			var checkStr = Obj.value;
			
			var allValid = true;
			var allNum = "";
			var DOTCount=0;
			//*************************************
			
			for (i = 0;  i < checkStr.length;  i++)
			{
				ch = checkStr.charAt(i);
				if (ch == "."){
					DOTCount = DOTCount + 1
				} 
			}
			if (DOTCount>1){
				alert("Please enter only digit characters in the field.");
				Obj.value=0;
				
			}
			//*************************************
			for (i = 0;  i < checkStr.length;  i++)
			{
				ch = checkStr.charAt(i);
				for (j = 0;  j < checkOK.length;  j++)
					if (ch == checkOK.charAt(j))
						break;
					if (j == checkOK.length)
					{
						allValid = false;
						break;
					}
					if (ch != ",")
						allNum += ch;
			}
					if (!allValid)
					{
						alert("Please enter only digit characters in the field.");
						Obj.value=0;
					}
					
					if (typ==1){
						Obj.value=pad_with_zeros(Obj.value,6);	
					}
					if (typ==2){
						Obj.value = pad_with_zeros(Obj.value,2);
					}
					
					
		}
				
		
	function pad_with_zeros(rounded_value, decimal_places) {
    var value_string = rounded_value.toString()
    var decimal_location = value_string.indexOf(".")
    if (decimal_location == -1) {
        decimal_part_length = 0
        value_string += decimal_places > 0 ? "." : ""
    }
    else {
        decimal_part_length = value_string.length - decimal_location - 1
    }
    var pad_total = decimal_places - decimal_part_length
    if (pad_total > 0) {
        for (var counter = 1; counter <= pad_total; counter++)
            value_string += "0"
        }
    return value_string
}


function addCommas(nStr){
 nStr += '';
 x = nStr.split('.');
 x1 = x[0];
 x2 = x.length > 1 ? '.' + x[1] : '';
 var rgx = /(\d+)(\d{3})/;
 while (rgx.test(x1)) {
  x1 = x1.replace(rgx, '$1' + ',' + '$2');
 }
 return x1 + x2;
}

//*****************************************************************************************************
//*****************************************************************************************************
//*****************************************************************************************************
//*****************************************************************************************************
 
function extractNumber(obj, decimalPlaces, allowNegative)
{
	var temp = obj.value;
	
	// avoid changing things if already formatted correctly
	var reg0Str = '[0-9]*';
	if (decimalPlaces > 0) {
		reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
	} else if (decimalPlaces < 0) {
		reg0Str += '\\.?[0-9]*';
	}
	reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
	reg0Str = reg0Str + '$';
	var reg0 = new RegExp(reg0Str);
	if (reg0.test(temp)) return true;

	// first replace all non numbers
	var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
	var reg1 = new RegExp(reg1Str, 'g');
	temp = temp.replace(reg1, '');

	if (allowNegative) {
		// replace extra negative
		var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
		var reg2 = /-/g;
		temp = temp.replace(reg2, '');
		if (hasNegative) temp = '-' + temp;
	}
	
	if (decimalPlaces != 0) {
		var reg3 = /\./g;
		var reg3Array = reg3.exec(temp);
		if (reg3Array != null) {
			// keep only first occurrence of .
			//  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
			var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
			reg3Right = reg3Right.replace(reg3, '');
			reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
			temp = temp.substring(0,reg3Array.index) + '.' + reg3Right;
		}
	}
	
	obj.value = temp;
}

     function Comma(Num)
 {
       Num += '';
       Num = Num.replace(/,/g, '');

       x = Num.split('.');
       x1 = x[0];
       x2 = x.length > 1 ? '.' + x[1] : '';
       var rgx = /(\d+)(\d{3})/;
       while (rgx.test(x1))
       x1 = x1.replace(rgx, '$1' + ',' + '$2');
       return x1 + x2;
 } 

function isNumberKey(evt) 
{
 if(event.keyCode < 46 || event.keyCode > 57 || event.keyCode == 47) { 
  event.returnValue = false;
 }
}






function blockNonNumbers(obj, e, allowDecimal, allowNegative)
{
	var key;
	var isCtrl = false;
	var keychar;
	var reg;
		
	if(window.event) {
		key = e.keyCode;
		isCtrl = window.event.ctrlKey
	}
	else if(e.which) {
		key = e.which;
		isCtrl = e.ctrlKey;
	}
	
	if (isNaN(key)) return true;
	
	keychar = String.fromCharCode(key);
	
	// check for backspace or delete, or if Ctrl was pressed
	if (key == 8 || isCtrl)
	{
		return true;
	}

	reg = /\d/;
	var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
	var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;
	
	return isFirstN || isFirstD || reg.test(keychar);
}