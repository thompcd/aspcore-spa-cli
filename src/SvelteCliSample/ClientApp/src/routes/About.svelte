<script>

// The proper structure for a part number's test list is

const testList = {
  "PartNumber": "78350794",
  "TestList": {
    "INSTALLPV1": "PASS",
    "TESTMODE": "0xFFFFFFFFFFFFFFFF",
    "SWIPE_SCALLYWAG": "",
    "DIN1LOW": "",
    "DIN1HIGH": "",
    "DIN2LOW": "",
    "DIN2HIGH": "",
    "DIN3LOW": "",
    "DIN3HIGH": "",
    "DIN4LOW": "",
    "DIN4HIGH": "",
    "DIN5LOW": "",
    "DIN5HIGH": "",
    "IOAN1REST_276_OHM": "90,110",
    "IOAN105V": "1.020,1.220",
    "IOAN2REST_276_OHM": "90,110",
    "IOAN205V": "1.020,1.220",
    "IOAN3REST_276_OHM": "90,110",
    "IOAN305V": "1.020,1.220",
    "FREQOUT1": "247,253",
    "FREQOUT2": "247,253",
    "CANPT2": "20",
    "WAV11TEST": "PASS",
    "FMRFTEST": "PASS",
    "AMRFTEST": "PASS",
    "WXRFTEST": "PASS",
    "ETHERNET": "PASS",
    "WIFI": "PASS",
    "Bootstrap_BlackLabel": "PASS",
	"RADIO": "1",
    "IGNIN": "-0.300,0.300"
  }
}

const availableTests = [
	{	"testSelected":false,
		"testName":"Digital Input 1 Hi",
		"lowerLimit":"PASS",
		"testValue":"",
		"upperLimit":"PASS",
		"result":"",
		"testStatus":"","id":0 },
	{	"testSelected":false,
		"testName":"Digital Input 2 Hi",
		"lowerLimit":"PASS",
		"testValue":"",
		"upperLimit":"PASS",
		"result":"",
		"testStatus":"","id":1	},
	{	"testSelected":false,
		"testName":"Analog Input 1, 0-5V",
		"lowerLimit":"4.5",
		"testValue":"",
		"upperLimit":"4.7",
		"result":"",
		"testStatus":"","id":2	}
	]
  $:console.log(JSON.stringify(testList));
  $:console.log(JSON.stringify(availableTests));


</script>


<svelte:head>
	<title>About</title>
</svelte:head>

<h1>A sample test list structure</h1>
<h8>
{JSON.stringify(testList, null, "\t")}
</h8>
<br>
<h8>
{JSON.stringify(availableTests, null, "\t")}
</h8>